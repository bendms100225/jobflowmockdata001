using System.Globalization;
using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using JobFlowOOH.Infrastructure.Portal;
using JobFlowOOH.JobFlow.Data;
using JobFlowOOH.JobFlow.Readers;
using JobFlowOOH.JobFlow.Writing.Experimental;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.IntegrationTests;

public sealed class PocWriteFixture : IAsyncLifetime
{
    public string ExperimentalConnection { get; private set; } = "";

    public ExperimentalDirectSqlJobFlowJobWriter Writer { get; private set; } = null!;

    public IJobFlowPocJobReader Reader { get; private set; } = null!;

    public JobFlowWritesOptions Writes { get; private set; } = null!;

    public string ClientCode { get; private set; } = "";

    public string SiteCode { get; private set; } = "";

    public string OtherSiteCode { get; private set; } = "";

    public int EngineerId { get; private set; }

    public string EngineerName { get; private set; } = "";

    public IJobFlowEngineerReader Engineers { get; private set; } = null!;

    public IPortalIdempotencyStore Store { get; private set; } = null!;

    public int SuccessJobNumber { get; private set; }

    public PocJobReceipt SuccessReceipt { get; private set; } = null!;

    public CreateJobCommand SuccessCommand { get; private set; } = null!;

    public IReadOnlyDictionary<string, int> UntouchedCounts { get; private set; } = new Dictionary<string, int>();

