using FluentResults;

namespace Teams.Domain.Errors;

public class ProjectNotFound: Error
{ 
    public ProjectNotFound(int id): base($"Project with id {id} not found")
    {
    }
}