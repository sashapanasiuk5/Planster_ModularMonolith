namespace Shared.Contracts.Dto.Work.Sprints;

public class SprintShortDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}