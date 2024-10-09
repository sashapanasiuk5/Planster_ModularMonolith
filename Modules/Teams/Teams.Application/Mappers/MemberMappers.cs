using Shared.Contracts.Dto.Teams.Member;
using Teams.Domain.Models;

namespace Teams.Application.Mappers;

public static class MemberMappers
{
    public static MemberPermissionDto ToPermissionDto(this ProjectMember projectMember)
    {
        return new MemberPermissionDto()
        {
            ProjectId = projectMember.ProjectId,
            Role = projectMember.Role
        };
    }

    public static MemberDto ToMemberDto(this ProjectMember projectMember)
    {
        return new MemberDto()
        {
            Id = projectMember.Member.Id,
            FirstName = projectMember.Member.FirstName,
            LastName = projectMember.Member.LastName,
            Email = projectMember.Member.Email,
            Role = projectMember.Role.ToString(),
        };
    }
}