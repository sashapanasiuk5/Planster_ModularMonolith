using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Teams;
using Shared.Contracts.Dto.Work.Sprints;
using Users.Contracts.Dto;
using WorkOrganization.Domain.Models;

namespace Shared.Contracts.Dto.Work.Tasks;

public class CreateTaskDto
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string AcceptanceCriteria { get; set; }
    public required int Priority { get; set; }
    public required int Estimation { get; set; }
    public required TaskType Type { get; set; }
    public required int StatusId { get; set; }
    public required int ProjectId { get; set; }
    public int? SprintId { get; set; }
    public int? AssigneeId { get; set; }
    
    public int? ParentTaskId { get; set; }
    public List<int> SubTasksIdsToAdd { get; set; } = new();
}