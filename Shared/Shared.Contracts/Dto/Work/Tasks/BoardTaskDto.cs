namespace Shared.Contracts.Dto.Work.Tasks;

public class BoardTaskDto
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; }
    public int? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
}