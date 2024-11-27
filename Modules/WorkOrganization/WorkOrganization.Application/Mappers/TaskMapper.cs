using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Teams;
using Shared.Contracts.Dto.Work.Sprints;
using Shared.Contracts.Dto.Work.Tasks;
using Users.Contracts.Dto;
using WorkOrganization.Domain.Models;
using TaskStatus = WorkOrganization.Domain.Models.TaskStatus;

namespace WorkOrganization.Application.Mappers;

public static class TaskMapper
{
    public static ProjectTask ToModel(this CreateTaskDto dto, Project project)
    {
        return new ProjectTask(
            dto.Code,
            dto.Title,
            dto.Description,
            dto.AcceptanceCriteria,
            dto.Priority,
            dto.Estimation,
            dto.Type,
            project);
    }

    public static TaskDto ToDto(this ProjectTask task)
    {
        var dto = new TaskDto()
        {
            Code = task.Code,
            Description = task.Description,
            AcceptanceCriteria = task.AcceptanceCriteria,
            Id = task.Id,
            Title = task.Title,
            Type = task.Type,
            Status = new TaskStatusDto()
            {
                Id = task.Status.Id,
                Name = task.Status.Name,
            },
            Priority = task.Priority,
            Estimation = task.Estimation,
            Sprint = task.Sprint?.ToShortDto(),
            ParentTask = task.ParentTask?.ToShortDto()
        };
        if (task.Assignee is not null)
        {
            dto.Assignee = new AssigneeShortDto()
            {
                Id = task.Assignee.Id,
                Name = task.Assignee.Name,
            };
        }

        foreach (var subTask in task.SubTasks)
        {
            dto.SubTasks.Add(subTask.ToShortDto());
        }

        return dto;
    }

    public static TaskShortDto ToShortDto(this ProjectTask task)
    {
        return new TaskShortDto()
        {
            Id = task.Id,
            Code = task.Code,
            Type = task.Type,
            Title = task.Title,
        };
    }

    public static TaskHierarchyDto ToHierarchyDto(this ProjectTask task)
    {
        TaskHierarchyDto dto = new TaskHierarchyDto()
        {
            Id = task.Id,
            Code = task.Code,
            Title = task.Title,
            Type = task.Type,
            Priority = task.Priority,
            Status = task.Status.Name,
        };

        if (task.Assignee != null)
            dto.AssigneeName = task.Assignee.Name;
        
        if (task.SubTasks.Count > 0)
        {
            List<TaskHierarchyDto> subTasks = new List<TaskHierarchyDto>();
            foreach (var subTask in task.SubTasks)
            {
                subTasks.Add(subTask.ToHierarchyDto());
            }
            dto.SubTasks = subTasks;
        }
        return dto;
    }
}