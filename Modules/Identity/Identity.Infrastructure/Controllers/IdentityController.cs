using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Commands;
using Application.Commands.Logout;
using Application.Commands.RefreshToken;
using Application.Commands.SwitchCurrentProject;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Respones;
using Shared.Contracts.Constants;

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

    [HttpPatch]
    [Authorize(AuthenticationSchemes = "SimpleAuth")]
    [Route("currentProject/{projectId}")]
    public async Task<IActionResult> UpdateCurrentProject(int projectId)
    {
        var identityIdString = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value;
        var identityId = int.Parse(identityIdString);
        var result = await _mediator.Send(new SwitchCurrentProjectCommand(identityId, projectId));
        if (result.IsSuccess)
        {
            return Ok(new SuccessResponse(result.Value));
        }
        return Unauthorized( new ErrorResponse(result.Errors.Select(x => x.Message).ToList()));
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshDto dto)
    {
        if (!Request.Headers.ContainsKey("Authorization") || !Request.Headers.ContainsKey("Authorization-Project"))
        {
            return BadRequest();
        }
        var userTokenHeader = Request.Headers["Authorization"][0];
        var projectTokenHeader = Request.Headers["Authorization-Project"][0];

        var userToken = userTokenHeader.Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var userSecurityToken = handler.ReadToken(userToken) as JwtSecurityToken;
        var identityId = int.Parse(userSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name).Value);

        int? projectId = null;
        if (projectTokenHeader != null)
        {
            var projectToken = projectTokenHeader.Replace("Bearer ", "");
            var projectSecurityToken = handler.ReadToken(projectToken) as JwtSecurityToken;
            projectId = int.Parse(projectSecurityToken.Claims.FirstOrDefault(c => c.Type == TokenConstants.PROJECT_ID).Value);
        }

        var result = await _mediator.Send(new RefreshTokenCommand(dto.Token, identityId, projectId));
        if (result.IsSuccess)
        {
            return Ok(new SuccessResponse(result.Value));
        }
        return Unauthorized( new ErrorResponse(result.Errors.Select(x => x.Message).ToList()));
    }
    
}