using Identity.Contracts.Dtos;
using Shared.Contracts.Dto.Teams.Member;

namespace Shared.Contracts.ModulesInterfaces;

public interface ITeamsModule
{
    Task<List<MemberPermissionDto?>> GetMemberPermissionsAsync(int memberId);
    
    Task<MemberPermissionDto?> GetMemberPermissionsByProjectAsync(int memberId, int projectId);
}