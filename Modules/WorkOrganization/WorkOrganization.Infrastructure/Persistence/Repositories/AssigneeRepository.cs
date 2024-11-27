using Microsoft.EntityFrameworkCore;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Infrastructure.Persistence.Repositories;

public class AssigneeRepository: IAssigneeRepository
{
    private readonly WorkDbContext _context;

    public AssigneeRepository(WorkDbContext context)
    {
        _context = context;
    }
    public async Task<Assignee?> GetByIdAsync(int id)
    {
        var assignee = await _context.Assignees.FindAsync(id);
        return assignee;
    }

    public void Add(Assignee assignee)
    {
        _context.Add(assignee);
    }

    public async Task DeleteByIdAsync(int assigneeId)
    {
        await _context.Assignees
            .Where(x => x.Id == assigneeId)
            .ExecuteDeleteAsync();
    }
}