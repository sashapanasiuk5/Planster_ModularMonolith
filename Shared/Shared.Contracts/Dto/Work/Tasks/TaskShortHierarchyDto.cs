using WorkOrganization.Domain.Models;

namespace Shared.Contracts.Dto.Work.Tasks;

public class TaskShortHierarchyDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public TaskType Type { get; set; }
    public List<TaskShortHierarchyDto> SubTasks { get; set; }
}