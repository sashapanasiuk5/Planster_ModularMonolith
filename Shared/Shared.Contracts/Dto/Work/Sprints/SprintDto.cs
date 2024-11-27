using Shared.Contracts.Dto.Tasks;

namespace Shared.Contracts.Dto.Work.Sprints;

public class SprintDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required List<TaskHierarchyDto> Tasks { get; set; } = new();
} 