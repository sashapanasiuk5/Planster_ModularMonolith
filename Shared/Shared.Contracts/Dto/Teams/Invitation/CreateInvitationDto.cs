using Teams.Domain.Enums;

namespace Shared.Contracts.Dto.Teams.Invitation;

public class CreateInvitationDto
{
    public ProjectRole Role { get; set; }
    public int NumberOfPlaces { get; set; }
}