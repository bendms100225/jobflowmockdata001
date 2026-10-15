using JobFlowOOH.Application.Abstractions;
using JobFlowOOH.Infrastructure.Portal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobFlowOOH.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddPortalInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddSingleton(new PortalDatabaseOptions
        {
            ConnectionString = configuration.GetConnectionString("PortalDatabase")
        });
        services.AddSingleton<PortalDatabaseInitializer>();
        services.AddSingleton<IPortalIdempotencyStore, PortalIdempotencyStore>();
        return services;
    }
}
