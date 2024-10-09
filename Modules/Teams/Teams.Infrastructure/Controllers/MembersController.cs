using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Commands.AcceptInvitation;
using Teams.Application.Commands.DeleteMember;
using Teams.Application.Commands.GetMemberProjects;
using Teams.Application.Commands.GetMembers;
using Teams.Domain.Enums;

namespace Teams.Infrastructure.Controllers;

public class MembersController: BaseController
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [Route("members/{memberId}/projects")]
    public async Task<IActionResult> GetMemberProjects([FromRoute] int memberId)
    {
        return HandleResult(await _mediator.Send(new GetMemberProjectsCommand(memberId)));
    }
    
    [HttpPut]
    [Authorize]
    [Route("projects/{projectId}/members")]
    public async Task<IActionResult> AcceptInvitation([FromBody] AcceptInvitationDto acceptDto, [FromRoute] int projectId)
    {
        var memberId = GetAuthenticatedUserId();
        return HandleResult(await _mediator.Send(new AcceptInvitationCommand(acceptDto, projectId, memberId)));
    }

    [HttpDelete]
    [ProjectAuth(ProjectRole.Owner)]
    [Route("projects/{projectId}/members/{memberId}")]
    public async Task<IActionResult> DeleteMember([FromRoute] int projectId, [FromRoute] int memberId)
    {
        return HandleResult(await _mediator.Send(new DeleteMemberCommand(memberId, projectId)));
    }

    [HttpGet]
    [Route("projects/{projectId}/members")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Employee, ProjectRole.Customer, ProjectRole.Manager)]
    public async Task<IActionResult> GetMembersAsync([FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new GetMembersQuery(projectId)));
    }
}