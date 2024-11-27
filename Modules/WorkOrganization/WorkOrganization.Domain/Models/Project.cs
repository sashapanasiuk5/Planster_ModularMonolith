namespace WorkOrganization.Domain.Models;

public class Project
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private List<Assignee> _assignees = new List<Assignee>();
    private List<ProjectTask> _tasks = new List<ProjectTask>();
    private List<Sprint> _sprints = new List<Sprint>();
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();
    public IReadOnlyCollection<Sprint> Sprints => _sprints.AsReadOnly();
    public IReadOnlyCollection<Assignee> Assignees => _assignees.AsReadOnly();

    public Project(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public void AddAssignee(Assignee assignee)
    {
        _assignees.Add(assignee);
    }

    public void RemoveAssignee(Assignee assignee)
    {
        _assignees.Remove(assignee);
    }
}