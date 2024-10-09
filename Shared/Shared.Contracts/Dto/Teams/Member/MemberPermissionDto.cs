using Teams.Domain.Enums;

namespace Shared.Contracts.Dto.Teams.Member;

public class MemberPermissionDto
{
    public int ProjectId { get; set; }
    public ProjectRole Role { get; set; }
}