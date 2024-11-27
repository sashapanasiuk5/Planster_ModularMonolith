using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using WorkOrganization.Application.Commands.Tasks.GetAllStatuses;

namespace WorkOrganization.Infrastructure.Controllers;

public class StatusController: BaseController
{
    private readonly IMediator _mediator;

    public StatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("statuses")]
    public async Task<IActionResult> GetStatuses()
    {
        return Ok(await _mediator.Send(new GetAllStatusesCommand()));
    }
}