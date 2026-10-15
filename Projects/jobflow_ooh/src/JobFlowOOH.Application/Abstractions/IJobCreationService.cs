using JobFlowOOH.Application.Jobs;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobCreationService
{
    Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken);
}
