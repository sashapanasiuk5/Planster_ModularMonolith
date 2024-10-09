using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Teams.Infrastructure.Persistence;

namespace Infrastructure.Utils;

public static class Seeder
{
    public static async Task SeedDataAsync(this IWebHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            //await TeamsModuleSeeder.SeedAsync(serviceProvider);
        }
    }
}