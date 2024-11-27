namespace Shared.Contracts.Dto.Work.Sprints;

public class UpdateSprintDto
{
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}