using System.Collections.Concurrent;
using System.Globalization;
using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.JobFlow.Writing;

public sealed class SimulatedJobFlowJobWriter : IJobFlowJobWriter
{
    private readonly TimeProvider _clock;
    private readonly JobFlowOptions _options;
    private readonly ILogger<SimulatedJobFlowJobWriter> _logger;
    private readonly ConcurrentDictionary<string, string> _references = new(StringComparer.Ordinal);
    private int _sequence;

    public SimulatedJobFlowJobWriter(
        TimeProvider clock,
        IOptions<JobFlowOptions> options,
        ILogger<SimulatedJobFlowJobWriter> logger)
    {
        _clock = clock;
        _options = options.Value;
        _logger = logger;
    }

    public Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        cancellationToken.ThrowIfCancellationRequested();
        var reference = _references.GetOrAdd(command.IdempotencyKey, _ => NextReference());
        var display = $"Simulated job reference {reference}. This was not created in JobFlow.";
        _logger.LogInformation("{DisplayText}", display);
        return Task.FromResult(new JobWriteResult(reference, true, display));
    }

    private string NextReference()
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById(_options.DatabaseTimeZone);
        var local = TimeZoneInfo.ConvertTime(_clock.GetUtcNow(), zone);
        var number = Interlocked.Increment(ref _sequence);
        return string.Create(
            CultureInfo.InvariantCulture,
            $"SIM-{local:yyyyMMdd}-{number:000000}");
    }
}
