using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Domain.Sections;
using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowSectionReader : IJobFlowSectionReader
{
    private readonly JobFlowSql _sql;

    public JobFlowSectionReader(JobFlowSql sql)
    {
        _sql = sql;
    }

    public Task<IReadOnlyList<Section>> ListSectionsAsync(CancellationToken cancellationToken)
    {
        return _sql.QueryAsync<Section>(JobFlowReadSql.Sections, null, cancellationToken);
    }

    public Task<IReadOnlyList<Subsection>> ListSubsectionsAsync(CancellationToken cancellationToken)
    {
        return _sql.QueryAsync<Subsection>(JobFlowReadSql.Subsections, null, cancellationToken);
    }
}
