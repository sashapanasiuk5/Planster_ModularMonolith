using WorkOrganization.Infrastructure.Persistence;

namespace Bootstraper;

public static class Seeder
{
    public static async Task SeedDataAsync(this IWebHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            await TaskStatusesSeeder.SeedAsync(serviceProvider);
        }
    }
}