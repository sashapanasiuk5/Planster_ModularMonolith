using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int id);
    void Add(Project project);
}