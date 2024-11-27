using Microsoft.EntityFrameworkCore;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Infrastructure.Persistence.Repositories;

public class SprintRepository: ISprintRepository
{
    private readonly WorkDbContext _context;
    
    public SprintRepository(WorkDbContext context)
    {
        _context = context;
    }

    public Task<List<Sprint>> GetAllSprintsAsync(int projectId)
    {
        return _context.Sprints
            .Where(s => s.ProjectId == projectId)
            .ToListAsync();
    }

    public Task<Sprint?> GetCurrentSprintAsync(int projectId)
    {
        return _context.Sprints
            .Where(s => s.ProjectId == projectId)
            .Where(x => x.EndDate >= DateTime.UtcNow && x.StartDate <= DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<Sprint?> GetByIdAsync(int id)
    {
        var sprint = await _context.Sprints.FindAsync(id);
        return sprint;
    }

    public void Remove(Sprint sprint)
    {
        _context.Sprints.Remove(sprint);
    }

    public void Add(Sprint sprint)
    {
        _context.Sprints.Add(sprint);
    }
}