using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using JobFlowOOH.Domain.Engineers;
using JobFlowOOH.Web.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.Web.Pages.Jobs;

public class ReviewModel : PageModel
{
    private readonly IJobFlowClientReader _clients;
    private readonly IJobFlowSiteReader _sites;
    private readonly IJobFlowResponseCategoryReader _categories;
    private readonly IJobFlowSectionReader _sections;
    private readonly IJobFlowEngineerReader _engineers;
    private readonly IJobCreationService _jobs;
    private readonly JobFlowOptions _jobFlow;

    public ReviewModel(
        IJobFlowClientReader clients,
        IJobFlowSiteReader sites,
        IJobFlowResponseCategoryReader categories,
        IJobFlowSectionReader sections,
        IJobFlowEngineerReader engineers,
        IJobCreationService jobs,
        IOptions<JobFlowOptions> jobFlow)
    {
        _clients = clients;
        _sites = sites;
        _categories = categories;
        _sections = sections;
        _engineers = engineers;
        _jobs = jobs;
        _jobFlow = jobFlow.Value;
    }

    public PocJobDraft Draft { get; private set; } = new();

    public string ClientText { get; private set; } = "";

    public string SiteText { get; private set; } = "";

    public string CategoryText { get; private set; } = "";

    public string SectionText { get; private set; } = "";

    public JobFlowEngineer? RequestedEngineer { get; private set; }

    public bool CanCreate { get; private set; }

    [BindProperty]
    public bool ConfirmExperimentalWrite { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        if (!PocJobDraftSession.TryRead(HttpContext.Session, out var draft))
        {
            return RedirectToPage("/Jobs/New");
        }

        await LoadAsync(draft, cancellationToken);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!PocJobDraftSession.TryRead(HttpContext.Session, out var draft))
        {
            return RedirectToPage("/Jobs/New");
        }

        await LoadAsync(draft, cancellationToken);
        if (!ConfirmExperimentalWrite)
        {
            ModelState.AddModelError(nameof(ConfirmExperimentalWrite), "Confirm that this creates an experimental JobFlowDemo record.");
        }

        if (RequestedEngineer is null || RequestedEngineer.EmployeeId != draft.RequestedEngineerEmployeeId)
        {
            ModelState.AddModelError(string.Empty, RequestedEngineerText.SelectValid);
            CanCreate = false;
        }

        if (!PocReceivedTime.TryParse(draft.ReceivedLocal, _jobFlow.DatabaseTimeZone, out var received))
        {
            ModelState.AddModelError(string.Empty, "Enter the received time.");
        }

        if (!ModelState.IsValid || RequestedEngineer is null)
        {
            return Page();
        }

        try
        {
            var result = await _jobs.CreateAsync(draft.ToCommand(received), cancellationToken);
            if (result.Receipt is null || result.Receipt.RequestedEngineerEmployeeId != draft.RequestedEngineerEmployeeId)
            {
                ModelState.AddModelError(string.Empty, result.DisplayText);
                return Page();
            }

            return RedirectToPage("/Jobs/Created", new { jobNumber = result.Receipt.JobNumber });
        }
        catch (JobCommandValidationException exception)
        {
            foreach (var error in exception.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
        catch (Exception exception) when (exception is JobFlowExperimentalWriteException or IdempotencyConflictException or JobFlowWriteNotConfiguredException)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
        }

        return Page();
    }

    private async Task LoadAsync(PocJobDraft draft, CancellationToken cancellationToken)
    {
        Draft = draft;
        var clients = await _clients.ListAsync(cancellationToken);
        var client = clients.FirstOrDefault(item => string.Equals(item.Code, draft.ClientCode, StringComparison.OrdinalIgnoreCase));
        ClientText = client is null ? draft.ClientCode : $"{client.Name} ({client.Code})";

        var sites = string.IsNullOrWhiteSpace(draft.ClientCode)
            ? []
            : await _sites.ListByClientAsync(draft.ClientCode, cancellationToken);
        var site = sites.FirstOrDefault(item => string.Equals(item.Code, draft.SiteCode, StringComparison.OrdinalIgnoreCase));
        SiteText = site is null ? draft.SiteCode : $"{site.Name} ({site.Code})";

        var categories = await _categories.ListAsync(cancellationToken);
        var category = categories.FirstOrDefault(item => item.Code == draft.ResponseCategoryCode);
        CategoryText = category is null
            ? draft.ResponseCategoryCode.ToString()
            : $"Code {category.Code} — {(category.Hours?.ToString() ?? "no")} hours";

        var sections = await _sections.ListSectionsAsync(cancellationToken);
        var subsections = await _sections.ListSubsectionsAsync(cancellationToken);
        var section = sections.FirstOrDefault(item => string.Equals(item.Code, draft.SectionCode, StringComparison.OrdinalIgnoreCase));
        var subsection = subsections.FirstOrDefault(item => string.Equals(item.Code, draft.SubsectionCode, StringComparison.Ordinal));
        var sectionName = section?.Name ?? draft.SectionCode;
        var subsectionName = subsection?.Name ?? draft.SubsectionCode;
        SectionText = $"{sectionName} ({draft.SectionCode}) / {subsectionName} ({draft.SubsectionCode})";

        RequestedEngineer = await _engineers.GetEngineerByEmployeeIdAsync(draft.RequestedEngineerEmployeeId, cancellationToken);
        CanCreate = RequestedEngineer is not null && RequestedEngineer.EmployeeId == draft.RequestedEngineerEmployeeId;
        if (!CanCreate)
        {
            ModelState.AddModelError(string.Empty, RequestedEngineerText.SelectValid);
        }
    }
}
