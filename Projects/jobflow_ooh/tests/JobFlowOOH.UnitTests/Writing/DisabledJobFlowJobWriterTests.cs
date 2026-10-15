using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.JobFlow.Writing;
using JobFlowOOH.UnitTests.Support;

namespace JobFlowOOH.UnitTests.Writing;

public class DisabledJobFlowJobWriterTests
{
    [Fact]
    public async Task CreateAsync_ThrowsAndDoesNotDescribeAJobReference()
    {
        var logger = new ListLogger<DisabledJobFlowJobWriter>();
        var writer = new DisabledJobFlowJobWriter(logger);

        var exception = await Assert.ThrowsAsync<JobFlowWriteNotConfiguredException>(
            () => writer.CreateAsync(SampleCommands.Valid(), CancellationToken.None));

        Assert.Contains("disabled", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(logger.Messages, message => message.Contains("disabled", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(logger.Messages, message => message.Contains("SIM-", StringComparison.Ordinal));
    }
}
