using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.JobFlow.Writing;
using JobFlowOOH.UnitTests.Support;

namespace JobFlowOOH.UnitTests.Jobs;

public class JobCreationServiceTests
{
    [Fact]
    public async Task CreateAsync_StopsBeforeTheWriterWhenTheCommandIsIncomplete()
    {
        var writer = new DisabledJobFlowJobWriter(new ListLogger<DisabledJobFlowJobWriter>());
        var service = new JobCreationService(new CreateJobCommandValidator(), writer);
        var command = SampleCommands.Valid();
        command = new CreateJobCommand
        {
            ClientCode = " ",
            SiteCode = command.SiteCode,
            Description = command.Description,
            OrderNumber = command.OrderNumber,
            ReportedBy = command.ReportedBy,
            SiteContactName = command.SiteContactName,
            ReceivedAt = command.ReceivedAt,
            ResponseCategoryCode = command.ResponseCategoryCode,
            RequestedEngineerEmployeeId = command.RequestedEngineerEmployeeId,
            SectionCode = command.SectionCode,
            SubsectionCode = command.SubsectionCode,
            IdempotencyKey = command.IdempotencyKey
        };

        var exception = await Assert.ThrowsAsync<JobCommandValidationException>(
            () => service.CreateAsync(command, CancellationToken.None));

        Assert.Contains("Client is required.", exception.Errors);
    }

    [Fact]
    public void Validate_AcceptsACompleteCommand()
    {
        var errors = new CreateJobCommandValidator().Validate(SampleCommands.Valid());

        Assert.Empty(errors);
    }

    [Fact]
    public void Validate_RejectsAMissingEngineer()
    {
        var command = SampleCommands.Valid();
        command = new CreateJobCommand
        {
            ClientCode = command.ClientCode,
            SiteCode = command.SiteCode,
            Description = command.Description,
            OrderNumber = command.OrderNumber,
            ReportedBy = command.ReportedBy,
            SiteContactName = command.SiteContactName,
            ReceivedAt = command.ReceivedAt,
            ResponseCategoryCode = command.ResponseCategoryCode,
            RequestedEngineerEmployeeId = 0,
            SectionCode = command.SectionCode,
            SubsectionCode = command.SubsectionCode,
            IdempotencyKey = command.IdempotencyKey
        };

        var errors = new CreateJobCommandValidator().Validate(command);

        Assert.Contains(RequestedEngineerText.SelectValid, errors);
    }

    [Fact]
    public async Task CreateAsync_PassesTheRequestedEngineerEmployeeId()
    {
        var writer = new CapturingWriter();
        var service = new JobCreationService(new CreateJobCommandValidator(), writer);

        await service.CreateAsync(SampleCommands.Valid(), CancellationToken.None);

        Assert.NotNull(writer.Command);
        Assert.Equal(10, writer.Command.RequestedEngineerEmployeeId);
    }

    private sealed class CapturingWriter : IJobFlowJobWriter
    {
        public CreateJobCommand? Command { get; private set; }

        public Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken)
        {
            Command = command;
            return Task.FromResult(new JobWriteResult("1", false, "ok"));
        }
    }
}
