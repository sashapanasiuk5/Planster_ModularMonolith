namespace WorkOrganization.Domain.Models;

public class Sprint
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public Project Project { get; private set; }
    public int ProjectId { get; private set; }
    
    private List<ProjectTask> _tasks = new List<ProjectTask>();
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();
    
    
    private Sprint() { }

    public Sprint(string title, DateTime startDate, DateTime endDate, Project project)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        Project = project;
    }

    public void Update(string title, DateTime startDate, DateTime endDate)
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