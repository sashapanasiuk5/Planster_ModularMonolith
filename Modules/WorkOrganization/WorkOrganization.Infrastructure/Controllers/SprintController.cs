using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Work.Sprints;
using Teams.Domain.Enums;
using WorkOrganization.Application.Commands.Sprints.CreateSprint;
using WorkOrganization.Application.Commands.Sprints.DeleteSprint;
using WorkOrganization.Application.Commands.Sprints.GetAllSprints;
using WorkOrganization.Application.Commands.Sprints.GetCurrentSprint;
using WorkOrganization.Application.Commands.Sprints.UpdateSprint;

namespace WorkOrganization.Infrastructure.Controllers;

[Route("projects/{projectId}/sprints")]
public class SprintController: BaseController
{
    private readonly IMediator _mediator;

    public SprintController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProjectAuth(ProjectRole.Manager, ProjectRole.Owner)]
    public async Task<IActionResult> CreateSprint([FromBody] CreateSprintDto sprint, [FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new CreateSprintCommand(sprint, projectId)));
    }

    [HttpGet]
    [ProjectAuth(ProjectRole.Customer, ProjectRole.Employee, ProjectRole.Manager, ProjectRole.Owner)]
    public async Task<IActionResult> GetSprints([FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new GetAllSprintsCommand(projectId)));
    }
    
    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrentSprint([FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new GetCurrentSprintCommand(projectId)));
    }

    [HttpPut]
    [Route("{sprintId}")]
    public async Task<IActionResult> UpdateSprint([FromRoute] int sprintId, [FromBody] UpdateSprintDto updateSprintDto)
    {
        return HandleResult(await _mediator.Send(new UpdateSprintCommand(updateSprintDto, sprintId)));
    }

    [HttpDelete]
    [Route("{sprintId}")]
    public async Task<IActionResult> DeleteSprint([FromRoute] int sprintId)
    {
        return HandleResult(await _mediator.Send(new DeleteSprintCommand(sprintId)));
    }
}