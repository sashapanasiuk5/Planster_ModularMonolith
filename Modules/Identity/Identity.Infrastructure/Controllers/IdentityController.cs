using Application.Commands;
using Application.Commands.Logout;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Respones;

namespace Infrastructure.Controllers;

[Route("identity")]
public class IdentityController: BaseController
{
    private readonly IMediator _mediator;  
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("session")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));
        if (result.IsSuccess)
        {
            var sessionId = result.Value.Id;
            Response.Cookies.Append("SessionID", sessionId);
            return Ok(new SuccessResponse(result.Value));
        }
        return BadRequest("Login Failed");
    }

    [HttpDelete]
    [Route("session")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = GetAuthenticatedUserId();
        var sessionId = Request.Cookies["SessionID"];
        if (sessionId != null)
        {
            await _mediator.Send(new LogoutCommand(sessionId, userId)); 
        }
        return NoContent();
    }
    
}