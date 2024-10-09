using FluentResults;

namespace Teams.Domain.Errors;

public class InvalidInvitationCode: Error
{
    public InvalidInvitationCode(string code): base($"Invalid invitation code: {code}")
    {
        
    }
}