using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Interfaces;

public interface ISprintRepository
{
    Task<List<Sprint>> GetAllSprintsAsync(int projectId);
    Task<Sprint?> GetCurrentSprintAsync(int projectId);
    Task<Sprint?> GetByIdAsync(int id);
    void Remove(Sprint sprint);
    void Add(Sprint sprint);
}