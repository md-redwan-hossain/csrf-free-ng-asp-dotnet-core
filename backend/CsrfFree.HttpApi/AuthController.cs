using Microsoft.AspNetCore.Mvc;

namespace CsrfFree.HttpApi;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    [ValidateAngularAntiForgeryToken]
    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new { userName = "John Doe" });
    }

    [HttpPost("load-csrf-token")]
    public IActionResult LoadCsrfToken()
    {
        return Ok();
    }
}