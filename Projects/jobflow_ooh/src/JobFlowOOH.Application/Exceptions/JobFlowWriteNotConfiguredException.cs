namespace JobFlowOOH.Application.Exceptions;

public sealed class JobFlowWriteNotConfiguredException : InvalidOperationException
{
    public JobFlowWriteNotConfiguredException()
        : base("JobFlow writes are disabled. The portal did not change JobFlow data.")
    {
    }
}
