using Teams.Domain.Enums;

namespace Shared.Contracts.Dto.Teams.Invitation;

public class InvitationDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int ProjectId { get; set; }
    public string Role { get; set; }
    public int NumberOfPlaces { get; set; }
    public int NumberOfInvitedUsers { get; set; }
}