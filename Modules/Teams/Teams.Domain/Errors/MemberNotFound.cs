using FluentResults;

namespace Teams.Domain.Errors;

public class MemberNotFound: Error
{
    public MemberNotFound(int id): base($"Member with id {id} is not found.")
    {
    }
}