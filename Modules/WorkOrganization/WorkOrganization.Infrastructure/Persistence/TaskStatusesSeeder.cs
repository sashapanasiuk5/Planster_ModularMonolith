using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskStatus = WorkOrganization.Domain.Models.TaskStatus;

namespace WorkOrganization.Infrastructure.Persistence;

public static class TaskStatusesSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<WorkDbContext>();

        if (!context.TaskStatuses.Any())
        {
            await context.TaskStatuses.AddRangeAsync(
                new TaskStatus("Backlog"),
                new TaskStatus("In Progress"),
                new TaskStatus("Done")
                );
            await context.SaveChangesAsync();
        }
    }
}