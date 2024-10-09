using FluentResults;
using Teams.Domain.Enums;

namespace Teams.Domain.Errors;

public class InvitationWithSameRoleExists: Error
{
    public InvitationWithSameRoleExists(ProjectRole role)
    {
        Message = $"Invitation with role {Enum.GetName(role)} already exists.";
    }
}