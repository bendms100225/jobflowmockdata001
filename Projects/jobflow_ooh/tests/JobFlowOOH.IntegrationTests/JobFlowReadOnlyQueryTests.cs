using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Options;
using JobFlowOOH.JobFlow.Data;
using JobFlowOOH.JobFlow.Readers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.IntegrationTests;

public class JobFlowReadOnlyQueryTests
{
    [Fact]
    public async Task Readers_ReturnReferenceDataWithoutChangingTheDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(FindWebProject())
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();
        var sql = new JobFlowSql(new JobFlowReadOnlyConnectionFactory(configuration));
        var categories = await new JobFlowResponseCategoryReader(sql).ListAsync(CancellationToken.None);
        var clients = await new JobFlowClientReader(sql).ListAsync(CancellationToken.None);
        var sections = await new JobFlowSectionReader(sql).ListSectionsAsync(CancellationToken.None);
        var subsections = await new JobFlowSectionReader(sql).ListSubsectionsAsync(CancellationToken.None);
        var engineers = await new JobFlowEngineerReader(sql).ListAsync(CancellationToken.None);
        var oohIds = await new JobFlowEngineerReader(sql).ListOohEngineerIdsAsync(CancellationToken.None);

        Assert.Contains(categories, category => category.Code == 2 && category.Hours == 4);
        Assert.Contains(categories, category => category.Code == 3 && category.Hours == 24);
        Assert.Contains(categories, category => category.Code == 5 && category.Hours == 168);
        Assert.NotEmpty(clients);
        Assert.NotEmpty(sections);
        Assert.NotEmpty(subsections);
        Assert.NotEmpty(engineers);
        Assert.All(engineers, engineer => Assert.True(engineer.EmployeeId > 0));
        Assert.All(engineers, engineer => Assert.False(string.IsNullOrWhiteSpace(engineer.DisplayName)));
        Assert.All(engineers, engineer => Assert.NotEmpty(engineer.OperativeSubsectionCodes));
        Assert.Empty(oohIds);

        var sites = await new JobFlowSiteReader(sql, Options.Create(new JobFlowOptions { SiteSearchLimit = 25 }))
            .SearchAsync(clients[0].Code, null, CancellationToken.None);
        Assert.True(sites.Count <= 25);
        Assert.All(sites, site => Assert.Equal(clients[0].Code, site.ClientCode));
    }

    private static string FindWebProject()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "src", "JobFlowOOH.Web");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not find the web project.");
    }
}
