using JobFlowOOH.Application.Options;
using JobFlowOOH.JobFlow.Writing;
using JobFlowOOH.UnitTests.Support;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.UnitTests.Writing;

public class SimulatedJobFlowJobWriterTests
{
    [Fact]
    public async Task CreateAsync_ReturnsAMarkedSimulationAndRepeatsTheSameKey()
    {
        var logger = new ListLogger<SimulatedJobFlowJobWriter>();
        var writer = CreateWriter(logger, new DateTimeOffset(2026, 10, 15, 12, 0, 0, TimeSpan.Zero));

        var first = await writer.CreateAsync(SampleCommands.Valid("same"), CancellationToken.None);
        var repeat = await writer.CreateAsync(SampleCommands.Valid("same"), CancellationToken.None);
        var second = await writer.CreateAsync(SampleCommands.Valid("other"), CancellationToken.None);

        Assert.True(first.IsSimulated);
        Assert.Equal("SIM-20261015-000001", first.Reference);
        Assert.Contains("Simulated", first.DisplayText, StringComparison.Ordinal);
        Assert.Contains("not created in JobFlow", first.DisplayText, StringComparison.Ordinal);
        Assert.Equal(first.Reference, repeat.Reference);
        Assert.Equal("SIM-20261015-000002", second.Reference);
        Assert.Contains(logger.Messages, message => message.Contains("Simulated job reference SIM-20261015-000001", StringComparison.Ordinal));
    }

    [Fact]
    public async Task CreateAsync_UsesTheConfiguredLondonDate()
    {
        var writer = CreateWriter(
            new ListLogger<SimulatedJobFlowJobWriter>(),
            new DateTimeOffset(2026, 10, 15, 23, 30, 0, TimeSpan.Zero));

        var result = await writer.CreateAsync(SampleCommands.Valid(), CancellationToken.None);

        Assert.Equal("SIM-20261016-000001", result.Reference);
        Assert.True(result.IsSimulated);
    }

    private static SimulatedJobFlowJobWriter CreateWriter(ListLogger<SimulatedJobFlowJobWriter> logger, DateTimeOffset utcNow)
    {
        return new SimulatedJobFlowJobWriter(
            new FixedTimeProvider(utcNow),
            Options.Create(new JobFlowOptions { DatabaseTimeZone = "Europe/London", SiteSearchLimit = 25 }),
            logger);
    }
}
