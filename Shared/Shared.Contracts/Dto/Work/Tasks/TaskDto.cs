using Shared.Contracts.Dto.Teams;
using Shared.Contracts.Dto.Work.Sprints;
using Users.Contracts.Dto;
using WorkOrganization.Domain.Models;

namespace Shared.Contracts.Dto.Tasks;

public class TaskDto
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string AcceptanceCriteria { get; set; }
    public required int Priority { get; set; }
    public required int Estimation { get; set; }
    public required TaskType Type { get; set; }
    
    public required TaskStatusDto Status { get; set; }
    public SprintShortDto? Sprint { get; set; }
    public AssigneeShortDto? Assignee { get; set; }
    
    public TaskShortDto? ParentTask { get; set; }
    public List<TaskShortDto> SubTasks { get; set; } = new();
}