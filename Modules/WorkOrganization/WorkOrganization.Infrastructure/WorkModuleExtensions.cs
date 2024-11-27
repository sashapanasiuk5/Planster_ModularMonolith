using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkOrganization.Application.Commands.CreateTask;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Infrastructure.Persistence;

namespace WorkOrganization.Infrastructure;

public static class WorkModuleExtensions
{
    public static void AddWorkModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommand).GetTypeInfo().Assembly));
    }
}