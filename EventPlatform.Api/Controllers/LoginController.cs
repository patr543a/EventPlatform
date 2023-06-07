using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController 
    : CustomController<ILoginService>
{
    public LoginController(ILoginService loginService)
        : base(loginService)
    {
    }

    [HttpGet]
    [Route("getToken")]
    public async Task<ActionResult<LoginResult>> GetSessionToken(string? username, string? password)
    {
        if (username is null || password is null)
            return BadRequest("Missing parameters");

        var result = await Task.FromResult(_service.GetSessionToken(username, password));

        if (result is null)
            return Unauthorized("Invalid login");

        return Ok(result);
    }
}
