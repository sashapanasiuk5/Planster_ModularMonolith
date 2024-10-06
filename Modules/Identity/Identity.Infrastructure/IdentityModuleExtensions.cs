using System.Reflection;
using Application.Commands.Login;
using Application.Handlers;
using Application.Interfaces;
using Application.Services;
using Infrastructure.ModulesInterfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class IdentityModuleExtensions
{
    public static void AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddScoped<ISessionStore, SessionStore>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(LoginCommandHandler).Assembly,
            typeof(UserRegisteredIntegrationEventHandler).Assembly
            )
        );
        services.AddScoped<IIdentityModule, IdentityModule>();
    }
}