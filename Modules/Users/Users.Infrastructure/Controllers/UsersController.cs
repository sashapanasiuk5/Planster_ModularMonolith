using Infrastructure.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Commands.Register;

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
    public async Task<IActionResult> Register(NewUserDto user)
    {
        var result = await _mediator.Send(new RegisterCommand(user));
        return Ok(result.Value);
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