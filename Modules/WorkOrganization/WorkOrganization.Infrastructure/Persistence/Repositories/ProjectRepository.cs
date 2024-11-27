using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Infrastructure.Persistence.Repositories;

public class ProjectRepository: IProjectRepository
{
    private readonly WorkDbContext _context;

    public ProjectRepository(WorkDbContext context)
    {
        _context = context;
    }
    public async Task<Project?> GetByIdAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        return project;
    }

    public void Add(Project project)
    {
        _context.Projects.Add(project);
    }
}