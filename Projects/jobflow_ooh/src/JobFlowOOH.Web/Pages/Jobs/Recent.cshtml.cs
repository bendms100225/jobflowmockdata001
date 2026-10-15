using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobFlowOOH.Web.Pages.Jobs;

public class RecentModel : PageModel
{
    private readonly IJobFlowPocJobReader _jobs;

    public RecentModel(IJobFlowPocJobReader jobs)
    {
        _jobs = jobs;
    }

    public IReadOnlyList<PocJobSummary> Jobs { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Jobs = await _jobs.ListRecentAsync(cancellationToken);
    }
}
