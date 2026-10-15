using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.JobFlow.Writing.Experimental;

public sealed class ExperimentalDirectSqlJobFlowJobWriter : IJobFlowJobWriter
{
    private readonly string _connectionString;
    private readonly JobFlowWritesOptions _writes;
    private readonly JobFlowOptions _jobFlow;
    private readonly IPortalIdempotencyStore _store;
    private readonly IJobFlowPocJobReader _jobs;
    private readonly IJobFlowEngineerReader _engineers;
    private readonly CreateJobCommandValidator _validator;
    private readonly TimeProvider _clock;
    private readonly ILogger<ExperimentalDirectSqlJobFlowJobWriter> _logger;
    private readonly bool _isDevelopment;

    public ExperimentalDirectSqlJobFlowJobWriter(
        string? connectionString,
        IOptions<JobFlowWritesOptions> writes,
        IOptions<JobFlowOptions> jobFlow,
        IPortalIdempotencyStore store,
        IJobFlowPocJobReader jobs,
        IJobFlowEngineerReader engineers,
        CreateJobCommandValidator validator,
        TimeProvider clock,
        ILogger<ExperimentalDirectSqlJobFlowJobWriter> logger,
        bool isDevelopment)
    {
        _connectionString = connectionString ?? "";
        _writes = writes.Value;
        _jobFlow = jobFlow.Value;
        _store = store;
        _jobs = jobs;
        _engineers = engineers;
        _validator = validator;
        _clock = clock;
        _logger = logger;
        _isDevelopment = isDevelopment;
    }

    public async Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ExperimentalWriteGuard.EnsureRegistered(_isDevelopment, _writes, _connectionString);
        var errors = _validator.Validate(command);
        if (errors.Count > 0)
        {
            throw new JobCommandValidationException(errors);
        }

        var username = _writes.DevelopmentUsername.Trim();
        var callTakenBy = _writes.DevelopmentCallTakenBy.Trim();
        var jobType = _writes.DevelopmentJobType!.Value;
        var hash = IdempotencyHasher.Hash(command, username, callTakenBy, jobType);
        var existing = await _store.FindAsync(command.IdempotencyKey.Trim(), cancellationToken);
        if (existing is not null)
        {
            return await ResolveExistingAsync(existing, hash, cancellationToken);
        }

        var correlationId = Guid.NewGuid();
        var began = await _store.TryBeginAsync(
            new PortalIdempotencyDraft(command.IdempotencyKey.Trim(), hash, correlationId, command.RequestedEngineerEmployeeId),
            cancellationToken);
        if (!began)
        {
            existing = await _store.FindAsync(command.IdempotencyKey.Trim(), cancellationToken);
            if (existing is null)
            {
                throw new IdempotencyConflictException("This submission is already in progress.");
            }

            return await ResolveExistingAsync(existing, hash, cancellationToken);
        }

        ExperimentalJobTransaction.Result inserted;
        try
        {
            inserted = await ExperimentalJobTransaction.ExecuteAsync(
                _connectionString,
                new ExperimentalJobTransaction.Request(
                    command.ClientCode.Trim(),
                    command.SiteCode.Trim(),
                    command.Description.Trim(),
                    command.OrderNumber.Trim(),
                    command.ReportedBy.Trim(),
                    command.SiteContactName.Trim(),
                    command.ReceivedAt,
                    command.ResponseCategoryCode,
                    command.SectionCode.Trim(),
                    command.SubsectionCode.Trim(),
                    command.RequestedEngineerEmployeeId,
                    username,
                    callTakenBy,
                    jobType,
                    _jobFlow.DatabaseTimeZone,
                    correlationId),
                _clock,
                _logger,
                cancellationToken);
        }
        catch (Exception exception)
        {
            await _store.AbandonAsync(command.IdempotencyKey.Trim(), cancellationToken);
            _logger.LogError(exception, "Experimental job create failed. Correlation {CorrelationId}.", correlationId);
            throw;
        }

        try
        {
            await _store.CompleteAsync(command.IdempotencyKey.Trim(), inserted.JobNumber, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "JobFlowDemo job {JobNumber} was created, but the portal submission record was not saved. Correlation {CorrelationId}.",
                inserted.JobNumber,
                correlationId);
            throw new JobFlowExperimentalWriteException(
                $"JobFlowDemo job {inserted.JobNumber} was created, but the portal submission record could not be saved. Do not submit this key again.",
                exception);
        }

