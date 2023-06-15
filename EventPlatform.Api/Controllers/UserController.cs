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
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetUserTaskHistory(Guid? sessionToken)
    {
        if (sessionToken is null)
            return BadRequest("Missing parameters");

        var username = LoginHandler.GetUsername((Guid)sessionToken);

        if (username is null)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetUserTaskHistory(username)));
    }

    [HttpGet]
    [Route("getCandidatesOfEvent")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetCandidatesOfEvent(Guid? sessionToken, int? eventId)
    {
        if (sessionToken is null || eventId is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetCandidatesOfEvent((Guid)sessionToken, (int)eventId)));
    }

    [HttpGet]
    [Route("getVolunteersOfTask")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetVolunteersOfTask(Guid? sessionToken, int? taskId)
    {
        if (sessionToken is null || taskId is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetVolunteersOfTask((Guid)sessionToken, (int)taskId)));
    }

    [HttpGet]
    [Route("getTasksUserVolunteerFor")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksUserVolunteerFor(Guid? sessionToken, string? username, int? eventId)
    {
        if (sessionToken is null || username is null || eventId is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetTasksUserVolunteerFor((Guid)sessionToken, username, (int)eventId)));
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

    [HttpPut]
    [Route("updateUserAssignment")]
    public async Task<ActionResult<PutResult<User, UserDto>>> UpdateUserAssignment(Guid? sessionToken, string? username, int? taskId)
    {
        if (sessionToken is null || username is null || taskId is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.UpdateUserAssignment((Guid)sessionToken, username, (int)taskId)));
    }

    [HttpDelete]
    [Route("deleteUserAssignment")]
    public async Task<ActionResult<DeleteResult<User, UserDto>>> DeleteUserAssignment(Guid? sessionToken, string? username, int? taskId)
    {
        if (sessionToken is null || username is null || taskId is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.DeleteUserAssignment((Guid)sessionToken, username, (int)taskId)));
    }
}
