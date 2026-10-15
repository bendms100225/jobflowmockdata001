using JobFlowOOH.Domain.Sections;

namespace JobFlowOOH.Application.Abstractions;

public interface IJobFlowSectionReader
{
    Task<IReadOnlyList<Section>> ListSectionsAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<Subsection>> ListSubsectionsAsync(CancellationToken cancellationToken);
}
