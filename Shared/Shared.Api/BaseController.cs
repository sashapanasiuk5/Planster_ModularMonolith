using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Respones;

namespace Shared.Api;

public class BaseController: Controller
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(new SuccessResponse(result.Value));
        }
        return BadRequest( new ErrorResponse(result.Errors.Select(x => x.Message).ToList()));
    }

    protected int GetAuthenticatedUserId()
    {
        var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (userNameClaim == null)
        {
            throw new UnauthorizedAccessException();
        }
        return Int32.Parse(userNameClaim.Value);
    }
}