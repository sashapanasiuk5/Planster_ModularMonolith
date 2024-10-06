using Application.Commands;
using Application.Commands.Logout;
using Infrastructure.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[Route("identity")]
public class IdentityController: ControllerBase
{
    private readonly IMediator _mediator;  
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("session")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));
        if (result.IsSuccess)
        {
            var sessionId = result.Value.SessionId;
            Response.Cookies.Append("SessionID", sessionId);
            return Ok(result.Value);
        }
        return BadRequest("Login Failed");
    }

    [Route("session")]
    [HttpDelete]
    public async Task<IActionResult> Logout()
    {
        var sessionId = Request.Cookies["SessionID"];
        if (sessionId != null)
        {
            await _mediator.Send(new LogoutCommand(sessionId)); 
        }
        return NoContent();
    }
    
}