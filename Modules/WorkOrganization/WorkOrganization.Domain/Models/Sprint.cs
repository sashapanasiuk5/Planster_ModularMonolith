namespace WorkOrganization.Domain.Models;

public class Sprint
{
    public int Id { get;  }
    public string Title { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    
    public Project Project { get; private set; }
    public int ProjectId { get;  }
    
    private readonly List<ProjectTask> _tasks = new List<ProjectTask>();
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();
    
    
    private Sprint() { }

    public Sprint(string title, DateOnly startDate, DateOnly endDate, Project project)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        Project = project;
    }

    public void Update(string title, DateOnly startDate, DateOnly endDate)
    {
        Title = title;
        EndDate = endDate;
        StartDate = startDate;
    }
    

    public void AddTask(ProjectTask task)
    {
        _tasks.Add(task);
    }

    public void RemoveTask(ProjectTask task)
    {
        _tasks.Remove(task);
    }
}