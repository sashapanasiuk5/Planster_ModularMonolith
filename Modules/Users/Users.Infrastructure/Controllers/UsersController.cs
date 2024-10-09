using Application.Commands;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Commands.Register;
using Users.Contracts.Dto;

namespace Users.Infrastructure.Controllers;

[Route("users")]
public class UsersController: Controller
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
            var sessionId = result.Value.Id;
            Response.Cookies.Append("SessionID", sessionId);
            return Ok(result.Value);
        }

        return BadRequest();
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "SessionTokens")]
    [Route("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        await Task.Delay(1000);
        return Ok("Very secret endpoint");
    }
}