        return ResultFrom(inserted, correlationId, command);
    }

    private async Task<JobWriteResult> ResolveExistingAsync(
        PortalIdempotencyRecord existing,
        string hash,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(existing.RequestHash, hash, StringComparison.Ordinal))
        {
            throw new IdempotencyConflictException("This submission key was already used for different content.");
        }

        if (existing.Status == "Completed" && existing.JobNumber is int jobNumber)
        {
            var detail = await _jobs.FindAsync(jobNumber, cancellationToken);
            if (detail is null)
            {
                throw new JobFlowExperimentalWriteException($"JobFlowDemo job {jobNumber} is recorded for this submission key, but it could not be read back.");
            }

            _logger.LogInformation(
                "Returned the original experimental JobFlowDemo job {JobNumber} for a repeated submission. Correlation {CorrelationId}.",
                jobNumber,
                existing.CorrelationId);
            return await ResultFromDetailAsync(detail, existing, cancellationToken);
        }

        throw new IdempotencyConflictException("This submission is already in progress.");
    }

    private static JobWriteResult ResultFrom(
        ExperimentalJobTransaction.Result inserted,
        Guid correlationId,
        CreateJobCommand command)
    {
        var receipt = new PocJobReceipt(
            inserted.JobNumber,
            correlationId.ToString("D"),
            command.ClientCode.Trim(),
            inserted.ClientName,
            command.SiteCode.Trim(),
            inserted.SiteName,
            command.OrderNumber.Trim(),
            command.Description.Trim(),
            command.ResponseCategoryCode,
            inserted.CategoryHours,
            inserted.CategoryName,
            inserted.ReceivedAt,
            inserted.Deadline,
            RequestedEngineerText.NotAssigned,
            command.SectionCode.Trim(),
            inserted.SectionName,
            command.SubsectionCode.Trim(),
            inserted.SubsectionName,
            inserted.JobType,
            command.SiteContactName.Trim(),
            command.ReportedBy.Trim(),
            command.RequestedEngineerEmployeeId,
            inserted.EngineerName);
        return new JobWriteResult(
            inserted.JobNumber.ToString(System.Globalization.CultureInfo.InvariantCulture),
            false,
            $"Development JobFlow record created. JobFlowDemo job number {inserted.JobNumber}. This experimental record is not a production JobFlow integration.",
            receipt);
    }

    private async Task<JobWriteResult> ResultFromDetailAsync(
        PocJobDetail detail,
        PortalIdempotencyRecord existing,
        CancellationToken cancellationToken)
    {
        var engineer = await _engineers.GetEngineerByEmployeeIdAsync(existing.RequestedEngineerEmployeeId, cancellationToken);
        var receipt = new PocJobReceipt(
            detail.JobNumber,
            existing.CorrelationId.ToString("D"),
            detail.ClientCode,
            detail.ClientName,
            detail.SiteCode,
            detail.SiteName,
            detail.OrderNumber,
            detail.Description,
            detail.CategoryCode ?? 0,
            detail.CategoryHours ?? 0,
            detail.CategoryName,
            ParseLondon(detail.ReceivedText, _jobFlow.DatabaseTimeZone),
            ParseLondon(detail.DeadlineText, _jobFlow.DatabaseTimeZone),
            RequestedEngineerText.NotAssigned,
            detail.SectionCode ?? "",
            detail.SectionName,
            detail.SubsectionCode ?? "",
            detail.SubsectionName,
            detail.JobType ?? 0,
            detail.SiteContactName ?? "",
            detail.ReportedBy ?? "",
            existing.RequestedEngineerEmployeeId,
            engineer?.DisplayName);
        return new JobWriteResult(
            detail.JobNumber.ToString(System.Globalization.CultureInfo.InvariantCulture),
            false,
            $"Development JobFlow record created. JobFlowDemo job number {detail.JobNumber}. This experimental record is not a production JobFlow integration.",
            receipt);
    }

    private static DateTimeOffset ParseLondon(string text, string timeZoneId)
    {
        var local = DateTime.ParseExact(
            text,
            "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None);
        var unspecified = DateTime.SpecifyKind(local, DateTimeKind.Unspecified);
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return new DateTimeOffset(unspecified, zone.GetUtcOffset(unspecified));
    }
}
