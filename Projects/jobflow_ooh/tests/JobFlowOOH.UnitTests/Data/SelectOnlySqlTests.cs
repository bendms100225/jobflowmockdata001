using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.UnitTests.Data;

public class SelectOnlySqlTests
{
    [Fact]
    public void Ensure_AcceptsTheReadStatements()
    {
        SelectOnlySql.Ensure(JobFlowReadSql.Clients);
        SelectOnlySql.Ensure(JobFlowReadSql.SitesByClient);
        SelectOnlySql.Ensure(JobFlowReadSql.SearchSites);
        SelectOnlySql.Ensure(JobFlowReadSql.EmployeeCount);
        SelectOnlySql.Ensure(JobFlowReadSql.SelectableEngineers);
        SelectOnlySql.Ensure(JobFlowReadSql.SelectableEngineerById);
        SelectOnlySql.Ensure(JobFlowReadSql.OperativeSubsectionCodes);
        SelectOnlySql.Ensure(JobFlowReadSql.OohEngineerIds);
        SelectOnlySql.Ensure(JobFlowReadSql.ResponseCategories);
        SelectOnlySql.Ensure(JobFlowReadSql.Sections);
        SelectOnlySql.Ensure(JobFlowReadSql.Subsections);
    }

    [Theory]
    [InlineData("UPDATE dbo.orders SET description = 'x'")]
    [InlineData("SELECT sitecode FROM dbo.houselist; SELECT clientcode FROM dbo.client")]
    [InlineData("SELECT clientcode INTO dbo.copy FROM dbo.client")]
    public void Ensure_RejectsStatementsThatAreNotASingleRead(string sql)
    {
        Assert.Throws<InvalidOperationException>(() => SelectOnlySql.Ensure(sql));
    }
}
