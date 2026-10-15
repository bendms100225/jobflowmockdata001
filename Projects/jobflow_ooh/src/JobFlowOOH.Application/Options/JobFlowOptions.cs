namespace JobFlowOOH.Application.Options;

public sealed class JobFlowOptions
{
    public const string SectionName = "JobFlow";

    public string DatabaseTimeZone { get; set; } = "Europe/London";

    public int SiteSearchLimit { get; set; } = 25;
}
