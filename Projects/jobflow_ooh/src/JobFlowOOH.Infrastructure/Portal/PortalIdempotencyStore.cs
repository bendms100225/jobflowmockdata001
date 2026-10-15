using JobFlowOOH.Application.Abstractions;
using Microsoft.Data.SqlClient;

namespace JobFlowOOH.Infrastructure.Portal;

public sealed class PortalIdempotencyStore : IPortalIdempotencyStore
{
    public const string Pending = "Pending";

    public const string Completed = "Completed";

    private readonly string _connectionString;

    public PortalIdempotencyStore(PortalDatabaseOptions options)
    {
        _connectionString = options.ConnectionString ?? "";
    }

    public async Task<PortalIdempotencyRecord?> FindAsync(string key, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT IdempotencyKey, RequestHash, JobNumber, CorrelationId, RequestedEngineerId, Status
            FROM dbo.PocIdempotency
            WHERE IdempotencyKey = @Key
            """;
        return await ReadOneAsync(sql, command => Add(command, "@Key", key), cancellationToken);
    }

    public async Task<PortalIdempotencyRecord?> FindByJobNumberAsync(int jobNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT TOP (1) IdempotencyKey, RequestHash, JobNumber, CorrelationId, RequestedEngineerId, Status
            FROM dbo.PocIdempotency
            WHERE JobNumber = @JobNumber
            ORDER BY CreatedAt DESC
            """;
        return await ReadOneAsync(sql, command => Add(command, "@JobNumber", jobNumber), cancellationToken);
    }

    public async Task<bool> TryBeginAsync(PortalIdempotencyDraft draft, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(draft);
        const string sql = """
            INSERT INTO dbo.PocIdempotency
            (
                IdempotencyKey,
                RequestHash,
                JobNumber,
                CorrelationId,
                RequestedEngineerId,
                Status,
                CreatedAt
            )
            VALUES
            (
                @Key,
                @RequestHash,
                NULL,
                @CorrelationId,
                @RequestedEngineerId,
                @Status,
                @CreatedAt
            )
            """;
        try
        {
            await ExecuteAsync(
                sql,
                command =>
                {
                    Add(command, "@Key", draft.Key);
                    Add(command, "@RequestHash", draft.RequestHash);
                    Add(command, "@CorrelationId", draft.CorrelationId);
                    Add(command, "@RequestedEngineerId", draft.RequestedEngineerEmployeeId);
                    Add(command, "@Status", Pending);
                    Add(command, "@CreatedAt", DateTimeOffset.UtcNow);
                },
                cancellationToken);
            return true;
        }
        catch (SqlException exception) when (exception.Number is 2627 or 2601)
        {
            return false;
        }
    }

    public async Task CompleteAsync(string key, int jobNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE dbo.PocIdempotency
            SET JobNumber = @JobNumber, Status = @Status
            WHERE IdempotencyKey = @Key AND Status = @Pending
            """;
        var count = await ExecuteAsync(
            sql,
            command =>
            {
                Add(command, "@JobNumber", jobNumber);
                Add(command, "@Status", Completed);
                Add(command, "@Key", key);
                Add(command, "@Pending", Pending);
            },
            cancellationToken);
        if (count != 1)
        {
            throw new InvalidOperationException("The portal submission record could not be completed.");
        }
    }

    public async Task AbandonAsync(string key, CancellationToken cancellationToken)
    {
        const string sql = """
            DELETE FROM dbo.PocIdempotency
            WHERE IdempotencyKey = @Key AND Status = @Pending
            """;
        await ExecuteAsync(
            sql,
            command =>
            {
                Add(command, "@Key", key);
                Add(command, "@Pending", Pending);
            },
            cancellationToken);
    }

    private async Task<PortalIdempotencyRecord?> ReadOneAsync(
        string sql,
        Action<SqlCommand> bind,
        CancellationToken cancellationToken)
    {
        await using var connection = Open();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        bind(command);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new PortalIdempotencyRecord(
            reader.GetString(0),
            reader.GetString(1).Trim(),
            reader.IsDBNull(2) ? null : reader.GetInt32(2),
            reader.GetGuid(3),
            reader.GetInt32(4),
            reader.GetString(5));
    }

    private async Task<int> ExecuteAsync(string sql, Action<SqlCommand> bind, CancellationToken cancellationToken)
    {
        await using var connection = Open();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        bind(command);
        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private SqlConnection Open()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:PortalDatabase is not configured.");
        }

        return new SqlConnection(_connectionString);
    }

    private static void Add(SqlCommand command, string name, object value)
    {
        command.Parameters.AddWithValue(name, value);
    }
}
