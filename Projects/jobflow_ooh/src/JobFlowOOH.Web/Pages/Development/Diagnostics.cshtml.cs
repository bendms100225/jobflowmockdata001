using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Domain.Engineers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobFlowOOH.Web.Pages.Development;

public class DiagnosticsModel : PageModel
{
    private readonly IWebHostEnvironment _environment;
    private readonly IJobFlowEngineerReader _engineers;

    public DiagnosticsModel(IWebHostEnvironment environment, IJobFlowEngineerReader engineers)
    {
        _environment = environment;
        _engineers = engineers;
    }

    public int EmployeeCount { get; private set; }

    public int SelectableCount { get; private set; }

    public IReadOnlyList<JobFlowEngineer> Sample { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }

        var engineers = await _engineers.ListAsync(cancellationToken);
        EmployeeCount = await _engineers.CountEmployeesAsync(cancellationToken);
        SelectableCount = engineers.Count;
        Sample = engineers.Take(10).ToArray();
        return Page();
    }
}
