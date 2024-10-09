using Teams.Domain.Models;

namespace Teams.Application.Interfaces;

public interface IMembersRepository
{
    Task<Member?> GetMemberByIdAsync(int id);
    Task<Member?> GetMemberByIdWithProjectsAsync(int id);
    void AddMember(Member member);
    
}