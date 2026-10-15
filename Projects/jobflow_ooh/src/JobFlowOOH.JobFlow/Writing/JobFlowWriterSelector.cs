using JobFlowOOH.Application.Options;

namespace JobFlowOOH.JobFlow.Writing;

public static class JobFlowWriterSelector
{
    public static bool UseSimulation(bool isDevelopment, string? mode)
    {
        return isDevelopment
            && string.Equals(mode, JobFlowWriteModes.Simulated, StringComparison.OrdinalIgnoreCase);
    }
}
