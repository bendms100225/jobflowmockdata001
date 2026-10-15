using JobFlowOOH.Domain.ResponseCategories;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowResponseCategoryReader
{
    Task<IReadOnlyList<ResponseCategory>> ListAsync(CancellationToken cancellationToken);
}
