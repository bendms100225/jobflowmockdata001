using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowPocJobReader : IJobFlowPocJobReader
{
    private readonly JobFlowSql _sql;

    public JobFlowPocJobReader(JobFlowSql sql)
    {
        _sql = sql;
    }

    public async Task<IReadOnlyList<PocJobSummary>> ListRecentAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT TOP (25)
                o.jobno AS JobNumber,
                o.clientcode AS ClientCode,
                c.client AS ClientName,
                o.sitecode AS SiteCode,
                h.house AS SiteName,
                o.orderno AS OrderNumber,
                o.categorycode AS CategoryCode,
                cat.category AS CategoryHours,
                CONVERT(varchar(19), o.[date], 120) AS ReceivedText,
                CONVERT(varchar(19), o.deadline, 120) AS DeadlineText
            FROM dbo.orders AS o
            LEFT JOIN dbo.client AS c ON c.clientcode = o.clientcode
            LEFT JOIN dbo.houselist AS h ON h.sitecode = o.sitecode
            LEFT JOIN dbo.category AS cat ON cat.categorycode = o.categorycode
            WHERE CAST(o.description AS varchar(4000)) LIKE 'POC-OOH-%'
              AND o.orderno LIKE 'POC-%'
              AND CAST(o.description AS varchar(4000)) NOT LIKE 'POC-OOH-TEST-%'
            ORDER BY o.jobno DESC
            """;
        return await _sql.QueryAsync<PocJobSummary>(sql, null, cancellationToken);
    }

    public async Task<PocJobDetail?> FindAsync(int jobNumber, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT TOP (1)
                o.jobno AS JobNumber,
                o.clientcode AS ClientCode,
                c.client AS ClientName,
                o.sitecode AS SiteCode,
                h.house AS SiteName,
                o.orderno AS OrderNumber,
                CAST(o.description AS varchar(4000)) AS Description,
                o.categorycode AS CategoryCode,
                cat.category AS CategoryHours,
                cat.categoryname AS CategoryName,
                CONVERT(varchar(19), o.[date], 120) AS ReceivedText,
                CONVERT(varchar(19), o.deadline, 120) AS DeadlineText,
                sec.scode AS SectionCode,
                sec.sname AS SectionName,
                sub.sscode AS SubsectionCode,
                sub.ssname AS SubsectionName,
                o.jobtype AS JobType,
                o.contactname AS SiteContactName,
                o.rpby AS ReportedBy,
                (
                    SELECT COUNT(*)
                    FROM dbo.userlog AS u
                    WHERE u.jobno = o.jobno AND u.formname = 'ORDER'
                ) AS OrderLogCount,
                (
                    SELECT TOP (1) u.luser
                    FROM dbo.userlog AS u
                    WHERE u.jobno = o.jobno AND u.formname = 'ORDER'
                    ORDER BY u.ulkey
                ) AS OrderLogUser
            FROM dbo.orders AS o
            LEFT JOIN dbo.client AS c ON c.clientcode = o.clientcode
            LEFT JOIN dbo.houselist AS h ON h.sitecode = o.sitecode
            LEFT JOIN dbo.category AS cat ON cat.categorycode = o.categorycode
            LEFT JOIN dbo.selectsection AS ss ON ss.jobno = o.jobno
            LEFT JOIN dbo.subsection AS sub ON sub.sscode = ss.sscode
            LEFT JOIN dbo.section AS sec ON sec.scode = sub.scode
            WHERE o.jobno = @JobNumber
              AND CAST(o.description AS varchar(4000)) LIKE 'POC-OOH-%'
              AND o.orderno LIKE 'POC-%'
            ORDER BY ss.sskey
            """;
        var rows = await _sql.QueryAsync<PocJobDetail>(sql, new { JobNumber = jobNumber }, cancellationToken);
        return rows.FirstOrDefault();
    }
}
