using FluentResults;

namespace Teams.Domain.Errors;

public class MemberAlreadyExists: Error
{

    public MemberAlreadyExists(int id)
    {
        Message = $"Member with ID {id} already exists on this project.";
    }
}