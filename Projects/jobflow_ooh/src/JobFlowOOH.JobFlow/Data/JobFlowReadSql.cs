namespace JobFlowOOH.JobFlow.Data;

internal static class JobFlowReadSql
{
    internal const string Clients = """
        SELECT
            clientcode AS Code,
            client AS Name,
            town AS Town,
            postcode AS Postcode
        FROM dbo.client
        ORDER BY client, clientcode
        """;

    internal const string SitesByClient = """
        SELECT
            sitecode AS Code,
            clientcode AS ClientCode,
            house AS Name,
            address1 AS AddressLine,
            town AS Town,
            postcode AS Postcode
        FROM dbo.houselist
        WHERE clientcode = @ClientCode
        ORDER BY house, sitecode
        """;

    internal const string SearchSites = """
        SELECT TOP (@Limit)
            sitecode AS Code,
            clientcode AS ClientCode,
            house AS Name,
            address1 AS AddressLine,
            town AS Town,
            postcode AS Postcode
        FROM dbo.houselist
        WHERE clientcode = @ClientCode
          AND (
                @HasQuery = 0
                OR house LIKE @Like ESCAPE '\'
                OR address1 LIKE @Like ESCAPE '\'
                OR town LIKE @Like ESCAPE '\'
                OR postcode LIKE @Like ESCAPE '\'
              )
        ORDER BY house, sitecode
        """;

    internal const string EmployeeCount = """
        SELECT COUNT(*)
        FROM dbo.employee
        """;

    internal const string SelectableEngineers = """
        SELECT
            e.idno AS EmployeeId,
            LTRIM(RTRIM(e.name)) AS DisplayName,
            NULLIF(LTRIM(RTRIM(e.trade)), '') AS Discipline,
            NULLIF(LTRIM(RTRIM(e.email)), '') AS Email,
            NULLIF(LTRIM(RTRIM(e.phone)), '') AS Phone
        FROM dbo.employee AS e
        WHERE LTRIM(RTRIM(e.name)) <> ''
          AND EXISTS (
                SELECT 1
                FROM dbo.operative AS o
                WHERE o.idno = e.idno
              )
        ORDER BY LTRIM(RTRIM(e.name)), e.idno
        """;

    internal const string SelectableEngineerById = """
        SELECT
            e.idno AS EmployeeId,
            LTRIM(RTRIM(e.name)) AS DisplayName,
            NULLIF(LTRIM(RTRIM(e.trade)), '') AS Discipline,
            NULLIF(LTRIM(RTRIM(e.email)), '') AS Email,
            NULLIF(LTRIM(RTRIM(e.phone)), '') AS Phone
        FROM dbo.employee AS e
        WHERE e.idno = @EmployeeId
          AND LTRIM(RTRIM(e.name)) <> ''
          AND EXISTS (
                SELECT 1
                FROM dbo.operative AS o
                WHERE o.idno = e.idno
              )
        """;

    internal const string OperativeSubsectionCodes = """
        SELECT
            o.idno AS EmployeeId,
            o.sscode AS SubsectionCode
        FROM dbo.operative AS o
        WHERE o.idno IS NOT NULL
          AND o.sscode IS NOT NULL
          AND LTRIM(RTRIM(o.sscode)) <> ''
        ORDER BY o.idno, o.sscode
        """;

    internal const string OohEngineerIds = """
        SELECT idno AS Id
        FROM dbo.ooheng
        WHERE idno IS NOT NULL
        ORDER BY idno
        """;

    internal const string ResponseCategories = """
        SELECT
            categorycode AS Code,
            category AS Hours,
            categoryname AS Name
        FROM dbo.category
        ORDER BY categorycode
        """;

    internal const string Sections = """
        SELECT
            scode AS Code,
            sname AS Name
        FROM dbo.section
        ORDER BY sname, scode
        """;

    internal const string Subsections = """
        SELECT
            sscode AS Code,
            scode AS SectionCode,
            ssname AS Name
        FROM dbo.subsection
        ORDER BY ssname, sscode
        """;
}
