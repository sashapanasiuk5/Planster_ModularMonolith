using FluentResults;

namespace WorkOrganization.Domain.Models;

public class ProjectTask
{
    public int Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string AcceptanceCriteria { get; private set; } = string.Empty;
    public int Priority { get; private set; }
    public int Estimation { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskType Type { get; private set; }
    public Project Project { get; private set; }
    
    public int ProjectId { get; private set; }
    public Assignee? Assignee { get; private set; }
    public Sprint? Sprint { get; private set; }
    
    public ProjectTask? ParentTask { get; private set; }
    public int? ParentTaskId { get; private set; }
    private List<ProjectTask> _subTasks = new List<ProjectTask>();
    public IReadOnlyCollection<ProjectTask> SubTasks => _subTasks.AsReadOnly();
    
    private ProjectTask()
    {
    }

    public ProjectTask(string code, string title, string description, string acceptanceCriteria, int priority, int estimation, TaskType type, Project project)
    {

        Code = code;
        Title = title;
        Description = description;
        AcceptanceCriteria = acceptanceCriteria;
        Priority = priority;
        Estimation = estimation;
        Type = type;
        Project = project;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string code, string title, string description, string acceptanceCriteria, int priority, int estimation, TaskType type)
    {
        Code = code;
        Title = title;
        Description = description;
        AcceptanceCriteria = acceptanceCriteria;
        Priority = priority;
        Estimation = estimation;
        Type = type;
    }

    public void SetAssignee(Assignee? assignee)
    {
        Assignee = assignee;
    }

    public void AddToSprint(Sprint sprint)
    {
        Sprint = sprint;
        foreach (var task in _subTasks)
        {
            task.AddToSprint(sprint);
        }
    }

    public void RemoveFromSprint()
    {
        Sprint = null;
        foreach (var task in _subTasks)
        {
            task.RemoveFromSprint();
        }
    }

    public Result AddParentTask(ProjectTask parentTask)
    {
        if (CanAddParentTask(parentTask))
        {
            ParentTask = parentTask;
            ParentTaskId = parentTask.Id;
            return Result.Ok();
        }
        return Result.Fail("Parent task cannot be added");
    }

    public void RemoveParentTask()
    {
        ParentTask = null;
        ParentTaskId = null;
    }

    public Result AddSubTask(ProjectTask subTask)
    {
        if (CanAddSubTask(subTask))
        {
            _subTasks.Add(subTask);
            return Result.Ok();
        }
        return Result.Fail("Sub task cannot be added");
    }

    public Result RemoveSubTaskById(int id)
    {
        var subTask = _subTasks.FirstOrDefault(s => s.Id == id);
        if(subTask is null)
            return Result.Fail($"Sub task with id {id} cannot be found");
        _subTasks.Remove(subTask);
        return Result.Ok();
    }

    public void ChangeStatus(TaskStatus newStatus)
    {
        Status = newStatus;
    }


    private bool CanAddParentTask(ProjectTask parentTask)
    {
        if (Type == TaskType.Epic)
            return false;
        if (Type == TaskType.Story && parentTask.Type != TaskType.Epic)
            return false;
        if (Type == TaskType.Task && parentTask.Type != TaskType.Story)
            return false;
        return true;
    }

    private bool CanAddSubTask(ProjectTask subTask)
    {
        if (Type == TaskType.Task)
            return false;
        if(Type == TaskType.Story && subTask.Type != TaskType.Task)
            return false;
        if(Type == TaskType.Epic && subTask.Type != TaskType.Story)
            return false;
        return true;
    }
}