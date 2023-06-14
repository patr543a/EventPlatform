using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace EventPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController 
    : CustomController<IEventService>
{
    public EventController(IEventService eventService)
        : base(eventService) 
    {
    }

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents(Guid? sessionToken)
    {
        if (sessionToken is null)
            return BadRequest("Missing parameters");

        var permissions = LoginHandler.GetUserPermissions((Guid)sessionToken);

        if (permissions <= UserType.None)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.GetEvents(permissions)));
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<PostResult<Event, EventDto>>> AddEvent(Guid? sessionToken, Event? @event)
    {
        if (sessionToken is null || @event is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.AddEvent(@event)));
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<DeleteResult<Event, EventDto>>> DeleteEvent(Guid? sessionToken, Event? @event)
    {
        if (sessionToken is null || @event is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.DeleteEvent((Guid)sessionToken, @event)));
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<PutResult<Event, EventDto>>> UpdateEvent(Guid? sessionToken, Event? @event)
    {
        if (sessionToken is null || @event is null)
            return BadRequest("Missing parameters");

        if (LoginHandler.GetUserPermissions((Guid)sessionToken) < UserType.Organizer)
            return Unauthorized("Access denied");

        return Ok(await Task.FromResult(_service.UpdateEvent((Guid)sessionToken, @event)));
    }
}