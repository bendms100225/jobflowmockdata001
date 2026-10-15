using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using JobFlowOOH.JobFlow.Writing.Experimental;
using JobFlowOOH.UnitTests.Support;

namespace JobFlowOOH.UnitTests.Writing;

public class ExperimentalWriteGuardTests
{
    private const string DemoConnection = "Server=localhost\\SQLEXPRESS;Database=JobFlowDemo;Integrated Security=True;TrustServerCertificate=True";

    [Fact]
    public void EnsureRegistered_RejectsNonDevelopment()
    {
        var exception = Assert.Throws<JobFlowExperimentalWriteException>(() =>
            ExperimentalWriteGuard.EnsureRegistered(false, ValidOptions(), DemoConnection));

        Assert.Contains("outside Development", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void EnsureRegistered_RejectsWhenMutationIsNotAllowed()
    {
        var options = ValidOptions();
        options.AllowDevelopmentDatabaseMutation = false;

        var exception = Assert.Throws<JobFlowExperimentalWriteException>(() =>
            ExperimentalWriteGuard.EnsureRegistered(true, options, DemoConnection));

        Assert.Contains("AllowDevelopmentDatabaseMutation", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void EnsureConnection_RejectsAnotherDatabaseName()
    {
        var exception = Assert.Throws<JobFlowExperimentalWriteException>(() =>
            ExperimentalWriteGuard.EnsureConnection("Server=localhost\\SQLEXPRESS;Database=master;Integrated Security=True"));

        Assert.Contains("JobFlowDemo", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void EnsureConnection_RejectsAReadOnlyConnection()
    {
        var exception = Assert.Throws<JobFlowExperimentalWriteException>(() =>
            ExperimentalWriteGuard.EnsureConnection(DemoConnection + ";ApplicationIntent=ReadOnly"));

        Assert.Contains("read-only", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void EnsureRegistered_AcceptsTheDevelopmentDemoGate()
    {
        ExperimentalWriteGuard.EnsureRegistered(true, ValidOptions(), DemoConnection);
    }

    [Fact]
    public void Calculate_UsesLondonLocalTimeAndCategoryHours()
    {
        var times = LondonJobClock.Calculate(new DateTimeOffset(2026, 10, 15, 23, 30, 0, TimeSpan.Zero), 4, "Europe/London");

        Assert.Equal(new DateTime(2026, 10, 16, 0, 30, 0), times.ReceivedLocal);
        Assert.Equal(new DateTime(2026, 10, 16, 4, 30, 0), times.DeadlineLocal);
    }

    [Fact]
    public void Hash_ChangesWhenTheDescriptionChanges()
    {
        var first = IdempotencyHasher.Hash(SampleCommands.Valid("same"), "office", "office", 0);
        var changed = SampleCommands.Valid("same");
        changed = new CreateJobCommand
        {
            ClientCode = changed.ClientCode,
            SiteCode = changed.SiteCode,
            Description = "POC-OOH-UNIT-002",
            OrderNumber = changed.OrderNumber,
            ReportedBy = changed.ReportedBy,
            SiteContactName = changed.SiteContactName,
            ReceivedAt = changed.ReceivedAt,
            ResponseCategoryCode = changed.ResponseCategoryCode,
            RequestedEngineerEmployeeId = changed.RequestedEngineerEmployeeId,
            SectionCode = changed.SectionCode,
            SubsectionCode = changed.SubsectionCode,
            IdempotencyKey = changed.IdempotencyKey
        };

        Assert.Equal(first, IdempotencyHasher.Hash(SampleCommands.Valid("same"), "office", "office", 0));
        Assert.NotEqual(first, IdempotencyHasher.Hash(changed, "office", "office", 0));
    }

    private static JobFlowWritesOptions ValidOptions()
    {
        return new JobFlowWritesOptions
        {
            Mode = JobFlowWriteModes.ExperimentalDirectSql,
            AllowDevelopmentDatabaseMutation = true,
            DevelopmentUsername = "configured-login",
            DevelopmentCallTakenBy = "configured-login",
            DevelopmentSectionId = "GM",
            DevelopmentSubsectionId = "16",
            DevelopmentJobType = 0
        };
    }
}
