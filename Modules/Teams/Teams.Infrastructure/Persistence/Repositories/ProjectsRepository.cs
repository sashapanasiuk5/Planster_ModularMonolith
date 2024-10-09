using Microsoft.EntityFrameworkCore;
using Teams.Application.Interfaces;
using Teams.Domain.Models;

namespace Teams.Infrastructure.Persistence.Repositories;

public class ProjectsRepository: IProjectsRepository
{
    private readonly TeamsDbContext _context;

    public ProjectsRepository(TeamsDbContext context)
    {
        _context = context;
    }
    public void AddProject(Project project)
    {
        _context.Projects.Add(project);
    }

    public void DeleteProject(Project project)
    {
        _context.Projects.Remove(project);
    }

    public async Task<Project?> GetProjectById(int projectId)
    {
        return await _context.Projects
            .Include(p => p.Members)
            .Include(p => p.Invitations)
            .SingleOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<Project?> GetProjectWithMembersById(int projectId)
    {
        return await _context.Projects
            .Include(p => p.Members)
            .ThenInclude(x => x.Member)
            .Include(p => p.Invitations)
            .SingleOrDefaultAsync(p => p.Id == projectId);
    }

    public void AddInvitation(Invitation invitation)
    {
        _context.Entry(invitation).State = EntityState.Added;
    }

    public void AddNewProjectMember(ProjectMember member)
    {
        _context.Entry(member).State = EntityState.Added;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}