using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Options;
using JobFlowOOH.Domain.Sites;
using JobFlowOOH.JobFlow.Data;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowSiteReader : IJobFlowSiteReader
{
    private readonly JobFlowSql _sql;
    private readonly IOptions<JobFlowOptions> _options;

    public JobFlowSiteReader(JobFlowSql sql, IOptions<JobFlowOptions> options)
    {
        _sql = sql;
        _options = options;
    }

    public Task<IReadOnlyList<Site>> ListByClientAsync(string clientCode, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientCode);
        return _sql.QueryAsync<Site>(
            JobFlowReadSql.SitesByClient,
            new { ClientCode = clientCode.Trim() },
            cancellationToken);
    }

    public Task<IReadOnlyList<Site>> SearchAsync(string clientCode, string? query, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientCode);
        var limit = _options.Value.SiteSearchLimit;
        if (limit < 1)
        {
            limit = 25;
        }

        var trimmed = query?.Trim();
        var hasQuery = string.IsNullOrEmpty(trimmed) ? 0 : 1;
        return _sql.QueryAsync<Site>(
            JobFlowReadSql.SearchSites,
            new
            {
                ClientCode = clientCode.Trim(),
                Limit = limit,
                HasQuery = hasQuery,
                Like = hasQuery == 1 ? ToContainsPattern(trimmed!) : "%"
            },
            cancellationToken);
    }

    private static string ToContainsPattern(string query)
    {
        var escaped = query
            .Replace("\\", "\\\\", StringComparison.Ordinal)
            .Replace("%", "\\%", StringComparison.Ordinal)
            .Replace("_", "\\_", StringComparison.Ordinal)
            .Replace("[", "\\[", StringComparison.Ordinal);
        return "%" + escaped + "%";
    }
}
