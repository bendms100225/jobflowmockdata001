using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using JobFlowOOH.Application.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobFlowOOH.IntegrationTests;

public class EngineerSelectionPageTests : IClassFixture<WebApplicationFactory<global::Program>>
{
    private readonly WebApplicationFactory<global::Program> _factory;

    public EngineerSelectionPageTests(WebApplicationFactory<global::Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task RequestedEngineer_SurvivesReviewAndIsNotAllocated()
    {
        var engineers = await _factory.Services.GetRequiredService<IJobFlowEngineerReader>().ListAsync(CancellationToken.None);
        var engineer = engineers.First(item => item.DisplayName.IndexOfAny(['&', '<', '>', '"']) < 0);
        var reference = LoadReference();
        using var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            HandleCookies = true
        });

        var newPage = await client.GetAsync("/jobs/new");
        var newHtml = await newPage.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, newPage.StatusCode);
        Assert.Contains($"value=\"{engineer.EmployeeId.ToString(CultureInfo.InvariantCulture)}\"", newHtml, StringComparison.Ordinal);
        Assert.Contains(engineer.DisplayName, newHtml, StringComparison.Ordinal);
        Assert.Contains("name=\"RequestedEngineerEmployeeId\"", newHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("AllocatedEngineerId", newHtml, StringComparison.Ordinal);

        var missing = await client.PostAsync("/jobs/new", Fields(newHtml, reference, engineer.EmployeeId, description: ""));
        var missingHtml = await missing.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, missing.StatusCode);
        Assert.Contains("Description is required.", missingHtml, StringComparison.Ordinal);
        AssertSelected(missingHtml, engineer.EmployeeId);

        var invalidPage = await client.GetAsync("/jobs/new");
        var invalidSource = await invalidPage.Content.ReadAsStringAsync();
        var invalid = await client.PostAsync("/jobs/new", Fields(invalidSource, reference, 2147483646, description: "TEST-invalid-engineer"));
        var invalidHtml = await invalid.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, invalid.StatusCode);
        Assert.Contains("Select a valid engineer.", invalidHtml, StringComparison.Ordinal);
        Assert.Null(invalid.Headers.Location);

        var freshPage = await client.GetAsync("/jobs/new");
        var freshHtml = await freshPage.Content.ReadAsStringAsync();
        var suffix = Guid.NewGuid().ToString("N")[..8];
        var review = await client.PostAsync("/jobs/new", Fields(freshHtml, reference, engineer.EmployeeId, description: "TEST-selector-" + suffix, order: "TEST-selector-" + suffix));
        Assert.Equal(HttpStatusCode.Redirect, review.StatusCode);
        Assert.NotNull(review.Headers.Location);

        var reviewPage = await client.GetAsync(review.Headers.Location);
        var reviewHtml = await reviewPage.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, reviewPage.StatusCode);
        Assert.Contains($"<dd id=\"requested-engineer-name\">{engineer.DisplayName}</dd>", reviewHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("name=\"RequestedEngineerEmployeeId\"", reviewHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Allocated engineer", reviewHtml, StringComparison.Ordinal);

        var resumed = await client.GetAsync("/jobs/new?resume=1");
        var resumedHtml = await resumed.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, resumed.StatusCode);
        AssertSelected(resumedHtml, engineer.EmployeeId);

        var allocationBefore = await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.allocation");
        var jobPlanBefore = await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.jobplan");
        var created = await client.PostAsync("/jobs/review", ReviewFields(reviewHtml));
        Assert.Equal(HttpStatusCode.Redirect, created.StatusCode);
        Assert.NotNull(created.Headers.Location);
        var createdPage = await client.GetAsync(created.Headers.Location);
        var createdHtml = await createdPage.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, createdPage.StatusCode);
        Assert.Contains("Development JobFlow record created", createdHtml, StringComparison.Ordinal);
        Assert.Contains($"data-employee-id=\"{engineer.EmployeeId.ToString(CultureInfo.InvariantCulture)}\"", createdHtml, StringComparison.Ordinal);
        Assert.Contains($">{engineer.DisplayName}</dd>", createdHtml, StringComparison.Ordinal);
        Assert.Contains("Requested engineer, not yet assigned in JobFlow.", createdHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Allocated engineer", createdHtml, StringComparison.Ordinal);

        var jobNumber = JobNumber(created.Headers.Location!.OriginalString);
        Assert.Equal(allocationBefore, await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.allocation"));
        Assert.Equal(jobPlanBefore, await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.jobplan"));
        Assert.Equal(0, await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.allocation WHERE jobno = @JobNumber", ("@JobNumber", jobNumber)));
        Assert.Equal(0, await CountAsync(reference.ExperimentalConnection, "SELECT COUNT(*) FROM dbo.jobplan WHERE jobno = @JobNumber", ("@JobNumber", jobNumber)));
        var storedEngineer = await CountAsync(
            reference.PortalConnection,
            "SELECT RequestedEngineerId FROM dbo.PocIdempotency WHERE JobNumber = @JobNumber",
            ("@JobNumber", jobNumber));
        Assert.Equal(engineer.EmployeeId, storedEngineer);

        var diagnostics = await client.GetStringAsync("/development/diagnostics");
        Assert.Contains("Requested engineer source = dbo.employee", diagnostics, StringComparison.Ordinal);
        Assert.Contains("dbo.employee count", diagnostics, StringComparison.Ordinal);
        Assert.Contains("<td>" + engineers[0].EmployeeId.ToString(CultureInfo.InvariantCulture) + "</td>", diagnostics, StringComparison.Ordinal);
        Assert.Contains("<td>" + engineers[0].DisplayName + "</td>", diagnostics, StringComparison.Ordinal);
        Assert.Contains(">none<", diagnostics, StringComparison.Ordinal);
    }

    private static void AssertSelected(string html, int employeeId)
    {
        var token = employeeId.ToString(CultureInfo.InvariantCulture);
        Assert.True(
            html.Contains($"selected=\"selected\" value=\"{token}\"", StringComparison.Ordinal)
            || html.Contains($"value=\"{token}\" selected=\"selected\"", StringComparison.Ordinal),
            $"Employee {token} was not selected.");
    }

    private static FormUrlEncodedContent Fields(string html, ReferenceData reference, int employeeId, string description, string? order = null)
    {
        return new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = Input(html, "__RequestVerificationToken"),
            ["IdempotencyKey"] = Input(html, "IdempotencyKey"),
            ["ClientCode"] = reference.ClientCode,
            ["SiteCode"] = reference.SiteCode,
            ["Description"] = description,
            ["OrderNumber"] = order ?? "TEST-selector",
            ["ReportedBy"] = "POC reported by",
            ["SiteContactName"] = "POC site contact",
            ["ReceivedLocal"] = "2026-10-15T18:00",
            ["ResponseCategoryCode"] = "2",
            ["SectionCode"] = reference.SectionCode,
            ["SubsectionCode"] = reference.SubsectionCode,
            ["RequestedEngineerEmployeeId"] = employeeId.ToString(CultureInfo.InvariantCulture)
        });
    }

    private static FormUrlEncodedContent ReviewFields(string html)
    {
        return new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = Input(html, "__RequestVerificationToken"),
            ["ConfirmExperimentalWrite"] = "true"
        });
    }

    private static string Input(string html, string name)
    {
        var tag = Regex.Match(html, $"<input\\b[^>]*name=\"{Regex.Escape(name)}\"[^>]*>", RegexOptions.IgnoreCase);
        if (!tag.Success)
        {
            throw new InvalidOperationException($"The page did not contain {name}.");
        }

        var value = Regex.Match(tag.Value, "value=\"([^\"]*)\"", RegexOptions.IgnoreCase);
        if (!value.Success)
        {
            throw new InvalidOperationException($"The page did not contain a value for {name}.");
        }

        return WebUtility.HtmlDecode(value.Groups[1].Value);
    }

    private static int JobNumber(string location)
    {
        var match = Regex.Match(location, @"/(\d+)$");
        Assert.True(match.Success, location);
        return int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
    }

    private static ReferenceData LoadReference()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(FindWebProject())
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();
        var experimental = configuration.GetConnectionString("JobFlowExperimental")
            ?? throw new InvalidOperationException("JobFlowExperimental is not configured.");
        var portal = configuration.GetConnectionString("PortalDatabase")
            ?? throw new InvalidOperationException("PortalDatabase is not configured.");
        using var connection = new SqlConnection(experimental);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT TOP (1) c.clientcode, h.sitecode
            FROM dbo.client AS c
            INNER JOIN dbo.houselist AS h ON h.clientcode = c.clientcode
            ORDER BY c.clientcode, h.sitecode
            """;
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("JobFlowDemo has no client with a site.");
        }

        return new ReferenceData(
            experimental,
            portal,
            reader.GetString(0),
            reader.GetString(1),
            configuration["JobFlowWrites:DevelopmentSectionId"] ?? "",
            configuration["JobFlowWrites:DevelopmentSubsectionId"] ?? "");
    }

    private static async Task<int> CountAsync(string connectionString, string sql, params (string Name, object Value)[] parameters)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value);
        }

        return Convert.ToInt32(await command.ExecuteScalarAsync(), CultureInfo.InvariantCulture);
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

    private sealed record ReferenceData(
        string ExperimentalConnection,
        string PortalConnection,
        string ClientCode,
        string SiteCode,
        string SectionCode,
        string SubsectionCode);
}
