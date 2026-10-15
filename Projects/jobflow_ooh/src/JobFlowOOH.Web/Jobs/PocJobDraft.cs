using System.Globalization;
using System.Text.Json;
using JobFlowOOH.Application.Jobs;

namespace JobFlowOOH.Web.Jobs;

public sealed class PocJobDraft
{
    public string ClientCode { get; set; } = "";

    public string SiteCode { get; set; } = "";

    public string Description { get; set; } = "";

    public string OrderNumber { get; set; } = "";

    public string ReportedBy { get; set; } = "";

    public string SiteContactName { get; set; } = "";

    public string ReceivedLocal { get; set; } = "";

    public int ResponseCategoryCode { get; set; }

    public string SectionCode { get; set; } = "";

    public string SubsectionCode { get; set; } = "";

    public int RequestedEngineerEmployeeId { get; set; }

    public string IdempotencyKey { get; set; } = "";

    public CreateJobCommand ToCommand(DateTimeOffset receivedAt)
    {
        return new CreateJobCommand
        {
            ClientCode = ClientCode,
            SiteCode = SiteCode,
            Description = Description,
            OrderNumber = OrderNumber,
            ReportedBy = ReportedBy,
            SiteContactName = SiteContactName,
            ReceivedAt = receivedAt,
            ResponseCategoryCode = ResponseCategoryCode,
            RequestedEngineerEmployeeId = RequestedEngineerEmployeeId,
            SectionCode = SectionCode,
            SubsectionCode = SubsectionCode,
            IdempotencyKey = IdempotencyKey
        };
    }
}

public static class PocJobDraftSession
{
    public const string Key = "jobflow-ooh-poc-draft";

    public static void Save(ISession session, PocJobDraft draft)
    {
        session.SetString(Key, JsonSerializer.Serialize(draft));
    }

    public static bool TryRead(ISession session, out PocJobDraft draft)
    {
        var json = session.GetString(Key);
        if (string.IsNullOrWhiteSpace(json))
        {
            draft = new PocJobDraft();
            return false;
        }

        var read = JsonSerializer.Deserialize<PocJobDraft>(json);
        if (read is null || read.RequestedEngineerEmployeeId <= 0 || string.IsNullOrWhiteSpace(read.IdempotencyKey))
        {
            draft = new PocJobDraft();
            return false;
        }

        draft = read;
        return true;
    }
}

public static class PocReceivedTime
{
    public static bool TryParse(string? text, string timeZoneId, out DateTimeOffset received)
    {
        received = default;
        var value = text?.Trim();
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var formats = new[] { "yyyy-MM-ddTHH:mm", "yyyy-MM-ddTHH:mm:ss" };
        if (!DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
        {
            return false;
        }

        var unspecified = DateTime.SpecifyKind(parsed, DateTimeKind.Unspecified);
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        received = new DateTimeOffset(unspecified, zone.GetUtcOffset(unspecified));
        return true;
    }

    public static string FormatLocal(DateTime value)
    {
        return value.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
    }

    public static string? ApplyPrefix(string? value, string prefix)
    {
        var body = value?.Trim() ?? "";
        if (body.Length == 0)
        {
            return null;
        }

        return body.StartsWith(prefix, StringComparison.Ordinal) ? body : prefix + body;
    }
}
