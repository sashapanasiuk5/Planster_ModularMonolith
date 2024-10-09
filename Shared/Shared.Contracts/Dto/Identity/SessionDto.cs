using Shared.Contracts.Dto.Teams.Member;

namespace Identity.Contracts.Dtos;

public class SessionDto
{
    public string Id { get; set; }
    public int IdentityId { get; set; }
    public DateTime ExpirationDateTime { get; set; }
    public List<MemberPermissionDto> Permissions { get; set; }
}