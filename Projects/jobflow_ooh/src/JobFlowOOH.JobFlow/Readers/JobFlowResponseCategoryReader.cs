using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Domain.ResponseCategories;
using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowResponseCategoryReader : IJobFlowResponseCategoryReader
{
    private readonly JobFlowSql _sql;

    public JobFlowResponseCategoryReader(JobFlowSql sql)
    {
        _sql = sql;
    }

    public Task<IReadOnlyList<ResponseCategory>> ListAsync(CancellationToken cancellationToken)
    {
        return _sql.QueryAsync<ResponseCategory>(JobFlowReadSql.ResponseCategories, null, cancellationToken);
    }
}
