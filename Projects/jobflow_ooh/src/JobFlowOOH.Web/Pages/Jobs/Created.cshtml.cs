using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Domain.Engineers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobFlowOOH.Web.Pages.Jobs;

public class CreatedModel : PageModel
{
    private readonly IJobFlowPocJobReader _jobs;
    private readonly IPortalIdempotencyStore _store;
    private readonly IJobFlowEngineerReader _engineers;

    public CreatedModel(IJobFlowPocJobReader jobs, IPortalIdempotencyStore store, IJobFlowEngineerReader engineers)
    {
        _jobs = jobs;
        _store = store;
        _engineers = engineers;
    }

    public PocJobDetail Job { get; private set; } = null!;

    public string CorrelationId { get; private set; } = "";

    public int? RequestedEngineerEmployeeId { get; private set; }

    public string RequestedEngineerName { get; private set; } = RequestedEngineerText.NotAssigned;

    public string? RequestedEngineerPhone { get; private set; }

    public string? RequestedEngineerDiscipline { get; private set; }

    public async Task<IActionResult> OnGetAsync(int jobNumber, CancellationToken cancellationToken)
    {
        var job = await _jobs.FindAsync(jobNumber, cancellationToken);
        if (job is null)
        {
            return NotFound();
        }

        Job = job;
        var submission = await _store.FindByJobNumberAsync(jobNumber, cancellationToken);
        if (submission is null)
        {
            CorrelationId = "Not recorded in the portal submission store.";
            return Page();
        }

        CorrelationId = submission.CorrelationId.ToString("D");
        RequestedEngineerEmployeeId = submission.RequestedEngineerEmployeeId;
        var engineer = await _engineers.GetEngineerByEmployeeIdAsync(submission.RequestedEngineerEmployeeId, cancellationToken);
        if (engineer is not null && engineer.EmployeeId == submission.RequestedEngineerEmployeeId)
        {
            RequestedEngineerName = engineer.DisplayName;
            RequestedEngineerPhone = engineer.Phone;
            RequestedEngineerDiscipline = engineer.Discipline;
        }

        return Page();
    }
}
