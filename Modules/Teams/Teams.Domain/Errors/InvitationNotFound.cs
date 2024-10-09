using FluentResults;

namespace Shared.Contracts.Errors;

public class InvitationNotFound: Error
{
    public InvitationNotFound(int id)
    {
        Message = $"Invitation with id {id} not found";
    }
}