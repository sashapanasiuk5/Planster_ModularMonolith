using WorkOrganization.Domain.Models;

namespace Shared.Contracts.Dto.Tasks;

public class TaskHierarchyDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public TaskType Type { get; set; }
    public string AssigneeName { get; set; }
    public int Priority { get; set; }
    public List<TaskHierarchyDto> SubTasks { get; set; }
}