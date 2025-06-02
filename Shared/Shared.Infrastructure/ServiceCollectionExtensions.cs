using Infrastructure.EventBus;
using Infrastructure.ModulesApi;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.ModulesInterfaces;
using Shared.Infrastructure.EventBus;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITeamsModule, TeamsModuleApi>();
        services.AddAuthentication("ProjectAuth")
            .AddScheme<AuthenticationSchemeOptions, SimpleAuthSchemeHandler>("SimpleAuth", opt => { })
            .AddScheme<AuthenticationSchemeOptions, ProjectAuthSchemeHandler>("ProjectAuth",opt => { });
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IEventBus, EventBus.EventBus>();
        services.AddHostedService<IntegrationEventProcessorJob>();
        return services;
    }
}