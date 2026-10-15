using JobFlowOOH.Application.Jobs;
using Microsoft.Data.SqlClient;

namespace JobFlowOOH.Infrastructure.Portal;

public sealed class PortalDatabaseInitializer
{
    private readonly string _connectionString;

    public PortalDatabaseInitializer(PortalDatabaseOptions options)
    {
        _connectionString = options.ConnectionString ?? "";
    }

    public async Task EnsureAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:PortalDatabase is not configured.");
        }

        var portal = new SqlConnectionStringBuilder(_connectionString);
        if (!string.Equals(portal.InitialCatalog, PocMarkers.PortalDatabaseName, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("PortalDatabase must be the JobFlowOohPortal database.");
        }

        var master = new SqlConnectionStringBuilder(_connectionString)
        {
            InitialCatalog = "master"
        };
        await using (var connection = new SqlConnection(master.ConnectionString))
        {
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = """
                IF DB_ID(N'JobFlowOohPortal') IS NULL
                    CREATE DATABASE [JobFlowOohPortal]
                """;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var portalConnection = new SqlConnection(_connectionString);
        await portalConnection.OpenAsync(cancellationToken);
        await using var table = portalConnection.CreateCommand();
        // RequestedEngineerId stores dbo.employee.idno. There is no foreign key to JobFlowDemo.
        table.CommandText = """
            IF OBJECT_ID(N'dbo.PocIdempotency', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.PocIdempotency
                (
                    IdempotencyKey nvarchar(200) NOT NULL CONSTRAINT PK_PocIdempotency PRIMARY KEY,
                    RequestHash char(64) NOT NULL,
                    JobNumber int NULL,
                    CorrelationId uniqueidentifier NOT NULL,
                    RequestedEngineerId int NOT NULL,
                    Status nvarchar(20) NOT NULL,
                    CreatedAt datetimeoffset NOT NULL
                );
            END
            """;
        await table.ExecuteNonQueryAsync(cancellationToken);
    }
}
