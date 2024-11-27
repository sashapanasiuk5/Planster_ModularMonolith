using Application.Commands;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Respones;
using User.Application.Commands.GetUserById;
using User.Application.Commands.Register;
using Users.Contracts.Dto;

namespace Users.Infrastructure.Controllers;

[Route("users")]
public class UsersController: BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] NewUserDto user)
    {
        var result = await _mediator.Send(new RegisterCommand(user));
        if (result.IsSuccess)
        {
            var sessionId = result.Value.Session.Id;
            Response.Cookies.Append("SessionID", sessionId);
            return Ok(new SuccessResponse(result.Value));
        }

        return BadRequest();
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        return HandleResult(await _mediator.Send(new GetUserByIdCommand(userId)));
    }
}