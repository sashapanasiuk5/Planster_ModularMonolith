using System.Security.Claims;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Teams;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Commands.AcceptInvitation;
using Teams.Application.Commands.CreateInvitation;
using Teams.Application.Commands.CreateProject;
using Teams.Application.Commands.DeleteInvitation;
using Teams.Application.Commands.DeleteMember;
using Teams.Application.Commands.GetInvitations;
using Teams.Application.Commands.GetMembers;
using Teams.Application.Commands.UpdateProject;
using Teams.Domain.Enums;
using Teams.Domain.Models;

namespace Teams.Infrastructure.Controllers;

public class ProjectsController: BaseController
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Authorize]
    [Route("projects")]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateUpdateProjectDto project)
    {
        var userId = GetAuthenticatedUserId();
        return HandleResult(await _mediator.Send(new CreateProjectCommand(project, userId)));
    }

    [HttpPut]
    [Authorize]
    [Route("projects/{projectId}")]
    [ProjectAuth(ProjectRole.Owner)]
    public async Task<IActionResult> UpdateProjectAsync(int projectId, [FromBody] CreateUpdateProjectDto project)
    {
        return HandleResult(await _mediator.Send(new UpdateProjectCommand(projectId, project)));
    }
}