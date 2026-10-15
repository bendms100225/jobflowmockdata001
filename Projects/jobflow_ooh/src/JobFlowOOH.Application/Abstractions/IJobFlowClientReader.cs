using JobFlowOOH.Domain.Clients;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowClientReader
{
    Task<IReadOnlyList<Client>> ListAsync(CancellationToken cancellationToken);
}
