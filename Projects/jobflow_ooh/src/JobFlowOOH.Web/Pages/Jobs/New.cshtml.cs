using System.Text.Json;
using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using JobFlowOOH.Domain.Clients;
using JobFlowOOH.Domain.Engineers;
using JobFlowOOH.Domain.ResponseCategories;
using JobFlowOOH.Domain.Sections;
using JobFlowOOH.Domain.Sites;
using JobFlowOOH.Web.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.Web.Pages.Jobs;

public class NewModel : PageModel
{
    private readonly IJobFlowClientReader _clients;
    private readonly IJobFlowSiteReader _sites;
    private readonly IJobFlowResponseCategoryReader _categories;
    private readonly IJobFlowSectionReader _sections;
    private readonly IJobFlowEngineerReader _engineers;
    private readonly JobFlowOptions _jobFlow;
    private readonly JobFlowWritesOptions _writes;

    public NewModel(
        IJobFlowClientReader clients,
        IJobFlowSiteReader sites,
        IJobFlowResponseCategoryReader categories,
        IJobFlowSectionReader sections,
        IJobFlowEngineerReader engineers,
        IOptions<JobFlowOptions> jobFlow,
        IOptions<JobFlowWritesOptions> writes)
    {
        _clients = clients;
        _sites = sites;
        _categories = categories;
        _sections = sections;
        _engineers = engineers;
        _jobFlow = jobFlow.Value;
        _writes = writes.Value;
    }

    public IReadOnlyList<Client> Clients { get; private set; } = [];

    public IReadOnlyList<Site> Sites { get; private set; } = [];

    public IReadOnlyList<ResponseCategory> Categories { get; private set; } = [];

    public IReadOnlyList<Section> Sections { get; private set; } = [];

    public IReadOnlyList<Subsection> Subsections { get; private set; } = [];

    public IReadOnlyList<JobFlowEngineer> Engineers { get; private set; } = [];

    public string SubsectionJson { get; private set; } = "[]";

    [BindProperty]
    public string? ClientCode { get; set; }

    [BindProperty]
    public string? SiteCode { get; set; }

    [BindProperty]
    public string? Description { get; set; }

    [BindProperty]
    public string? OrderNumber { get; set; }

    [BindProperty]
    public string? ReportedBy { get; set; }

    [BindProperty]
    public string? SiteContactName { get; set; }

    [BindProperty]
    public string? ReceivedLocal { get; set; }

    [BindProperty]
    public int? ResponseCategoryCode { get; set; }

    [BindProperty]
    public string? SectionCode { get; set; }

    [BindProperty]
    public string? SubsectionCode { get; set; }

    [BindProperty]
    public int? RequestedEngineerEmployeeId { get; set; }

    [BindProperty]
    public string? IdempotencyKey { get; set; }

