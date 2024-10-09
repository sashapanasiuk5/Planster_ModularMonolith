using Shared.Contracts.Dto.Teams.Member;

namespace Shared.Contracts.ModulesInterfaces;

public interface ITeamsModule
{
    Task<List<MemberPermissionDto>> GetMemberPermissionsAsync(int memberId);
}