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
using WorkOrganization.Application.Commands.Tasks.FindTaskByTitle;
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

    [HttpGet("search")]
    [ProjectAuth(ProjectRole.Customer, ProjectRole.Employee, ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> SearchTasks([FromRoute] int projectId, [FromQuery] string search)
    {
        return HandleResult(await _mediator.Send(new FindTaskByTitleCommand(projectId, search)));
    }

    [HttpGet]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee, ProjectRole.Customer)]
    public async Task<IActionResult> GetTasks([FromRoute] int projectId, [FromQuery] int? statusId, [FromQuery] int? sprintId)
    {
        TaskFilterDto? filter = null;
        if (statusId != null || sprintId != null)
        {
            filter = new TaskFilterDto()
            {
                StatusId = statusId.Value,
                SprintId = sprintId.Value
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

    [HttpDelete]
    [Route("{taskId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager, ProjectRole.Employee)]
    public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
    {
        return HandleResult(await _mediator.Send(new DeleteTaskCommand(taskId)));
    }
}