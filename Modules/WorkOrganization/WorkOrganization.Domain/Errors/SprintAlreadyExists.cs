using FluentResults;

namespace WorkOrganization.Domain.Errors;

public class SprintAlreadyExists: Error
{
    public SprintAlreadyExists()
    {
        Message = "Sprint already exists";
    }
}