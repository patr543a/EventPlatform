using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController 
        : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
            => _eventService = eventService;

        [HttpGet]
        [Route("getToken")]
        public async Task<ActionResult<LoginResult>> GetSessionToken(string? username, string? password)
        {
            if (username is null ||  password is null)
                return BadRequest("Missing parameters");

            var result = await Task.FromResult(_eventService.GetSessionToken(username, password));

            if (result is null)
                return Unauthorized("Invalid login");

            return Ok(result);
        }

        [HttpGet]
        [Route("getEvents")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents(Guid sessionToken)
            => Ok(await Task.FromResult(_eventService.GetEvents(sessionToken)));

        [HttpGet]
        [Route("getUserTaskHistory")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetUserTaskHistory(string? username, Guid? sessionToken)
        {

        }
    }
}