using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Interfaces;

public interface IEventService
    : IService
{
    IEnumerable<EventDto> GetEvents(Guid sessionToken);
    PostResult<Event, EventDto> AddEvent(Guid sessionToken, Event @event);
    DeleteResult<Event, EventDto> DeleteEvent(Guid sessionToken, Event @event);
    PutResult<Event, EventDto> UpdateEvent(Guid sessionToken, Event @event);
}
