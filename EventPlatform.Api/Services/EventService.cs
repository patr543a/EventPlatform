using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Services;

public class EventService
    : ServiceBase, IEventService
{
    public IEnumerable<EventDto> GetEvents(UserType permission)
        => from e in _repositories.EventRepository
           .GetVisibleEvents(permission)
           select new EventDto(e);

    public PostResult<Event, EventDto> AddEvent(Event @event)
        => _repositories
            .EventRepository
            .Add<Event, EventDto, EventContext>(@event);

    public DeleteResult<Event, EventDto> DeleteEvent(Guid sessionToken, Event @event)
        => _repositories
            .EventRepository
            .DeleteWithIdentification(sessionToken, @event);

    public PutResult<Event, EventDto> UpdateEvent(Guid sessionToken, Event @event)
        => _repositories
            .EventRepository
            .UpdateWithIdentification(sessionToken, @event);
}
