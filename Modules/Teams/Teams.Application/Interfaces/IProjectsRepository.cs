using Teams.Domain.Models;

namespace Teams.Application.Interfaces;

public interface IProjectsRepository
{
    void AddProject(Project project);
    void DeleteProject(Project project);
    
    Task<Project?> GetProjectById(int projectId);
    Task<Project?> GetProjectWithMembersById(int projectId);
    
    
    void AddInvitation(Invitation invitation);
    void AddNewProjectMember(ProjectMember member);
}