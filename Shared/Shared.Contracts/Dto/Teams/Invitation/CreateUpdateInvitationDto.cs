using Teams.Domain.Enums;

namespace Shared.Contracts.Dto.Teams.Invitation;

public class CreateUpdateInvitationDto
{
    public ProjectRole RoleId { get; set; }
    public int NumberOfPlaces { get; set; }
}