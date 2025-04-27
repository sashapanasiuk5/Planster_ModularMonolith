namespace Shared.Contracts.Dto.Work.Sprints;

public class UpdateSprintDto
{
    public string Title { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public List<int> TasksIdsToAdd { get; set; } = new();
    public List<int> TasksIdsToRemove { get; set; } = new();
}