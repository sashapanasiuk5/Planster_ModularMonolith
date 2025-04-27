using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Commands.Register;
using User.Application.Interfaces;
using User.Application.Services;
using Users.Infrastructure.Persistence;
using Users.Infrastructure.Persistence.Repositories;

namespace Users.Infrastructure;

public static class UsersModuleExtensions
{
    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IThumbnailService, ThumbnailService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).GetTypeInfo().Assembly));
    }
}