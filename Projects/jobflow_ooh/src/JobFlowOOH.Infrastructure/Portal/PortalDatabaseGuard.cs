using Microsoft.Data.SqlClient;

namespace JobFlowOOH.Infrastructure.Portal;

public static class PortalDatabaseGuard
{
    public static void EnsureSeparate(string? jobFlowConnectionString, string? portalConnectionString)
    {
        if (string.IsNullOrWhiteSpace(jobFlowConnectionString) || string.IsNullOrWhiteSpace(portalConnectionString))
        {
            return;
        }

        var jobFlow = new SqlConnectionStringBuilder(jobFlowConnectionString);
        var portal = new SqlConnectionStringBuilder(portalConnectionString);
        var sameServer = string.Equals(NormalizeServer(jobFlow.DataSource), NormalizeServer(portal.DataSource), StringComparison.OrdinalIgnoreCase);
        var sameDatabase = string.Equals(jobFlow.InitialCatalog, portal.InitialCatalog, StringComparison.OrdinalIgnoreCase);
        if (sameServer && sameDatabase && !string.IsNullOrWhiteSpace(jobFlow.InitialCatalog))
        {
            throw new InvalidOperationException("PortalDatabase must not target the JobFlow database.");
        }
    }

    private static string NormalizeServer(string dataSource)
    {
        var value = dataSource.Trim();
        if (value.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase))
        {
            value = value[4..];
        }

        value = value.Replace("(local)", "localhost", StringComparison.OrdinalIgnoreCase);
        if (value == ".")
        {
            return "localhost";
        }

        return value;
    }
}
