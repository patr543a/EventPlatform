using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;
using TaskECP = EventPlatform.Entities.ECP.Task;

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
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetUserTaskHistory(Guid? sessionToken, string? username)
    {
        if (username is null || sessionToken is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetUserTaskHistory(username)));
    }

    [HttpPut]
    [Route("updateUserDescription")]
    public async Task<ActionResult<PutResult<User, UserDto>>> UpdateUserDescription(Guid? sessionToken, string? description)
    {
        if (sessionToken is null || description is null)
            return BadRequest("Missing parameters");

        var username = LoginHandler.GetUsername((Guid)sessionToken);

        if (username is null)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.UpdateUserDescription(username, description)));
    }

    [HttpPut]
    [Route("updateUserCandidate")]
    public async Task<ActionResult<PutResult<User, UserDto>>> UpdateUserCandidate(Guid? sessionToken, int? taskId)
    {
        if (sessionToken is null || taskId is null)
            return BadRequest("Missing parameters");

        var username = LoginHandler.GetUsername((Guid)sessionToken);

        if (username is null)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.UpdateUserCandidate(username, (int)taskId)));
    }
}
