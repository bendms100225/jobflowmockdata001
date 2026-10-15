namespace JobFlowOOH.Application.Jobs;

public sealed class CreateJobCommand
{
    public required string ClientCode { get; init; }

    public required string SiteCode { get; init; }

    public required string Description { get; init; }

    public required string OrderNumber { get; init; }

    public required string ReportedBy { get; init; }

    public required string SiteContactName { get; init; }

    public required DateTimeOffset ReceivedAt { get; init; }

    public required int ResponseCategoryCode { get; init; }

    public required int RequestedEngineerEmployeeId { get; init; }

    public required string SectionCode { get; init; }

    public required string SubsectionCode { get; init; }

    public required string IdempotencyKey { get; init; }
}
