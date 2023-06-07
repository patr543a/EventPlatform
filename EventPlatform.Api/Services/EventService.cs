using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Services;

public class EventService
    : ServiceBase, IEventService
{
    public IEnumerable<EventDto> GetEvents(Guid sessionToken)
        => from e in _repositories.EventRepository
           .GetVisibleEvents(LoginHandler.GetUserPermissions(sessionToken))
           select new EventDto(e);

    public PostResult<Event, EventDto> AddEvent(Guid sessionToken, Event @event)
        => _repositories
            .EventRepository
            .Add<Event, EventDto, EventContext>(sessionToken, @event);

    public DeleteResult<Event, EventDto> DeleteEvent(Guid sessionToken, Event @event)
        => _repositories
            .EventRepository
            .Delete<Event, EventDto, EventContext>(sessionToken, @event);

    public PutResult<Event, EventDto> UpdateEvent(Guid sessionToken, Event @event)
        => _repositories
            .EventRepository
            .Update<Event, EventDto, EventContext>(sessionToken, @event);
}
