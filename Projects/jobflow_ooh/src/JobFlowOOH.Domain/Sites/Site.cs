namespace JobFlowOOH.Domain.Sites;

public sealed record Site(
    string Code,
    string? ClientCode,
    string? Name,
    string? AddressLine,
    string? Town,
    string? Postcode);
