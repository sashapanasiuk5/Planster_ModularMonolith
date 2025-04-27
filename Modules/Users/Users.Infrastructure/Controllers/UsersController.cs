using System.Security.Claims;
using Application.Commands;
using Identity.Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Shared.Api.Extensions;
using Shared.Api.Respones;
using User.Application.Commands.GetUserById;
using User.Application.Commands.GetUserPhoto;
using User.Application.Commands.Register;
using User.Application.Commands.UploadProfilePhoto;
using Users.Contracts.Dto;
using Users.Domain.Enums;

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
    [Authorize]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId, [FromQuery] bool withDetails = false)
    {
        return HandleResult(await _mediator.Send(new GetUserByIdCommand(userId, withDetails)));
    }

    [HttpPost]
    [Authorize]
    [Route("{userId}/photo")]
    public async Task<IActionResult> UploadPhotoById([FromRoute] int userId, [FromQuery] string mimeType, [FromQuery] string fileName)
    {
        var authorizedUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
        if(userId.ToString() != authorizedUserId)
            return Forbid();
        var photo = await Request.GetRawBodyBytesAsync();
        return HandleResult(await _mediator.Send(new UploadProfilePhotoCommand(userId, new FileDto()
        {
            Data = photo,
            FileName = fileName,
            MimeType = mimeType
        })));
    }
    
    [HttpGet]
    [Authorize]
    [Route("{userId}/photo")]
    public async Task<IActionResult> GetPhotoById([FromRoute] int userId, [FromQuery] string size)
    {
        PhotoResolution resolution;
        switch (size)
        {
            case "normal":
                resolution = PhotoResolution.Normal;
                break;
            case "icon":
                resolution = PhotoResolution.Low;
                break;
            default:
                throw new Exception($"Unsupported PhotoResolution: {size}");
        }
        
        var fileResult = await _mediator.Send(new GetUserPhotoCommand(userId, resolution));
        if (fileResult.IsSuccess)
        {
            return File(fileResult.Value.Data, fileResult.Value.MimeType, fileResult.Value.FileName);
        }
        return NotFound(new ErrorResponse(fileResult.Errors.Select(x =>x.Message).ToList()));
    }
}