namespace Shared.Contracts.Dto.Work.Tasks;

public class UpdateTaskDto: CreateTaskDto
{
    public List<int> SubTasksIdsToRemove { get; set; } = new();
}