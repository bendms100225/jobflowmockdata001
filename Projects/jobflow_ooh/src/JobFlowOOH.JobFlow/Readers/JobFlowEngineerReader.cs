using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Domain.Engineers;
using JobFlowOOH.JobFlow.Data;

namespace JobFlowOOH.JobFlow.Readers;

internal sealed class JobFlowEngineerReader : IJobFlowEngineerReader
{
    private readonly JobFlowSql _sql;

    public JobFlowEngineerReader(JobFlowSql sql)
    {
        _sql = sql;
    }

    public async Task<int> CountEmployeesAsync(CancellationToken cancellationToken)
    {
        var rows = await _sql.QueryAsync<int>(JobFlowReadSql.EmployeeCount, null, cancellationToken);
        return rows.Single();
    }

    public async Task<IReadOnlyList<JobFlowEngineer>> ListAsync(CancellationToken cancellationToken)
    {
        var rows = await _sql.QueryAsync<EmployeeRow>(JobFlowReadSql.SelectableEngineers, null, cancellationToken);
        var codes = await LoadSubsectionCodesAsync(cancellationToken);
        return rows
            .Where(row => row.EmployeeId > 0 && !string.IsNullOrWhiteSpace(row.DisplayName))
            .Select(row => ToEngineer(row, codes))
            .ToArray();
    }

    public async Task<JobFlowEngineer?> GetEngineerByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken)
    {
        if (employeeId <= 0)
        {
            return null;
        }

        var rows = await _sql.QueryAsync<EmployeeRow>(
            JobFlowReadSql.SelectableEngineerById,
            new { EmployeeId = employeeId },
            cancellationToken);
        var row = rows.SingleOrDefault();
        if (row is null || row.EmployeeId != employeeId || string.IsNullOrWhiteSpace(row.DisplayName))
        {
            return null;
        }

        var codes = await LoadSubsectionCodesAsync(cancellationToken);
        return ToEngineer(row, codes);
    }

    public async Task<IReadOnlyList<int>> ListOohEngineerIdsAsync(CancellationToken cancellationToken)
    {
        var rows = await _sql.QueryAsync<IdRow>(JobFlowReadSql.OohEngineerIds, null, cancellationToken);
        return rows.Select(row => row.Id).ToArray();
    }

    private async Task<IReadOnlyDictionary<int, string[]>> LoadSubsectionCodesAsync(CancellationToken cancellationToken)
    {
        var rows = await _sql.QueryAsync<SubsectionRow>(JobFlowReadSql.OperativeSubsectionCodes, null, cancellationToken);
        return rows
            .GroupBy(row => row.EmployeeId)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(row => row.SubsectionCode?.Trim())
                    .Where(code => !string.IsNullOrWhiteSpace(code))
                    .Select(code => code!)
                    .Distinct(StringComparer.Ordinal)
                    .ToArray());
    }

    private static JobFlowEngineer ToEngineer(EmployeeRow row, IReadOnlyDictionary<int, string[]> codes)
    {
        codes.TryGetValue(row.EmployeeId, out var subsections);
        return new JobFlowEngineer(
            row.EmployeeId,
            row.DisplayName!.Trim(),
            BlankToNull(row.Discipline),
            BlankToNull(row.Email),
            BlankToNull(row.Phone),
            subsections ?? []);
    }

    private static string? BlankToNull(string? value)
    {
        var trimmed = value?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
    }

    private sealed class EmployeeRow
    {
        public int EmployeeId { get; init; }

        public string? DisplayName { get; init; }

        public string? Discipline { get; init; }

        public string? Email { get; init; }

        public string? Phone { get; init; }
    }

    private sealed class SubsectionRow
    {
        public int EmployeeId { get; init; }

        public string? SubsectionCode { get; init; }
    }

    private sealed class IdRow
    {
        public int Id { get; init; }
    }
}
