using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Interfaces;

public interface IEventService
    : IService
{
    IEnumerable<EventDto> GetEvents(UserType permission);
    PostResult<Event, EventDto> AddEvent(Event @event);
    DeleteResult<Event, EventDto> DeleteEvent(Guid sessionToken, Event @event);
    PutResult<Event, EventDto> UpdateEvent(Guid sessionToken, Event @event);
}
