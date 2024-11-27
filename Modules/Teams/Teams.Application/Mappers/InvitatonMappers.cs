using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Domain.Models;

namespace Teams.Application.Mappers;

public static class InvitatonMappers
{
    public static InvitationDto ToDto(this Invitation invitation)
    {
        return new InvitationDto()
        {
            Id = invitation.Id,
            Code = invitation.Code,
            ProjectId = invitation.Project.Id,
            NumberOfPlaces = invitation.NumberOfPlaces,
            NumberOfInvitedUsers = invitation.NumberOfInvitedUsers,
            Role = Enum.GetName(invitation.Role),
            RoleId = invitation.Role,
        };
    }
}