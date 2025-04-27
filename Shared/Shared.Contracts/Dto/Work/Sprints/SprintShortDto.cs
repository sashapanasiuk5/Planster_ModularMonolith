namespace Shared.Contracts.Dto.Work.Sprints;

public class SprintShortDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}