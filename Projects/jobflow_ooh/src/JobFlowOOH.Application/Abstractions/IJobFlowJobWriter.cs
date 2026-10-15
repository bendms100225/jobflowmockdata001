using JobFlowOOH.Application.Jobs;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowJobWriter
{
    Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken);
}
