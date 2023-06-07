using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;

namespace EventPlatform.DataAccess.Repositories;

public class EventRepository
    : Repository<EventContext, Event>
{
    public EventRepository(EventContext context) 
        : base(context) 
    {
    }

    public IEnumerable<Event> GetVisibleEvents(UserTypes userType)
        => userType switch
        {
            UserTypes.Volunteer => Get(e => e.NeedsVolunteers),
            UserTypes.Organizer => Get(),
            _ => Enumerable.Empty<Event>(),
        };
}
