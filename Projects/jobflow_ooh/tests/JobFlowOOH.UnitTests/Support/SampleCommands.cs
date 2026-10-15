using JobFlowOOH.Application.Jobs;

namespace JobFlowOOH.UnitTests.Support;

internal static class SampleCommands
{
    public static CreateJobCommand Valid(string idempotencyKey = "key-1")
    {
        return new CreateJobCommand
        {
            ClientCode = "C1",
            SiteCode = "S1",
            Description = "POC-OOH-UNIT-001",
            OrderNumber = "POC-UNIT-001",
            ReportedBy = "Caller",
            SiteContactName = "Site contact",
            ReceivedAt = new DateTimeOffset(2026, 10, 15, 18, 0, 0, TimeSpan.Zero),
            ResponseCategoryCode = 2,
            RequestedEngineerEmployeeId = 10,
            SectionCode = "GM",
            SubsectionCode = "16",
            IdempotencyKey = idempotencyKey
        };
    }
}
