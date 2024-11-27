namespace WorkOrganization.Application.Interfaces;

public interface IUnitOfWork
{
    public ITaskRepository TaskRepository { get; }
    public IProjectRepository ProjectRepository { get; }
    public IAssigneeRepository AssigneeRepository { get; }
    public ISprintRepository SprintRepository { get; }
    
    public Task SaveAsync();
}