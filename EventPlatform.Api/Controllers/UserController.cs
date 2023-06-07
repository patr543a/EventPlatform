using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController 
    : CustomController<IUserService>
{
    public UserController(IUserService userService)
        : base(userService)
    {
    }

    [HttpGet]
    [Route("getUserTaskHistory")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetUserTaskHistory(string? username, Guid? sessionToken)
    {
        if (username is null || sessionToken is null)
            return BadRequest("Missing parameters");

        return Ok(await Task.FromResult(_service.GetUserTaskHistory(username, (Guid)sessionToken)));
    }
}
