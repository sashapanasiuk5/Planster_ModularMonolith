namespace Shared.Contracts.Dto.Teams.Invitation;

public class InvitationInfoDto
{
    public string Code { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string Role { get; set; }
}