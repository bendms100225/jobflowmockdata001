namespace JobFlowOOH.Application.Jobs;

public sealed class CreateJobCommandValidator
{
    public const int OrderNumberLimit = 30;

    public const int NameLimit = 50;

    public const int CodeLimit = 50;

    public const int SectionLimit = 20;

    public const int DescriptionLimit = 4000;

    public const int IdempotencyKeyLimit = 200;

    public IReadOnlyList<string> Validate(CreateJobCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        var errors = new List<string>();
        Require(errors, command.ClientCode, "Client", CodeLimit);
        Require(errors, command.SiteCode, "Site", CodeLimit);
        Require(errors, command.Description, "Description", DescriptionLimit);
        Require(errors, command.OrderNumber, "Order number", OrderNumberLimit);
        Require(errors, command.ReportedBy, "Reported by", NameLimit);
        Require(errors, command.SiteContactName, "Site contact", NameLimit);
        Require(errors, command.SectionCode, "Section", SectionLimit);
        Require(errors, command.SubsectionCode, "Subsection", SectionLimit);
        Require(errors, command.IdempotencyKey, "Idempotency key", IdempotencyKeyLimit);
        if (command.ReceivedAt == default)
        {
            errors.Add("Received time is required.");
        }

        if (command.RequestedEngineerEmployeeId <= 0)
        {
            errors.Add(RequestedEngineerText.SelectValid);
        }

        if (!string.IsNullOrWhiteSpace(command.Description)
            && !command.Description.Trim().StartsWith(PocMarkers.DescriptionPrefix, StringComparison.Ordinal))
        {
            errors.Add($"Description must start with {PocMarkers.DescriptionPrefix}.");
        }

        if (!string.IsNullOrWhiteSpace(command.OrderNumber)
            && !command.OrderNumber.Trim().StartsWith(PocMarkers.OrderPrefix, StringComparison.Ordinal))
        {
            errors.Add($"Order number must start with {PocMarkers.OrderPrefix}.");
        }

        return errors;
    }

    private static void Require(List<string> errors, string? value, string label, int limit)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"{label} is required.");
            return;
        }

        if (value.Trim().Length > limit)
        {
            errors.Add($"{label} must be {limit} characters or fewer.");
        }
    }
}
