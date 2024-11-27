using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Interfaces;

public interface IAssigneeRepository
{
    Task<Assignee?> GetByIdAsync(int id);
    void Add(Assignee assignee);
    Task DeleteByIdAsync(int assigneeId);
}