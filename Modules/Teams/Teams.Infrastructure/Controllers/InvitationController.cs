using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Attributes;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Commands.CreateInvitation;
using Teams.Application.Commands.DeleteInvitation;
using Teams.Application.Commands.GetInvitationByCode;
using Teams.Application.Commands.GetInvitations;
using Teams.Application.Commands.UpdateInvitation;
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

    [HttpGet]
    [Route("projects/{projectId}/invitations/{code}")]
    public async Task<IActionResult> GetInvitation([FromRoute] int projectId, string code)
    {
        return HandleResult(await _mediator.Send(new GetInvitationByCodeQuery(projectId, code)));
    }

    [HttpPost]
    [Route("projects/{projectId}/invitations")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateUpdateInvitationDto invitation, [FromRoute] int projectId)
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

    [HttpPut]
    [Route("projects/{projectId}/invitations/{invitationId}")]
    [ProjectAuth(ProjectRole.Owner, ProjectRole.Manager)]
    public async Task<IActionResult> EditInvitation([FromRoute] int projectId, [FromBody] CreateUpdateInvitationDto invitation, [FromRoute] int invitationId)
    {
        return HandleResult(await _mediator.Send(new UpdateInvitationCommand(projectId, invitationId, invitation)));
    }
}