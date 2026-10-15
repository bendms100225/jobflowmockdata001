using JobFlowOOH.Application.Jobs;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowPocJobReader
{
    Task<IReadOnlyList<PocJobSummary>> ListRecentAsync(CancellationToken cancellationToken);

    Task<PocJobDetail?> FindAsync(int jobNumber, CancellationToken cancellationToken);
}
