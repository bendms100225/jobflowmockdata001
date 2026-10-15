using JobFlowOOH.Domain.Engineers;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowEngineerReader
{
    Task<int> CountEmployeesAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<JobFlowEngineer>> ListAsync(CancellationToken cancellationToken);

    Task<JobFlowEngineer?> GetEngineerByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken);

    Task<IReadOnlyList<int>> ListOohEngineerIdsAsync(CancellationToken cancellationToken);
}
