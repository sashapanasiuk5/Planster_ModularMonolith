using Microsoft.EntityFrameworkCore;
using Teams.Application.Interfaces;
using Teams.Domain.Models;

namespace Teams.Infrastructure.Persistence.Repositories;

public class MembersRepository: IMembersRepository
{
    private readonly TeamsDbContext _teamsDbContext;

    public MembersRepository(TeamsDbContext teamsDbContext)
    {
        _teamsDbContext = teamsDbContext;
    }
    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        return await _teamsDbContext.Members
            .Include(m => m.ProjectMembers)
            .SingleOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<Member?> GetMemberByIdWithProjectsAsync(int id)
    {
        return await _teamsDbContext.Members
            .Include(m => m.ProjectMembers)
            .ThenInclude(m => m.Project)
            .SingleOrDefaultAsync(m => m.Id == id);
    }

    public void AddMember(Member member)
    {
        _teamsDbContext.Members.Add(member);
    }
}