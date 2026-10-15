namespace JobFlowOOH.Application.Jobs;

public readonly record struct LondonJobTimes(
    DateTime ReceivedLocal,
    DateTime DeadlineLocal,
    DateTimeOffset Received,
    DateTimeOffset Deadline);

public static class LondonJobClock
{
    public static LondonJobTimes Calculate(DateTimeOffset receivedAt, int responseHours, string timeZoneId)
    {
        if (responseHours < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(responseHours));
        }

        if (string.IsNullOrWhiteSpace(timeZoneId))
        {
            throw new ArgumentException("A time zone is required.", nameof(timeZoneId));
        }

        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var local = TimeZoneInfo.ConvertTime(receivedAt, zone);
        var receivedLocal = TrimToSeconds(DateTime.SpecifyKind(local.DateTime, DateTimeKind.Unspecified));
        var deadlineLocal = receivedLocal.AddHours(responseHours);
        return new LondonJobTimes(
            receivedLocal,
            deadlineLocal,
            new DateTimeOffset(receivedLocal, zone.GetUtcOffset(receivedLocal)),
            new DateTimeOffset(deadlineLocal, zone.GetUtcOffset(deadlineLocal)));
    }

    public static DateTime LocalNow(TimeProvider clock, string timeZoneId)
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var local = TimeZoneInfo.ConvertTime(clock.GetUtcNow(), zone);
        return TrimToSeconds(DateTime.SpecifyKind(local.DateTime, DateTimeKind.Unspecified));
    }

    public static string SqlText(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
    }

    private static DateTime TrimToSeconds(DateTime value)
    {
        return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, DateTimeKind.Unspecified);
    }
}
