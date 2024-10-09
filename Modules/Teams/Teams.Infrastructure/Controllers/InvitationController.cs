using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Commands.CreateInvitation;
using Teams.Application.Commands.DeleteInvitation;
using Teams.Application.Commands.GetInvitations;
using Teams.Domain.Enums;

namespace Teams.Infrastructure.Controllers;

public class InvitationController: BaseController
{
    private readonly IMediator _mediator;

    public InvitationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("projects/{projectId}/invitations")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> GetInvitationsAsync([FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new GetInvitationsQuery(projectId)));
    }

    [HttpPost]
    [Route("projects/{projectId}/invitations")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationDto invitation, [FromRoute] int projectId)
    {
        return HandleResult(await _mediator.Send(new CreateInvitationCommand(invitation, projectId)));
    }

    [HttpDelete]
    [Route("projects/{projectId}/invitations/{invitationId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> DeleteInvitation([FromRoute] int projectId, [FromRoute] int invitationId)
    {
        return HandleResult(await _mediator.Send(new DeleteInvitationCommand(projectId, invitationId)));
    }
}