using JobFlowOOH.Infrastructure.Portal;

namespace JobFlowOOH.UnitTests.Portal;

public class PortalDatabaseGuardTests
{
    [Fact]
    public void EnsureSeparate_RejectsTheSameServerAndDatabase()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => PortalDatabaseGuard.EnsureSeparate(
            "Server=localhost\\SQLEXPRESS;Database=JobFlowDemo;Integrated Security=True",
            "Server=(local)\\SQLEXPRESS;Database=jobflowdemo;Integrated Security=True"));

        Assert.Contains("must not target the JobFlow database", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void EnsureSeparate_AllowsAnEmptyPortalConnection()
    {
        PortalDatabaseGuard.EnsureSeparate(
            "Server=localhost\\SQLEXPRESS;Database=JobFlowDemo;Integrated Security=True",
            " ");
    }

    [Fact]
    public void EnsureSeparate_AllowsADifferentDatabaseOnTheSameServer()
    {
        PortalDatabaseGuard.EnsureSeparate(
            "Server=localhost\\SQLEXPRESS;Database=JobFlowDemo;Integrated Security=True",
            "Server=localhost\\SQLEXPRESS;Database=JobFlowOohPortal;Integrated Security=True");
    }
}
