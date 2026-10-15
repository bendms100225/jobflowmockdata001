namespace JobFlowOOH.Application.Exceptions;

public sealed class JobCommandValidationException : Exception
{
    public JobCommandValidationException(IReadOnlyList<string> errors)
        : base("The job command is not valid.")
    {
        Errors = errors;
    }

    public IReadOnlyList<string> Errors { get; }
}
