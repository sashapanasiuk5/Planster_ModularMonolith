namespace Shared.Contracts.Dto.Work.Sprints;

public class CreateSprintDto
{    
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<int> TasksIdsToAdd { get; set; } = new();
}