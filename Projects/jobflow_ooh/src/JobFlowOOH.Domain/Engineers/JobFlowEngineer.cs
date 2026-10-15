namespace JobFlowOOH.Domain.Engineers;

public sealed record JobFlowEngineer(
    int EmployeeId,
    string DisplayName,
    string? Discipline,
    string? Email,
    string? Phone,
    IReadOnlyList<string> OperativeSubsectionCodes);
