using System.Data;
using Dapper;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace JobFlowOOH.JobFlow.Writing.Experimental;

internal static class ExperimentalJobTransaction
{
    internal sealed record Request(
        string ClientCode,
        string SiteCode,
        string Description,
        string OrderNumber,
        string ReportedBy,
        string SiteContactName,
        DateTimeOffset ReceivedAt,
        int ResponseCategoryCode,
        string SectionCode,
        string SubsectionCode,
        int RequestedEngineerEmployeeId,
        string Username,
        string CallTakenBy,
        int JobType,
        string TimeZoneId,
        Guid CorrelationId,
        string? SelectSectionCodeOverride = null,
        string? UserLogUserOverride = null);

    internal sealed record Result(
        int JobNumber,
        string? ClientName,
        string? SiteName,
        int CategoryHours,
        string? CategoryName,
        DateTimeOffset ReceivedAt,
        DateTimeOffset Deadline,
        string? SectionName,
        string? SubsectionName,
        string? EngineerName,
        int JobType);

    public static async Task<Result> ExecuteAsync(
        string connectionString,
        Request request,
        TimeProvider clock,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        ExperimentalWriteGuard.EnsureConnection(connectionString);
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        var databaseName = await connection.QuerySingleAsync<string>(Command("SELECT DB_NAME()", null, null, cancellationToken));
        if (!string.Equals(databaseName, PocMarkers.JobFlowDatabaseName, StringComparison.Ordinal))
        {
            throw new JobFlowExperimentalWriteException("Experimental writes are refused unless the connected database is JobFlowDemo.");
        }

        await connection.ExecuteAsync(Command("SET XACT_ABORT ON", null, null, cancellationToken));
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        try
        {
            var prepared = await PrepareAsync(connection, transaction, request, clock, cancellationToken);
            var jobNumber = await InsertOrderAsync(connection, transaction, request, prepared, cancellationToken);
            await InsertSectionAsync(connection, transaction, jobNumber, request, cancellationToken);
            await InsertOrderLogAsync(connection, transaction, jobNumber, request, prepared.AuditLocal, cancellationToken);
            await VerifyAsync(connection, transaction, jobNumber, request, prepared, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation(
                "Experimental JobFlowDemo job {JobNumber} committed. Correlation {CorrelationId}. Engineer {EngineerId} was not assigned in JobFlow.",
                jobNumber,
                request.CorrelationId,
                request.RequestedEngineerEmployeeId);
            return new Result(
                jobNumber,
                prepared.ClientName,
                prepared.SiteName,
                prepared.CategoryHours,
                prepared.CategoryName,
                prepared.Times.Received,
                prepared.Times.Deadline,
                prepared.SectionName,
                prepared.SubsectionName,
                prepared.EngineerName,
                request.JobType);
        }
        catch (JobCommandValidationException)
        {
            await RollbackAsync(transaction, logger, request.CorrelationId, cancellationToken);
            throw;
        }
        catch (JobFlowExperimentalWriteException)
        {
            await RollbackAsync(transaction, logger, request.CorrelationId, cancellationToken);
            throw;
        }
        catch (Exception exception)
        {
            await RollbackAsync(transaction, logger, request.CorrelationId, cancellationToken);
            logger.LogError(
                exception,
                "Rolled back experimental JobFlowDemo job. Correlation {CorrelationId}.",
                request.CorrelationId);
            throw new JobFlowExperimentalWriteException(JobFlowExperimentalWriteException.SafeMessage, exception);
        }
    }

    private static async Task<Prepared> PrepareAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        Request request,
        TimeProvider clock,
        CancellationToken cancellationToken)
    {
        var clientName = await connection.QuerySingleOrDefaultAsync<string?>(
            Command(
                "SELECT client FROM dbo.client WHERE clientcode = @ClientCode",
                new { request.ClientCode },
                transaction,
                cancellationToken));
        if (clientName is null && !await ExistsAsync(connection, transaction, "SELECT clientcode FROM dbo.client WHERE clientcode = @ClientCode", new { request.ClientCode }, cancellationToken))
        {
            throw new JobCommandValidationException(["Client was not found."]);
        }

        var site = await connection.QuerySingleOrDefaultAsync<SiteRow>(
            Command(
                """
                SELECT house AS Name, clientcode AS ClientCode
                FROM dbo.houselist
                WHERE sitecode = @SiteCode
                """,
                new { request.SiteCode },
                transaction,
                cancellationToken));
        if (site is null)
        {
            throw new JobCommandValidationException(["Site was not found."]);
        }

        if (!string.Equals(site.ClientCode, request.ClientCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new JobCommandValidationException(["Site does not belong to the selected client."]);
        }

        var category = await connection.QuerySingleOrDefaultAsync<CategoryRow>(
            Command(
                """
                SELECT category AS Hours, categoryname AS Name
                FROM dbo.category
                WHERE categorycode = @ResponseCategoryCode
                """,
                new { request.ResponseCategoryCode },
                transaction,
                cancellationToken));
        if (category is null)
        {
            throw new JobCommandValidationException(["Category was not found."]);
        }

        if (category.Hours is null)
        {
            throw new JobCommandValidationException(["Category has no response duration."]);
        }

        var sectionName = await connection.QuerySingleOrDefaultAsync<string?>(
            Command(
                "SELECT sname FROM dbo.section WHERE scode = @SectionCode",
                new { request.SectionCode },
                transaction,
                cancellationToken));
        if (!await ExistsAsync(connection, transaction, "SELECT scode FROM dbo.section WHERE scode = @SectionCode", new { request.SectionCode }, cancellationToken))
        {
            throw new JobCommandValidationException(["Section was not found."]);
        }

        var subsection = await connection.QuerySingleOrDefaultAsync<SubsectionRow>(
            Command(
                """
                SELECT ssname AS Name, scode AS SectionCode
                FROM dbo.subsection
                WHERE sscode = @SubsectionCode
                """,
                new { request.SubsectionCode },
                transaction,
                cancellationToken));
        if (subsection is null)
        {
            throw new JobCommandValidationException(["Subsection was not found."]);
        }

        if (!string.Equals(subsection.SectionCode, request.SectionCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new JobCommandValidationException(["Section and subsection are not related."]);
        }

        if (!await ExistsAsync(connection, transaction, "SELECT username FROM dbo.loginuser WHERE username = @Username", new { Username = request.Username }, cancellationToken))
        {
            throw new JobCommandValidationException(["Development username is not a JobFlow login."]);
        }

        if (!await ExistsAsync(connection, transaction, "SELECT username FROM dbo.loginuser WHERE username = @Username", new { Username = request.CallTakenBy }, cancellationToken))
        {
            throw new JobCommandValidationException(["Development call taken by is not a JobFlow login."]);
        }

        var jobTypeExists = await connection.QuerySingleAsync<int>(
            Command(
                "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.orders WHERE jobtype = @JobType) THEN 1 ELSE 0 END",
                new { request.JobType },
                transaction,
                cancellationToken));
        if (jobTypeExists != 1)
        {
            throw new JobCommandValidationException(["Development job type does not occur on an existing job."]);
        }

        var engineerName = await connection.QuerySingleOrDefaultAsync<string?>(
            Command(
                """
                SELECT LTRIM(RTRIM(e.name))
                FROM dbo.employee AS e
                WHERE e.idno = @RequestedEngineerEmployeeId
                  AND LTRIM(RTRIM(e.name)) <> ''
                  AND EXISTS (
                        SELECT 1
                        FROM dbo.operative AS o
                        WHERE o.idno = e.idno
                      )
                """,
                new { request.RequestedEngineerEmployeeId },
                transaction,
                cancellationToken));
        if (string.IsNullOrWhiteSpace(engineerName))
        {
            throw new JobCommandValidationException([RequestedEngineerText.SelectValid]);
        }

        if (!request.Description.StartsWith(PocMarkers.DescriptionPrefix, StringComparison.Ordinal)
            || !request.OrderNumber.StartsWith(PocMarkers.OrderPrefix, StringComparison.Ordinal))
        {
            throw new JobCommandValidationException(["The job is missing its POC marker."]);
        }

        return new Prepared(
            clientName,
            site.Name,
            category.Hours.Value,
            category.Name,
            sectionName,
            subsection.Name,
            engineerName,
            LondonJobClock.Calculate(request.ReceivedAt, category.Hours.Value, request.TimeZoneId),
            LondonJobClock.LocalNow(clock, request.TimeZoneId));
    }

    private static async Task<int> InsertOrderAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        Request request,
        Prepared prepared,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.orders
            (
                [date],
                clientcode,
                sitecode,
                contactname,
                description,
                categorycode,
                deadline,
                orderno,
                calltakenby,
                username,
                rpby,
                jobtype
            )
            OUTPUT INSERTED.jobno
            VALUES
            (
                @ReceivedLocal,
                @ClientCode,
                @SiteCode,
                @SiteContactName,
                @Description,
                @CategoryCode,
                @DeadlineLocal,
                @OrderNumber,
                @CallTakenBy,
                @Username,
                @ReportedBy,
                @JobType
            )
            """;
        return await connection.QuerySingleAsync<int>(
            Command(
                sql,
                new
                {
                    ReceivedLocal = prepared.Times.ReceivedLocal,
                    request.ClientCode,
                    request.SiteCode,
                    request.SiteContactName,
                    request.Description,
                    CategoryCode = request.ResponseCategoryCode,
                    DeadlineLocal = prepared.Times.DeadlineLocal,
                    request.OrderNumber,
                    request.CallTakenBy,
                    request.Username,
                    request.ReportedBy,
                    request.JobType
                },
                transaction,
                cancellationToken));
    }

    private static async Task InsertSectionAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        int jobNumber,
        Request request,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.selectsection
            (
                jobno,
                sscode
            )
            VALUES
            (
                @JobNumber,
                @SubsectionCode
            )
            """;
        var subsectionCode = request.SelectSectionCodeOverride ?? request.SubsectionCode;
        var inserted = await connection.ExecuteAsync(
            Command(sql, new { JobNumber = jobNumber, SubsectionCode = subsectionCode }, transaction, cancellationToken));
        if (inserted != 1)
        {
            throw new InvalidOperationException("selectsection insert did not add one row.");
        }
    }

    private static async Task InsertOrderLogAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        int jobNumber,
        Request request,
        DateTime auditLocal,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.userlog
            (
                jobno,
                formname,
                luser,
                ldate
            )
            VALUES
            (
                @JobNumber,
                @FormName,
                @LoggedUser,
                @AuditLocal
            )
            """;
        var inserted = await connection.ExecuteAsync(
            Command(
                sql,
                new
                {
                    JobNumber = jobNumber,
                    FormName = "ORDER",
                    LoggedUser = request.UserLogUserOverride ?? request.Username,
                    AuditLocal = auditLocal
                },
                transaction,
                cancellationToken));
        if (inserted != 1)
        {
            throw new InvalidOperationException("userlog insert did not add one row.");
        }
    }

    private static async Task VerifyAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        int jobNumber,
        Request request,
        Prepared prepared,
        CancellationToken cancellationToken)
    {
        const string orderSql = """
            SELECT
                jobno AS JobNumber,
                clientcode AS ClientCode,
                sitecode AS SiteCode,
                categorycode AS CategoryCode,
                orderno AS OrderNumber,
                CAST(description AS varchar(4000)) AS DescriptionText,
                contactname AS ContactName,
                rpby AS ReportedBy,
                username AS Username,
                calltakenby AS CallTakenBy,
                jobtype AS JobType,
                CONVERT(varchar(19), [date], 120) AS ReceivedText,
                CONVERT(varchar(19), deadline, 120) AS DeadlineText,
                pmanager AS ProjectManager,
                pcodinator AS Coordinator,
                profitper AS Margin
            FROM dbo.orders
            WHERE jobno = @JobNumber
            """;
        var order = await connection.QuerySingleOrDefaultAsync<OrderRow>(
            Command(orderSql, new { JobNumber = jobNumber }, transaction, cancellationToken));
        if (order is null)
        {
            throw new InvalidOperationException("Read-back did not find the orders row.");
        }

        Require(order.JobNumber == jobNumber, "job number");
        Require(order.ClientCode == request.ClientCode, "client");
        Require(order.SiteCode == request.SiteCode, "site");
        Require(order.CategoryCode == request.ResponseCategoryCode, "category");
        Require(order.OrderNumber == request.OrderNumber, "order reference");
        Require(order.DescriptionText == request.Description, "description");
        Require(order.DescriptionText?.StartsWith(PocMarkers.DescriptionPrefix, StringComparison.Ordinal) == true, "description marker");
        Require(order.ReceivedText == LondonJobClock.SqlText(prepared.Times.ReceivedLocal), "received time");
        Require(order.DeadlineText == LondonJobClock.SqlText(prepared.Times.DeadlineLocal), "deadline");
        Require(order.ContactName == request.SiteContactName, "site contact");
        Require(order.ReportedBy == request.ReportedBy, "reported by");
        Require(order.Username == request.Username, "username");
        Require(order.CallTakenBy == request.CallTakenBy, "call taken by");
        Require(order.JobType == request.JobType, "job type");
        Require(order.ProjectManager is null && order.Coordinator is null && order.Margin is null, "project and margin fields");

        var expectedSubsection = request.SelectSectionCodeOverride ?? request.SubsectionCode;
        var sections = (await connection.QueryAsync<SectionRow>(
            Command(
                """
                SELECT jobno AS JobNumber, sscode AS SubsectionCode
                FROM dbo.selectsection
                WHERE jobno = @JobNumber
                """,
                new { JobNumber = jobNumber },
                transaction,
                cancellationToken))).AsList();
        Require(sections.Count == 1 && sections[0].JobNumber == jobNumber && sections[0].SubsectionCode == expectedSubsection, "selectsection");

        var expectedUser = request.UserLogUserOverride ?? request.Username;
        var logs = (await connection.QueryAsync<LogRow>(
            Command(
                """
                SELECT jobno AS JobNumber, formname AS FormName, luser AS LoggedUser, cname AS Cname
                FROM dbo.userlog
                WHERE jobno = @JobNumber AND formname = @FormName
                """,
                new { JobNumber = jobNumber, FormName = "ORDER" },
                transaction,
                cancellationToken))).AsList();
        Require(logs.Count == 1 && logs[0].JobNumber == jobNumber && logs[0].FormName == "ORDER" && logs[0].LoggedUser == expectedUser && logs[0].Cname is null, "userlog");
    }

    private static async Task<bool> ExistsAsync(
        SqlConnection connection,
        IDbTransaction transaction,
        string sql,
        object parameters,
        CancellationToken cancellationToken)
    {
        var value = await connection.ExecuteScalarAsync<object?>(Command(sql, parameters, transaction, cancellationToken));
        return value is not null and not DBNull;
    }

    private static void Require(bool condition, string check)
    {
        if (!condition)
        {
            throw new InvalidOperationException($"Read-back verification failed: {check}.");
        }
    }

    private static async Task RollbackAsync(
        IDbTransaction transaction,
        ILogger logger,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (transaction.Connection is not null)
            {
                await ((SqlTransaction)transaction).RollbackAsync(cancellationToken);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Rollback failed for correlation {CorrelationId}.", correlationId);
        }
    }

    private static CommandDefinition Command(string sql, object? parameters, IDbTransaction? transaction, CancellationToken cancellationToken)
    {
        return new CommandDefinition(sql, parameters, transaction, cancellationToken: cancellationToken);
    }

    private sealed record Prepared(
        string? ClientName,
        string? SiteName,
        int CategoryHours,
        string? CategoryName,
        string? SectionName,
        string? SubsectionName,
        string? EngineerName,
        LondonJobTimes Times,
        DateTime AuditLocal);

    private sealed class SiteRow
    {
        public string? Name { get; init; }

        public string? ClientCode { get; init; }
    }

    private sealed class CategoryRow
    {
        public int? Hours { get; init; }

        public string? Name { get; init; }
    }

    private sealed class SubsectionRow
    {
        public string? Name { get; init; }

        public string? SectionCode { get; init; }
    }

    private sealed class OrderRow
    {
        public int JobNumber { get; init; }

        public string? ClientCode { get; init; }

        public string? SiteCode { get; init; }

        public int? CategoryCode { get; init; }

        public string? OrderNumber { get; init; }

        public string? DescriptionText { get; init; }

        public string? ContactName { get; init; }

        public string? ReportedBy { get; init; }

        public string? Username { get; init; }

        public string? CallTakenBy { get; init; }

        public int? JobType { get; init; }

        public string? ReceivedText { get; init; }

        public string? DeadlineText { get; init; }

        public string? ProjectManager { get; init; }

        public string? Coordinator { get; init; }

        public decimal? Margin { get; init; }
    }

    private sealed class SectionRow
    {
        public int JobNumber { get; init; }

        public string? SubsectionCode { get; init; }
    }

    private sealed class LogRow
    {
        public int JobNumber { get; init; }

        public string? FormName { get; init; }

        public string? LoggedUser { get; init; }

        public string? Cname { get; init; }
    }
}
