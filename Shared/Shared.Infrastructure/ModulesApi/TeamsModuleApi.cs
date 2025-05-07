using Shared.Contracts.Dto.Teams.Member;
using Shared.Contracts.ModulesInterfaces;
using Teams.Domain.Enums;

namespace Infrastructure.ModulesApi;

public class TeamsModuleApi: ITeamsModule
{
    public Task<List<MemberPermissionDto>> GetMemberPermissionsAsync(int memberId)
    {
        return Task.FromResult(new List<MemberPermissionDto>()
        {
            new ()
            {
                ProjectId = 4,
                Role = ProjectRole.Customer
            }
        });
    }

    public Task<MemberPermissionDto> GetMemberPermissionsByProjectAsync(int memberId, int projectId)
    {
        return Task.FromResult(new MemberPermissionDto()
        {
            ProjectId = projectId,
            Role = ProjectRole.Customer
        });
    }
}