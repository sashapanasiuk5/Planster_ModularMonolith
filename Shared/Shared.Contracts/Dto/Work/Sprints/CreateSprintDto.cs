namespace Shared.Contracts.Dto.Work.Sprints;

public class CreateSprintDto
{    
    public string Title { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<int> TasksIdsToAdd { get; set; } = new();
}