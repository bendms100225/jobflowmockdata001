using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Domain.Clients;
using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowClientReader : IJobFlowClientReader
{
    private readonly JobFlowSql _sql;

    public JobFlowClientReader(JobFlowSql sql)
    {
        _sql = sql;
    }

    public async Task<IReadOnlyList<Client>> ListAsync(CancellationToken cancellationToken)
    {
        return await _sql.QueryAsync<Client>(JobFlowReadSql.Clients, null, cancellationToken);
    }
}
