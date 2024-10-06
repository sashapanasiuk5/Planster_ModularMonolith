using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventBus;
using Shared.Infrastructure.Middlewares;

namespace Shared.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, SessionAuthSchemeHandler>("SessionTokens",
            opt => { });
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