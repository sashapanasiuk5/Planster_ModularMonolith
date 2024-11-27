namespace WorkOrganization.Domain.Models;

public class TaskStatus
{
    public int Id { get; init; }
    public string Name { get; init; }

    private TaskStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public TaskStatus(string name)
    {
        Name = name;
    }
}