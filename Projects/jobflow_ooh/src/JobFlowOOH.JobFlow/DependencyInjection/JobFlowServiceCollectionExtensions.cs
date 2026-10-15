using System.Globalization;
using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using JobFlowOOH.JobFlow.Data;
using JobFlowOOH.JobFlow.Readers;
using JobFlowOOH.JobFlow.Writing;
using JobFlowOOH.JobFlow.Writing.Experimental;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobFlowOOH.JobFlow;

public static class JobFlowServiceCollectionExtensions
{
    public static IServiceCollection AddJobFlow(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton(TimeProvider.System);
        services.Configure<JobFlowOptions>(configuration.GetSection(JobFlowOptions.SectionName));
        services.Configure<JobFlowWritesOptions>(configuration.GetSection(JobFlowWritesOptions.SectionName));
        services.AddSingleton<JobFlowReadOnlyConnectionFactory>();
        services.AddSingleton<JobFlowSql>();
        services.AddSingleton<IJobFlowClientReader, JobFlowClientReader>();
        services.AddSingleton<IJobFlowSiteReader, JobFlowSiteReader>();
        services.AddSingleton<IJobFlowEngineerReader, JobFlowEngineerReader>();
        services.AddSingleton<IJobFlowResponseCategoryReader, JobFlowResponseCategoryReader>();
        services.AddSingleton<IJobFlowSectionReader, JobFlowSectionReader>();
        services.AddSingleton<IJobFlowPocJobReader, JobFlowPocJobReader>();
        services.AddSingleton<CreateJobCommandValidator>();
        services.AddSingleton<IJobCreationService, JobCreationService>();

        var mode = configuration[$"{JobFlowWritesOptions.SectionName}:Mode"];
        if (JobFlowWriterSelector.UseSimulation(isDevelopment, mode))
        {
            services.AddSingleton<IJobFlowJobWriter, SimulatedJobFlowJobWriter>();
        }
        else if (ExperimentalWriteGuard.IsExperimentalMode(mode))
        {
            var writes = BindWrites(configuration);
            ExperimentalWriteGuard.EnsureRegistered(
                isDevelopment,
                writes,
                configuration.GetConnectionString("JobFlowExperimental"));
            services.AddSingleton<IJobFlowJobWriter>(services => new ExperimentalDirectSqlJobFlowJobWriter(
                configuration.GetConnectionString("JobFlowExperimental"),
                services.GetRequiredService<IOptions<JobFlowWritesOptions>>(),
                services.GetRequiredService<IOptions<JobFlowOptions>>(),
                services.GetRequiredService<IPortalIdempotencyStore>(),
                services.GetRequiredService<IJobFlowPocJobReader>(),
                services.GetRequiredService<IJobFlowEngineerReader>(),
                services.GetRequiredService<CreateJobCommandValidator>(),
                services.GetRequiredService<TimeProvider>(),
                services.GetRequiredService<ILogger<ExperimentalDirectSqlJobFlowJobWriter>>(),
                isDevelopment));
        }
        else if (string.IsNullOrWhiteSpace(mode) || string.Equals(mode, JobFlowWriteModes.Disabled, StringComparison.OrdinalIgnoreCase))
        {
            services.AddSingleton<IJobFlowJobWriter, DisabledJobFlowJobWriter>();
        }
        else
        {
            throw new InvalidOperationException($"Unknown JobFlowWrites mode '{mode}'.");
        }

        return services;
    }

    private static JobFlowWritesOptions BindWrites(IConfiguration configuration)
    {
        var section = JobFlowWritesOptions.SectionName;
        return new JobFlowWritesOptions
        {
            Mode = configuration[$"{section}:Mode"] ?? JobFlowWriteModes.Disabled,
            AllowDevelopmentDatabaseMutation = bool.TryParse(configuration[$"{section}:AllowDevelopmentDatabaseMutation"], out var allow) && allow,
            DevelopmentUsername = configuration[$"{section}:DevelopmentUsername"] ?? "",
            DevelopmentCallTakenBy = configuration[$"{section}:DevelopmentCallTakenBy"] ?? "",
            DevelopmentSectionId = configuration[$"{section}:DevelopmentSectionId"] ?? "",
            DevelopmentSubsectionId = configuration[$"{section}:DevelopmentSubsectionId"] ?? "",
            DevelopmentJobType = int.TryParse(configuration[$"{section}:DevelopmentJobType"], NumberStyles.Integer, CultureInfo.InvariantCulture, out var jobType)
                ? jobType
                : null
        };
    }
}
