using Shared.Contracts.Dto.Teams;
using Teams.Domain.Models;

namespace Teams.Application.Mappers;

public static class ProjectMapper
{
    public static Project ToProject(this ProjectDto dto)
    {
        return new Project()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    }
    
    public static Project ToProject(this CreateUpdateProjectDto dto)
    {
        return new Project()
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }
}