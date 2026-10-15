namespace JobFlowOOH.Application.Options;

public sealed class JobFlowWritesOptions
{
    public const string SectionName = "JobFlowWrites";

    public string Mode { get; set; } = JobFlowWriteModes.Disabled;

    public bool AllowDevelopmentDatabaseMutation { get; set; }

    public string DevelopmentUsername { get; set; } = "";

    public string DevelopmentCallTakenBy { get; set; } = "";

    public string DevelopmentSectionId { get; set; } = "";

    public string DevelopmentSubsectionId { get; set; } = "";

    public int? DevelopmentJobType { get; set; }
}
