using System.Globalization;
using JobFlowOOH.JobFlow.Data;
using JobFlowOOH.JobFlow.Readers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JobFlowOOH.IntegrationTests;

public class EmployeeReaderTests
{
    [Fact]
    public async Task SelectableEngineers_UseTheEmployeeIdAndDisplayName()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(FindWebProject())
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();
        var connectionString = configuration.GetConnectionString("JobFlowReadOnly")
            ?? throw new InvalidOperationException("JobFlowReadOnly is not configured.");
        var reader = new JobFlowEngineerReader(new JobFlowSql(new JobFlowReadOnlyConnectionFactory(configuration)));

        var engineers = await reader.ListAsync(CancellationToken.None);
        var employeeCount = await reader.CountEmployeesAsync(CancellationToken.None);
        var databaseCount = await ScalarAsync(connectionString, "SELECT COUNT(*) FROM dbo.employee");
        var selectableCount = await ScalarAsync(
            connectionString,
            """
            SELECT COUNT(*)
            FROM dbo.employee AS e
            WHERE LTRIM(RTRIM(e.name)) <> ''
              AND EXISTS (SELECT 1 FROM dbo.operative AS o WHERE o.idno = e.idno)
            """);
        var firstId = await ScalarAsync(
            connectionString,
            """
            SELECT TOP (1) e.idno
            FROM dbo.employee AS e
            WHERE LTRIM(RTRIM(e.name)) <> ''
              AND EXISTS (SELECT 1 FROM dbo.operative AS o WHERE o.idno = e.idno)
            ORDER BY LTRIM(RTRIM(e.name)), e.idno
            """);

        Assert.Equal(databaseCount, employeeCount);
        Assert.NotEmpty(engineers);
        Assert.Equal(selectableCount, engineers.Count);
        Assert.Equal(firstId, engineers[0].EmployeeId);
        Assert.All(engineers, engineer => Assert.False(string.IsNullOrWhiteSpace(engineer.DisplayName)));

        foreach (var engineer in engineers)
        {
            var name = await TextAsync(
                connectionString,
                "SELECT LTRIM(RTRIM(name)) FROM dbo.employee WHERE idno = @EmployeeId",
                ("@EmployeeId", engineer.EmployeeId));
            var operativeRows = await ScalarAsync(
                connectionString,
                "SELECT COUNT(*) FROM dbo.operative WHERE idno = @EmployeeId",
                ("@EmployeeId", engineer.EmployeeId));
            Assert.Equal(name, engineer.DisplayName);
            Assert.True(operativeRows > 0);
            var again = await reader.GetEngineerByEmployeeIdAsync(engineer.EmployeeId, CancellationToken.None);
            Assert.NotNull(again);
            Assert.Equal(engineer.EmployeeId, again.EmployeeId);
            Assert.Equal(engineer.DisplayName, again.DisplayName);
        }

        var excluded = await ScalarAsync(
            connectionString,
            """
            SELECT TOP (1) e.idno
            FROM dbo.employee AS e
            WHERE NOT EXISTS (SELECT 1 FROM dbo.operative AS o WHERE o.idno = e.idno)
            ORDER BY e.idno
            """);
        Assert.Null(await reader.GetEngineerByEmployeeIdAsync(excluded, CancellationToken.None));
        Assert.Null(await reader.GetEngineerByEmployeeIdAsync(0, CancellationToken.None));
        Assert.Null(await reader.GetEngineerByEmployeeIdAsync(2147483646, CancellationToken.None));
        Assert.DoesNotContain(engineers, engineer => engineer.EmployeeId == excluded);
    }

    private static async Task<int> ScalarAsync(string connectionString, string sql, params (string Name, object Value)[] parameters)
    {
        var value = await ScalarObjectAsync(connectionString, sql, parameters);
        return Convert.ToInt32(value, CultureInfo.InvariantCulture);
    }

    private static async Task<string?> TextAsync(string connectionString, string sql, params (string Name, object Value)[] parameters)
    {
        var value = await ScalarObjectAsync(connectionString, sql, parameters);
        return value is null or DBNull ? null : Convert.ToString(value, CultureInfo.InvariantCulture)?.Trim();
    }

    private static async Task<object?> ScalarObjectAsync(string connectionString, string sql, params (string Name, object Value)[] parameters)
    {
        await using var connection = new SqlConnection(connectionString);
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
