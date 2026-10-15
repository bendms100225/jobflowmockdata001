using Dapper;
using Microsoft.Data.SqlClient;

namespace JobFlowOOH.JobFlow.Data;

internal sealed class JobFlowSql
{
    private readonly JobFlowReadOnlyConnectionFactory _connections;

    public JobFlowSql(JobFlowReadOnlyConnectionFactory connections)
    {
        _connections = connections;
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? parameters, CancellationToken cancellationToken)
    {
        SelectOnlySql.Ensure(sql);
        await using var connection = _connections.Open();
        var rows = await connection.QueryAsync<T>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
        return rows.AsList();
    }
}
