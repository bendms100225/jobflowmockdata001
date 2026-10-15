namespace JobFlowOOH.Application.Abstractions;

public interface IPortalIdempotencyStore
{
    Task<PortalIdempotencyRecord?> FindAsync(string key, CancellationToken cancellationToken);

    Task<PortalIdempotencyRecord?> FindByJobNumberAsync(int jobNumber, CancellationToken cancellationToken);

    Task<bool> TryBeginAsync(PortalIdempotencyDraft draft, CancellationToken cancellationToken);

    Task CompleteAsync(string key, int jobNumber, CancellationToken cancellationToken);

    Task AbandonAsync(string key, CancellationToken cancellationToken);
}

public sealed record PortalIdempotencyDraft(
    string Key,
    string RequestHash,
    Guid CorrelationId,
    int RequestedEngineerEmployeeId);

public sealed record PortalIdempotencyRecord(
    string Key,
    string RequestHash,
    int? JobNumber,
    Guid CorrelationId,
    int RequestedEngineerEmployeeId,
    string Status);
