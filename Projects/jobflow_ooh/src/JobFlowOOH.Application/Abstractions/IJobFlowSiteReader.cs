using JobFlowOOH.Domain.Sites;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowSiteReader
{
    Task<IReadOnlyList<Site>> ListByClientAsync(string clientCode, CancellationToken cancellationToken);

    Task<IReadOnlyList<Site>> SearchAsync(string clientCode, string? query, CancellationToken cancellationToken);
}
