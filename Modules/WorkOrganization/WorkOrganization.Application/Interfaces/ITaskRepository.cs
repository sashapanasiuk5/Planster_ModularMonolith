using Shared.Contracts.Dto.Work.Tasks;
using WorkOrganization.Domain.Models;
using TaskStatus = WorkOrganization.Domain.Models.TaskStatus;

namespace WorkOrganization.Application.Interfaces;

public interface ITaskRepository
{
    
    Task<ProjectTask?> GetWithHierarchyByIdAsync(int id);
    Task<List<ProjectTask>> GetAllWithHierarchyAsync(int projectId, TaskFilterDto? filter = null);
    Task<ProjectTask?> GetByIdAsync(int id);
    Task<ProjectTask?> GetByIdWithDetailsAsync(int id);
    Task<TaskStatus?> GetStatusByIdAsync(int id);

    Task<List<TaskStatus>> GetAllStatusesAsync();
    Task<List<ProjectTask>> FindTasksByTileAsync(int projectId, string title);
    
    void RemoveTask(ProjectTask projectTask);
    void AddTask(ProjectTask task);
}