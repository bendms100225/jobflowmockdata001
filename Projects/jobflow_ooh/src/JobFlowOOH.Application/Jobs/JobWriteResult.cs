namespace JobFlowOOH.Application.Jobs;

public sealed record JobWriteResult(
    string Reference,
    bool IsSimulated,
    string DisplayText,
    PocJobReceipt? Receipt = null);
