using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Tasks;
using Shared.Contracts.Dto.Work.Tasks;
using Teams.Domain.Enums;
using WorkOrganization.Application.Commands.CreateTask;
using WorkOrganization.Application.Commands.DeleteTask;
using WorkOrganization.Application.Commands.GetAllTasks;
using WorkOrganization.Application.Commands.GetById;
using WorkOrganization.Application.Commands.Tasks.ChangeTaskStatus;
using WorkOrganization.Application.Commands.Tasks.FindTaskByTitle;
using WorkOrganization.Application.Commands.Tasks.FindTasksWithHierarchy;
using WorkOrganization.Application.Commands.Tasks.GetTasksOnBoard;
using WorkOrganization.Application.Commands.UpdateTask;

namespace WorkOrganization.Infrastructure.Controllers;

[Route("projects/{projectId}/tasks")]
public class TaskController: BaseController
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{taskId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee, ProjectRole.Customer)]
    public async Task<IActionResult> GetTask([FromRoute] int taskId)
    {
        return HandleResult(await _mediator.Send(new GetTaskByIdCommand(taskId)));
    }

    [HttpGet("board")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee, ProjectRole.Customer)]
    public async Task<IActionResult> GetBoard([FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new GetTasksOnBoardCommand(projectId)));
    }

    [HttpGet("search")]
    [ProjectAuth(ProjectRole.Customer, ProjectRole.Employee, ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> SearchTasks([FromRoute] int projectId, [FromQuery] string search, [FromQuery] bool hierarchical)
    {
        if (hierarchical)
        {
            return HandleResult(await _mediator.Send(new FindTasksWithHierarchyCommand(projectId, search)));
        }
        return HandleResult(await _mediator.Send(new FindTaskByTitleCommand(projectId, search)));
    }

    [HttpGet]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee, ProjectRole.Customer)]
    public async Task<IActionResult> GetTasks([FromRoute] int projectId, [FromQuery] int? statusId, [FromQuery] int? sprintId, [FromQuery] int? assigneeId)
    {
        TaskFilterDto? filter = null;
        if (statusId != null || sprintId != null || assigneeId != null)
        {
            filter = new TaskFilterDto()
            {
                StatusId = statusId,
                SprintId = sprintId,
                AssigneeId = assigneeId
            };
        }
        return HandleResult(await _mediator.Send(new GetAllTasksCommand(projectId, filter)));
    }
    
    [HttpPost]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee)]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto task)
    {
        return HandleResult(await _mediator.Send(new CreateTaskCommand(task)));
    }

    [HttpPut]
    [Route("{taskId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee)]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto task, [FromRoute] int taskId)
    {
        return HandleResult(await _mediator.Send(new UpdateTaskCommand(task, taskId)));
    }

    [HttpPut("{taskId}/status")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee)]
    public async Task<IActionResult> ChangeStatus([FromRoute] int taskId, [FromQuery] int statusId)
    {
        return HandleResult(await _mediator.Send(new ChangeTaskStatusCommand(taskId, statusId)));
    }

    [HttpDelete]
    [Route("{taskId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee)]
    public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
    {
        return HandleResult(await _mediator.Send(new DeleteTaskCommand(taskId)));
    }
}