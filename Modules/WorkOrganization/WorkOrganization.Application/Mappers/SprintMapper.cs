using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Work.Sprints;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Mappers;

public static class SprintMapper
{
    public static SprintShortDto ToShortDto(this Sprint sprint)
    {
        return new SprintShortDto()
        {
            Id = sprint.Id,
            Title = sprint.Title,
            EndDate = sprint.EndDate,
            StartDate = sprint.StartDate,
        };
    }

    public static SprintDto ToDto(this Sprint sprint, List<TaskHierarchyDto> tasks)
    {
        return new SprintDto()
        {
            Id = sprint.Id,
            Title = sprint.Title,
            EndDate = sprint.EndDate,
            StartDate = sprint.StartDate,
            Tasks = tasks
        };
    }

    public static Sprint ToModel(this CreateSprintDto dto, Project project)
    {
        return new Sprint(dto.Title, dto.StartDate, dto.EndDate, project);
    }
}