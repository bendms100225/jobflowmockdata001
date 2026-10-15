using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;

namespace JobFlowOOH.Application.Jobs;

public sealed class JobCreationService : IJobCreationService
{
    private readonly CreateJobCommandValidator _validator;
    private readonly IJobFlowJobWriter _writer;

    public JobCreationService(CreateJobCommandValidator validator, IJobFlowJobWriter writer)
    {
        _validator = validator;
        _writer = writer;
    }

    public Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var errors = _validator.Validate(command);
        if (errors.Count > 0)
        {
            throw new JobCommandValidationException(errors);
        }

        return _writer.CreateAsync(command, cancellationToken);
    }
}
