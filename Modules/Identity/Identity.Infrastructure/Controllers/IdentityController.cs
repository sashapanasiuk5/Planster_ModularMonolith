using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

public class IdentityController: ControllerBase
{

    [Route("login")]
    public IActionResult Login()
    {
        return Ok("Hello world!");
    }
}