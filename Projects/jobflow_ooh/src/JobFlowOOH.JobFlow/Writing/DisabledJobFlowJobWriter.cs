using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using Microsoft.Extensions.Logging;

namespace JobFlowOOH.JobFlow.Writing;

public sealed class DisabledJobFlowJobWriter : IJobFlowJobWriter
{
    private readonly ILogger<DisabledJobFlowJobWriter> _logger;

    public DisabledJobFlowJobWriter(ILogger<DisabledJobFlowJobWriter> logger)
    {
        _logger = logger;
    }

    public Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        _logger.LogWarning("Rejected a job create request because JobFlow writes are disabled.");
        throw new JobFlowWriteNotConfiguredException();
    }
}
