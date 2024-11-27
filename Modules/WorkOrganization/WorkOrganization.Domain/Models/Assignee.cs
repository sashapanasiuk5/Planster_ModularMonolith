namespace WorkOrganization.Domain.Models;

public class Assignee
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    private List<ProjectTask> _tasks = new List<ProjectTask>();
    private List<Project> _projects = new List<Project>();
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();
    public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();
    private Assignee() { }

    public Assignee(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}