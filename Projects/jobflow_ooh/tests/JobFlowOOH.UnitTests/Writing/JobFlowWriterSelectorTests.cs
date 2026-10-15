using JobFlowOOH.JobFlow.Writing;

namespace JobFlowOOH.UnitTests.Writing;

public class JobFlowWriterSelectorTests
{
    [Theory]
    [InlineData(false, "Simulated", false)]
    [InlineData(false, "Disabled", false)]
    [InlineData(true, "Disabled", false)]
    [InlineData(true, "disabled", false)]
    [InlineData(true, null, false)]
    [InlineData(true, "Direct", false)]
    [InlineData(true, "Simulated", true)]
    [InlineData(true, "simulated", true)]
    public void UseSimulation_RequiresDevelopmentAndSimulatedMode(bool isDevelopment, string? mode, bool expected)
    {
        Assert.Equal(expected, JobFlowWriterSelector.UseSimulation(isDevelopment, mode));
    }
}
