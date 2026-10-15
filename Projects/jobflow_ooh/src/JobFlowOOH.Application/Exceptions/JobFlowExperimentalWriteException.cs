namespace JobFlowOOH.Application.Exceptions;

public sealed class JobFlowExperimentalWriteException : Exception
{
    public const string SafeMessage = "The experimental job was not created. The database transaction was rolled back.";

    public JobFlowExperimentalWriteException(string message)
        : base(message)
    {
    }

    public JobFlowExperimentalWriteException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