    public async Task InitializeAsync()
    {
        var web = FindWebProject();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(web)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .AddUserSecrets<PocJobWriteTests>(optional: true)
            .Build();
        ExperimentalConnection = configuration.GetConnectionString("JobFlowExperimental")
            ?? throw new InvalidOperationException("JobFlowExperimental is not configured.");
        var portal = configuration.GetConnectionString("PortalDatabase")
            ?? throw new InvalidOperationException("PortalDatabase is not configured.");
        Writes = new JobFlowWritesOptions
        {
            Mode = configuration["JobFlowWrites:Mode"] ?? "",
            AllowDevelopmentDatabaseMutation = bool.TryParse(configuration["JobFlowWrites:AllowDevelopmentDatabaseMutation"], out var allow) && allow,
            DevelopmentUsername = configuration["JobFlowWrites:DevelopmentUsername"] ?? "",
            DevelopmentCallTakenBy = configuration["JobFlowWrites:DevelopmentCallTakenBy"] ?? "",
            DevelopmentSectionId = configuration["JobFlowWrites:DevelopmentSectionId"] ?? "",
            DevelopmentSubsectionId = configuration["JobFlowWrites:DevelopmentSubsectionId"] ?? "",
            DevelopmentJobType = int.TryParse(configuration["JobFlowWrites:DevelopmentJobType"], NumberStyles.Integer, CultureInfo.InvariantCulture, out var jobType) ? jobType : null
        };
        if (string.IsNullOrWhiteSpace(Writes.DevelopmentUsername) || string.IsNullOrWhiteSpace(Writes.DevelopmentCallTakenBy))
        {
            throw new InvalidOperationException("Set JobFlowWrites:DevelopmentUsername and DevelopmentCallTakenBy in user-secrets before the POC write tests.");
        }

        var portalOptions = new PortalDatabaseOptions { ConnectionString = portal };
        await new PortalDatabaseInitializer(portalOptions).EnsureAsync();
        var store = new PortalIdempotencyStore(portalOptions);
        Store = store;
        var jobFlow = Options.Create(new JobFlowOptions());
        var readSql = new JobFlowSql(new JobFlowReadOnlyConnectionFactory(configuration));
        Reader = new JobFlowPocJobReader(readSql);
        Engineers = new JobFlowEngineerReader(readSql);
        Writer = CreateWriter(true, ExperimentalConnection, Writes, store, jobFlow);

        UntouchedCounts = new Dictionary<string, int>
        {
            ["status"] = await ScalarAsync("SELECT COUNT(*) FROM dbo.status"),
            ["jobplan"] = await ScalarAsync("SELECT COUNT(*) FROM dbo.jobplan"),
            ["allocation"] = await ScalarAsync("SELECT COUNT(*) FROM dbo.allocation")
        };

        await using var connection = new SqlConnection(ExperimentalConnection);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT TOP (1) c.clientcode, h.sitecode
            FROM dbo.client AS c
            INNER JOIN dbo.houselist AS h ON h.clientcode = c.clientcode
            ORDER BY c.clientcode, h.sitecode
            """;
        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (!await reader.ReadAsync())
            {
                throw new InvalidOperationException("JobFlowDemo has no client with a site.");
            }

            ClientCode = reader.GetString(0);
            SiteCode = reader.GetString(1);
        }

        OtherSiteCode = await TextAsync(
            "SELECT TOP (1) sitecode FROM dbo.houselist WHERE clientcode <> @ClientCode ORDER BY sitecode",
            ("@ClientCode", ClientCode)) ?? throw new InvalidOperationException("JobFlowDemo has no site for another client.");
        EngineerId = await ScalarAsync(
            """
            SELECT TOP (1) e.idno
            FROM dbo.employee AS e
            WHERE LTRIM(RTRIM(e.name)) <> ''
              AND EXISTS (SELECT 1 FROM dbo.operative AS o WHERE o.idno = e.idno)
            ORDER BY LTRIM(RTRIM(e.name)), e.idno
            """);
        EngineerName = (await TextAsync(
            "SELECT LTRIM(RTRIM(name)) FROM dbo.employee WHERE idno = @EmployeeId",
            ("@EmployeeId", EngineerId)))?.Trim()
            ?? throw new InvalidOperationException("The selectable employee has no name.");
        var related = await TextAsync(
            "SELECT scode FROM dbo.subsection WHERE sscode = @SubsectionCode",
            ("@SubsectionCode", Writes.DevelopmentSubsectionId));
        if (!string.Equals(related, Writes.DevelopmentSectionId, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The configured development section and subsection are not related.");
        }

        SuccessCommand = Build(Marker("success"), OrderMarker("success"));
        var created = await Writer.CreateAsync(SuccessCommand, CancellationToken.None);
        SuccessJobNumber = created.Receipt!.JobNumber;
        SuccessReceipt = created.Receipt;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public ExperimentalDirectSqlJobFlowJobWriter CreateWriter(
        bool isDevelopment,
        string connectionString,
        JobFlowWritesOptions? writes = null,
        PortalIdempotencyStore? store = null,
        IOptions<JobFlowOptions>? jobFlow = null)
    {
        return new ExperimentalDirectSqlJobFlowJobWriter(
            connectionString,
            Options.Create(writes ?? Writes),
            jobFlow ?? Options.Create(new JobFlowOptions()),
            store ?? new PortalIdempotencyStore(new PortalDatabaseOptions()),
            Reader,
            Engineers,
            new CreateJobCommandValidator(),
            TimeProvider.System,
            NullLogger<ExperimentalDirectSqlJobFlowJobWriter>.Instance,
            isDevelopment);
    }

    public CreateJobCommand Build(
        string descriptionBody,
        string orderBody,
        string? key = null,
        string? clientCode = null,
        string? siteCode = null,
        int categoryCode = 2,
        string? sectionCode = null,
        string? subsectionCode = null,
        int? requestedEngineerEmployeeId = null)
    {
        return new CreateJobCommand
        {
            ClientCode = clientCode ?? ClientCode,
            SiteCode = siteCode ?? SiteCode,
            Description = descriptionBody.StartsWith(PocMarkers.DescriptionPrefix, StringComparison.Ordinal)
                ? descriptionBody
                : PocMarkers.TestDescriptionPrefix + descriptionBody,
            OrderNumber = orderBody.StartsWith(PocMarkers.OrderPrefix, StringComparison.Ordinal)
                ? orderBody
                : PocMarkers.TestOrderPrefix + orderBody,
            ReportedBy = "POC reported by",
            SiteContactName = "POC site contact",
            ReceivedAt = new DateTimeOffset(2026, 10, 15, 18, 0, 0, TimeSpan.Zero),
            ResponseCategoryCode = categoryCode,
            RequestedEngineerEmployeeId = requestedEngineerEmployeeId ?? EngineerId,
            SectionCode = sectionCode ?? Writes.DevelopmentSectionId,
            SubsectionCode = subsectionCode ?? Writes.DevelopmentSubsectionId,
            IdempotencyKey = key ?? Guid.NewGuid().ToString("N")
        };
    }

    public async Task<int> ScalarAsync(string sql, params (string Name, object Value)[] parameters)
    {
        var value = await ScalarObjectAsync(sql, parameters);
        return Convert.ToInt32(value, CultureInfo.InvariantCulture);
    }

    public async Task<string?> TextAsync(string sql, params (string Name, object Value)[] parameters)
    {
        var value = await ScalarObjectAsync(sql, parameters);
        return value is null or DBNull ? null : Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    public static string Marker(string name)
    {
        return name + "-" + Guid.NewGuid().ToString("N")[..8];
    }

    public static string OrderMarker(string name)
    {
        return name + "-" + Guid.NewGuid().ToString("N")[..8];
    }

    private async Task<object?> ScalarObjectAsync(string sql, params (string Name, object Value)[] parameters)
    {
        await using var connection = new SqlConnection(ExperimentalConnection);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value);
        }

        return await command.ExecuteScalarAsync();
    }

    private static string FindWebProject()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "src", "JobFlowOOH.Web");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not find the web project.");
    }
}

public class PocJobWriteTests : IClassFixture<PocWriteFixture>
{
    private readonly PocWriteFixture _fixture;

    public PocJobWriteTests(PocWriteFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Create_GeneratesAJobNumber()
    {
        Assert.True(_fixture.SuccessJobNumber > 0);
        Assert.DoesNotContain(_fixture.SuccessJobNumber.ToString(CultureInfo.InvariantCulture), _fixture.SuccessCommand.Description, StringComparison.Ordinal);
    }

    [Fact]
    public async Task OrdersRow_MatchesTheSubmittedJob()
    {
        var hours = await _fixture.ScalarAsync(
            "SELECT category FROM dbo.category WHERE categorycode = @CategoryCode",
            ("@CategoryCode", _fixture.SuccessCommand.ResponseCategoryCode));
        var times = LondonJobClock.Calculate(_fixture.SuccessCommand.ReceivedAt, hours, "Europe/London");
        var description = await _fixture.TextAsync(
            "SELECT CAST(description AS varchar(4000)) FROM dbo.orders WHERE jobno = @JobNumber",
            ("@JobNumber", _fixture.SuccessJobNumber));
        var orderNumber = await _fixture.TextAsync(
            "SELECT orderno FROM dbo.orders WHERE jobno = @JobNumber",
            ("@JobNumber", _fixture.SuccessJobNumber));
        var client = await _fixture.TextAsync("SELECT clientcode FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var site = await _fixture.TextAsync("SELECT sitecode FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var category = await _fixture.ScalarAsync("SELECT categorycode FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var deadline = await _fixture.TextAsync("SELECT CONVERT(varchar(19), deadline, 120) FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var received = await _fixture.TextAsync("SELECT CONVERT(varchar(19), [date], 120) FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var contact = await _fixture.TextAsync("SELECT contactname FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var reportedBy = await _fixture.TextAsync("SELECT rpby FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var username = await _fixture.TextAsync("SELECT username FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));
        var projectManager = await _fixture.TextAsync("SELECT pmanager FROM dbo.orders WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber));

        Assert.Equal(_fixture.SuccessCommand.Description, description);
        Assert.StartsWith(PocMarkers.TestDescriptionPrefix, description, StringComparison.Ordinal);
        Assert.Equal(_fixture.SuccessCommand.OrderNumber, orderNumber);
        Assert.StartsWith(PocMarkers.TestOrderPrefix, orderNumber, StringComparison.Ordinal);
        Assert.Equal(_fixture.SuccessCommand.ClientCode, client);
        Assert.Equal(_fixture.SuccessCommand.SiteCode, site);
        Assert.Equal(_fixture.SuccessCommand.ResponseCategoryCode, category);
        Assert.Equal(LondonJobClock.SqlText(times.ReceivedLocal), received);
        Assert.Equal(LondonJobClock.SqlText(times.DeadlineLocal), deadline);
        Assert.Equal("POC site contact", contact);
        Assert.Equal("POC reported by", reportedBy);
        Assert.Equal(_fixture.Writes.DevelopmentUsername, username);
        Assert.NotEqual(_fixture.EngineerId.ToString(CultureInfo.InvariantCulture), username);
        Assert.NotEqual(_fixture.EngineerName, username);
        Assert.True(string.IsNullOrEmpty(projectManager));
        Assert.Equal(0, await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.status WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber)));
        Assert.Equal(0, await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.jobplan WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber)));
        Assert.Equal(0, await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.allocation WHERE jobno = @JobNumber", ("@JobNumber", _fixture.SuccessJobNumber)));
    }

    [Fact]
    public async Task SelectSection_LinksTheNewJob()
    {
        var count = await _fixture.ScalarAsync(
            "SELECT COUNT(*) FROM dbo.selectsection WHERE jobno = @JobNumber AND sscode = @SubsectionCode",
            ("@JobNumber", _fixture.SuccessJobNumber),
            ("@SubsectionCode", _fixture.SuccessCommand.SubsectionCode));

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task UserLog_HasTheOrderRow()
    {
        var count = await _fixture.ScalarAsync(
            "SELECT COUNT(*) FROM dbo.userlog WHERE jobno = @JobNumber AND formname = 'ORDER' AND luser = @Username",
            ("@JobNumber", _fixture.SuccessJobNumber),
            ("@Username", _fixture.Writes.DevelopmentUsername));

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task SelectSectionFailure_RollsBackTheOrder()
    {
        var description = PocMarkers.TestDescriptionPrefix + PocWriteFixture.Marker("section-fail");
        var exception = await Assert.ThrowsAsync<JobFlowExperimentalWriteException>(() => InsertAsync(description, selectSectionOverride: "NO-SUCH-CODE"));

        Assert.Equal(JobFlowExperimentalWriteException.SafeMessage, exception.Message);
        Assert.Equal(0, await CountDescriptionAsync(description));
    }

    [Fact]
    public async Task UserLogFailure_RollsBackTheOrder()
    {
        var description = PocMarkers.TestDescriptionPrefix + PocWriteFixture.Marker("log-fail");
        var exception = await Assert.ThrowsAsync<JobFlowExperimentalWriteException>(() => InsertAsync(description, userLogOverride: new string('X', 150)));

        Assert.Equal(JobFlowExperimentalWriteException.SafeMessage, exception.Message);
        Assert.Equal(0, await CountDescriptionAsync(description));
    }

    [Fact]
    public async Task InvalidClient_DoesNotInsert()
    {
        var command = _fixture.Build(PocWriteFixture.Marker("client"), PocWriteFixture.OrderMarker("client"), clientCode: "NO-SUCH-CLIENT");
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("Client was not found.", exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task SiteFromAnotherClient_DoesNotInsert()
    {
        var command = _fixture.Build(PocWriteFixture.Marker("site"), PocWriteFixture.OrderMarker("site"), siteCode: _fixture.OtherSiteCode);
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("Site does not belong to the selected client.", exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task InvalidCategory_DoesNotInsert()
    {
        var command = _fixture.Build(PocWriteFixture.Marker("category"), PocWriteFixture.OrderMarker("category"), categoryCode: 999);
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("Category was not found.", exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task UnrelatedSection_DoesNotInsert()
    {
        var command = _fixture.Build(PocWriteFixture.Marker("section"), PocWriteFixture.OrderMarker("section"), sectionCode: "E", subsectionCode: "16");
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("Section and subsection are not related.", exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task DuplicateKey_ReturnsTheOriginalJobNumber()
    {
        var key = Guid.NewGuid().ToString("N");
        var command = _fixture.Build(PocWriteFixture.Marker("idem"), PocWriteFixture.OrderMarker("idem"), key);
        var first = await _fixture.Writer.CreateAsync(command, CancellationToken.None);
        var second = await _fixture.Writer.CreateAsync(command, CancellationToken.None);

        Assert.Equal(first.Receipt!.JobNumber, second.Receipt!.JobNumber);
        Assert.Equal(1, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task ReusedKeyWithDifferentContent_IsRejected()
    {
        var key = Guid.NewGuid().ToString("N");
        var command = _fixture.Build(PocWriteFixture.Marker("idem-a"), PocWriteFixture.OrderMarker("idem-a"), key);
        await _fixture.Writer.CreateAsync(command, CancellationToken.None);
        var changed = _fixture.Build(PocWriteFixture.Marker("idem-b"), PocWriteFixture.OrderMarker("idem-b"), key);

        var exception = await Assert.ThrowsAsync<IdempotencyConflictException>(() => _fixture.Writer.CreateAsync(changed, CancellationToken.None));

        Assert.Contains("different content", exception.Message, StringComparison.Ordinal);
        Assert.Equal(1, await CountDescriptionAsync(command.Description));
        Assert.Equal(0, await CountDescriptionAsync(changed.Description));
    }

    [Fact]
    public async Task NonDevelopment_IsRejected()
    {
        var writer = _fixture.CreateWriter(false, _fixture.ExperimentalConnection);
        var command = _fixture.Build(PocWriteFixture.Marker("env"), PocWriteFixture.OrderMarker("env"));

        var exception = await Assert.ThrowsAsync<JobFlowExperimentalWriteException>(() => writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("outside Development", exception.Message, StringComparison.Ordinal);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task WrongDatabaseName_IsRejected()
    {
        var builder = new SqlConnectionStringBuilder(_fixture.ExperimentalConnection)
        {
            InitialCatalog = "master"
        };
        var writer = _fixture.CreateWriter(true, builder.ConnectionString);
        var command = _fixture.Build(PocWriteFixture.Marker("db"), PocWriteFixture.OrderMarker("db"));

        var exception = await Assert.ThrowsAsync<JobFlowExperimentalWriteException>(() => writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains("JobFlowDemo", exception.Message, StringComparison.Ordinal);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
    }

    [Fact]
    public async Task RequestedEngineer_ReachesTheCommandAndThePortalRow()
    {
        Assert.Equal(_fixture.EngineerId, _fixture.SuccessCommand.RequestedEngineerEmployeeId);
        Assert.Equal(_fixture.EngineerId, _fixture.SuccessReceipt.RequestedEngineerEmployeeId);
        Assert.Equal(_fixture.EngineerName, _fixture.SuccessReceipt.RequestedEngineerName);
        var stored = await _fixture.Store.FindByJobNumberAsync(_fixture.SuccessJobNumber, CancellationToken.None);
        Assert.NotNull(stored);
        Assert.Equal(_fixture.EngineerId, stored.RequestedEngineerEmployeeId);
        var callTakenBy = await _fixture.TextAsync(
            "SELECT calltakenby FROM dbo.orders WHERE jobno = @JobNumber",
            ("@JobNumber", _fixture.SuccessJobNumber));
        Assert.Equal(_fixture.Writes.DevelopmentCallTakenBy, callTakenBy);
        Assert.NotEqual(_fixture.EngineerName, callTakenBy);
    }

    [Fact]
    public async Task EmployeeOutsideTheSelectableSet_DoesNotInsert()
    {
        var excluded = await _fixture.ScalarAsync(
            """
            SELECT TOP (1) e.idno
            FROM dbo.employee AS e
            WHERE NOT EXISTS (SELECT 1 FROM dbo.operative AS o WHERE o.idno = e.idno)
            ORDER BY e.idno
            """);
        var command = _fixture.Build(
            PocWriteFixture.Marker("out"),
            PocWriteFixture.OrderMarker("out"),
            requestedEngineerEmployeeId: excluded);
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains(RequestedEngineerText.SelectValid, exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
        Assert.Equal(0, await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.allocation WHERE jobno IN (SELECT jobno FROM dbo.orders WHERE CAST(description AS varchar(4000)) = @Description)", ("@Description", command.Description)));
    }

    [Fact]
    public async Task UnknownEmployee_DoesNotInsert()
    {
        var command = _fixture.Build(
            PocWriteFixture.Marker("miss"),
            PocWriteFixture.OrderMarker("miss"),
            requestedEngineerEmployeeId: 2147483646);
        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(() => _fixture.Writer.CreateAsync(command, CancellationToken.None));

        Assert.Contains(RequestedEngineerText.SelectValid, exception.Errors);
        Assert.Equal(0, await CountDescriptionAsync(command.Description));
        Assert.Equal(0, await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.jobplan WHERE jobno IN (SELECT jobno FROM dbo.orders WHERE CAST(description AS varchar(4000)) = @Description)", ("@Description", command.Description)));
    }

    [Fact]
    public async Task StatusJobPlanAndAllocation_StayAtTheBaseline()
    {
        Assert.Equal(_fixture.UntouchedCounts["status"], await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.status"));
        Assert.Equal(_fixture.UntouchedCounts["jobplan"], await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.jobplan"));
        Assert.Equal(_fixture.UntouchedCounts["allocation"], await _fixture.ScalarAsync("SELECT COUNT(*) FROM dbo.allocation"));
        var detail = await _fixture.Reader.FindAsync(_fixture.SuccessJobNumber, CancellationToken.None);
        Assert.NotNull(detail);
        Assert.StartsWith(PocMarkers.TestDescriptionPrefix, detail.Description, StringComparison.Ordinal);
        var recent = await _fixture.Reader.ListRecentAsync(CancellationToken.None);
        Assert.DoesNotContain(recent, job => job.JobNumber == _fixture.SuccessJobNumber);
    }

    private async Task InsertAsync(string description, string? selectSectionOverride = null, string? userLogOverride = null)
    {
        var order = PocMarkers.TestOrderPrefix + PocWriteFixture.OrderMarker("rollback");
        await ExperimentalJobTransaction.ExecuteAsync(
            _fixture.ExperimentalConnection,
            new ExperimentalJobTransaction.Request(
                _fixture.ClientCode,
                _fixture.SiteCode,
                description,
                order,
                "POC reported by",
                "POC site contact",
                new DateTimeOffset(2026, 10, 15, 18, 0, 0, TimeSpan.Zero),
                2,
                _fixture.Writes.DevelopmentSectionId,
                _fixture.Writes.DevelopmentSubsectionId,
                _fixture.EngineerId,
                _fixture.Writes.DevelopmentUsername,
                _fixture.Writes.DevelopmentCallTakenBy,
                _fixture.Writes.DevelopmentJobType!.Value,
                "Europe/London",
                Guid.NewGuid(),
                selectSectionOverride,
                userLogOverride),
            TimeProvider.System,
            NullLogger.Instance,
            CancellationToken.None);
    }

    private async Task<int> CountDescriptionAsync(string description)
    {
        return await _fixture.ScalarAsync(
            "SELECT COUNT(*) FROM dbo.orders WHERE CAST(description AS varchar(4000)) = @Description",
            ("@Description", description));
    }
}