    public async Task OnGetAsync(string? resume, CancellationToken cancellationToken)
    {
        if (string.Equals(resume, "1", StringComparison.Ordinal) && PocJobDraftSession.TryRead(HttpContext.Session, out var draft))
        {
            Apply(draft);
        }
        else
        {
            IdempotencyKey = Guid.NewGuid().ToString("N");
            SectionCode = _writes.DevelopmentSectionId;
            SubsectionCode = _writes.DevelopmentSubsectionId;
            ReceivedLocal = PocReceivedTime.FormatLocal(LondonJobClock.LocalNow(TimeProvider.System, _jobFlow.DatabaseTimeZone));
        }

        await LoadAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostLoadSitesAsync(CancellationToken cancellationToken)
    {
        await LoadAsync(cancellationToken);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await LoadAsync(cancellationToken);
        var engineer = await ValidateEngineerAsync(cancellationToken);
        var description = PocReceivedTime.ApplyPrefix(Description, PocMarkers.DescriptionPrefix);
        var orderNumber = PocReceivedTime.ApplyPrefix(OrderNumber, PocMarkers.OrderPrefix);
        if (description is null)
        {
            ModelState.AddModelError(nameof(Description), "Description is required.");
        }

        if (orderNumber is null)
        {
            ModelState.AddModelError(nameof(OrderNumber), "Order number is required.");
        }

        if (string.IsNullOrWhiteSpace(ClientCode))
        {
            ModelState.AddModelError(nameof(ClientCode), "Client is required.");
        }

        if (string.IsNullOrWhiteSpace(SiteCode))
        {
            ModelState.AddModelError(nameof(SiteCode), "Site is required.");
        }

        if (string.IsNullOrWhiteSpace(ReportedBy))
        {
            ModelState.AddModelError(nameof(ReportedBy), "Reported by is required.");
        }

        if (string.IsNullOrWhiteSpace(SiteContactName))
        {
            ModelState.AddModelError(nameof(SiteContactName), "Site contact is required.");
        }

        if (ResponseCategoryCode is null or <= 0)
        {
            ModelState.AddModelError(nameof(ResponseCategoryCode), "Category is required.");
        }

        if (string.IsNullOrWhiteSpace(SectionCode))
        {
            ModelState.AddModelError(nameof(SectionCode), "Section is required.");
        }

        if (string.IsNullOrWhiteSpace(SubsectionCode))
        {
            ModelState.AddModelError(nameof(SubsectionCode), "Subsection is required.");
        }

        var receivedOk = PocReceivedTime.TryParse(ReceivedLocal, _jobFlow.DatabaseTimeZone, out _);
        if (!receivedOk)
        {
            ModelState.AddModelError(nameof(ReceivedLocal), "Enter the received time.");
        }

        if (!ModelState.IsValid || engineer is null || description is null || orderNumber is null || ResponseCategoryCode is null)
        {
            return Page();
        }

        PocJobDraftSession.Save(HttpContext.Session, new PocJobDraft
        {
            ClientCode = ClientCode!.Trim(),
            SiteCode = SiteCode!.Trim(),
            Description = description,
            OrderNumber = orderNumber,
            ReportedBy = ReportedBy!.Trim(),
            SiteContactName = SiteContactName!.Trim(),
            ReceivedLocal = ReceivedLocal!.Trim(),
            ResponseCategoryCode = ResponseCategoryCode.Value,
            SectionCode = SectionCode!.Trim(),
            SubsectionCode = SubsectionCode!.Trim(),
            RequestedEngineerEmployeeId = engineer.EmployeeId,
            IdempotencyKey = string.IsNullOrWhiteSpace(IdempotencyKey) ? Guid.NewGuid().ToString("N") : IdempotencyKey.Trim()
        });
        return RedirectToPage("/Jobs/Review");
    }

    private async Task<JobFlowEngineer?> ValidateEngineerAsync(CancellationToken cancellationToken)
    {
        if (RequestedEngineerEmployeeId is null or <= 0)
        {
            ModelState.AddModelError(nameof(RequestedEngineerEmployeeId), RequestedEngineerText.SelectValid);
            return null;
        }

        var engineer = await _engineers.GetEngineerByEmployeeIdAsync(RequestedEngineerEmployeeId.Value, cancellationToken);
        if (engineer is null || engineer.EmployeeId != RequestedEngineerEmployeeId.Value)
        {
            ModelState.AddModelError(nameof(RequestedEngineerEmployeeId), RequestedEngineerText.SelectValid);
            return null;
        }

        return engineer;
    }

    private void Apply(PocJobDraft draft)
    {
        ClientCode = draft.ClientCode;
        SiteCode = draft.SiteCode;
        Description = draft.Description.StartsWith(PocMarkers.DescriptionPrefix, StringComparison.Ordinal)
            ? draft.Description[PocMarkers.DescriptionPrefix.Length..]
            : draft.Description;
        OrderNumber = draft.OrderNumber.StartsWith(PocMarkers.OrderPrefix, StringComparison.Ordinal)
            ? draft.OrderNumber[PocMarkers.OrderPrefix.Length..]
            : draft.OrderNumber;
        ReportedBy = draft.ReportedBy;
        SiteContactName = draft.SiteContactName;
        ReceivedLocal = draft.ReceivedLocal;
        ResponseCategoryCode = draft.ResponseCategoryCode;
        SectionCode = draft.SectionCode;
        SubsectionCode = draft.SubsectionCode;
        RequestedEngineerEmployeeId = draft.RequestedEngineerEmployeeId;
        IdempotencyKey = draft.IdempotencyKey;
    }

    private async Task LoadAsync(CancellationToken cancellationToken)
    {
        Clients = await _clients.ListAsync(cancellationToken);
        Categories = await _categories.ListAsync(cancellationToken);
        Sections = await _sections.ListSectionsAsync(cancellationToken);
        Subsections = await _sections.ListSubsectionsAsync(cancellationToken);
        Engineers = await _engineers.ListAsync(cancellationToken);
        Sites = string.IsNullOrWhiteSpace(ClientCode)
            ? []
            : await _sites.ListByClientAsync(ClientCode, cancellationToken);
        SubsectionJson = JsonSerializer.Serialize(
            Subsections.Select(subsection => new
            {
                code = subsection.Code,
                sectionCode = subsection.SectionCode,
                name = subsection.Name
            }));
    }
}
