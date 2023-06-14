using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;
using EventPlatform.Entities.Models;
using Microsoft.Extensions.Logging;

namespace EventPlatform.DataAccess.Repositories;

public class EventRepository
    : Repository<EventContext, Event>
{
    public EventRepository(EventContext context) 
        : base(context) 
    {
    }

    public IEnumerable<Event> GetVisibleEvents(UserType userType)
        => userType switch
        {
            <= UserType.Volunteer => Get(e => e.NeedsVolunteers),
            >= UserType.Organizer => Get(),
        };

    public PutResult<Event, EventDto> UpdateWithIdentification(Guid userId, Event @event)
        => this.UpdateWithIdentification<Event, EventDto, EventContext>(
            userId,
            @event,
            e => e.EventId ?? -1,
            (e, s) => e.OrganizerIdFk == s
        );

    public DeleteResult<Event, EventDto> DeleteWithIdentification(Guid userId, Event @event)
        => this.DeleteWithIdentification<Event, EventDto, EventContext>(
            userId,
            @event,
            e => e.EventId ?? -1,
            (e, s) => e.OrganizerIdFk == s
        );
}
