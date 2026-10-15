namespace JobFlowOOH.Application.Jobs;

public sealed record PocJobSummary(
    int JobNumber,
    string ClientCode,
    string? ClientName,
    string SiteCode,
    string? SiteName,
    string OrderNumber,
    int? CategoryCode,
    int? CategoryHours,
    string ReceivedText,
    string DeadlineText);

public sealed record PocJobDetail(
    int JobNumber,
    string ClientCode,
    string? ClientName,
    string SiteCode,
    string? SiteName,
    string OrderNumber,
    string Description,
    int? CategoryCode,
    int? CategoryHours,
    string? CategoryName,
    string ReceivedText,
    string DeadlineText,
    string? SectionCode,
    string? SectionName,
    string? SubsectionCode,
    string? SubsectionName,
    int? JobType,
    string? SiteContactName,
    string? ReportedBy,
    int OrderLogCount,
    string? OrderLogUser);
