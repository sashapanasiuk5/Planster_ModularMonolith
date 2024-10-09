using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Commands.CreateProject;
using Teams.Application.Interfaces;
using Teams.Domain.Interfaces;
using Teams.Domain.Services;
using Teams.Infrastructure.Persistence;

namespace Teams.Infrastructure;

public static class TeamsModuleExtensions
{
    public static void AddTeamsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TeamsDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRandomStringGenerator, RandomStringGenerator>();
        services.AddScoped<ITeamsModule, TeamsModule>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).GetTypeInfo().Assembly));
    }
}