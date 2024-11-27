using FluentResults;

namespace WorkOrganization.Domain.Errors;

public class SprintNotFound: Error
{
    public SprintNotFound(int id)
    {
        Message = $"Sprint with id {id} not found";
    }
}