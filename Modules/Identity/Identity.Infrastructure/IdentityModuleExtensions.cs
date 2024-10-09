using System.Reflection;
using Application.Commands.Login;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.ModulesInterfaces;

namespace Infrastructure;

public static class IdentityModuleExtensions
{
    public static void AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddScoped<ISessionStore, SessionStore>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IEncryptor, Encryptor>();
        services.Configure<EncryptionOptions>(opts =>
        {
            opts.Key = configuration.GetSection("Encryption")!.GetValue<string>("Key");
            opts.IV = configuration.GetSection("Encryption")!.GetValue<string>("IV");
            opts.Prefix = configuration.GetSection("Encryption")!.GetValue<string>("Prefix");
        });
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(LoginCommandHandler).Assembly
            )
        );
        services.AddScoped<IIdentityModule, IdentityModule>();
    }

}