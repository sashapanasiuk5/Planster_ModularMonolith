using WorkOrganization.Domain.Models;

namespace Shared.Contracts.Dto.Tasks;

public class TaskShortDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public TaskType Type { get; set; }
    public string Title { get; set; }
}