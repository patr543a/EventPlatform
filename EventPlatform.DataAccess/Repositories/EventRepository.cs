using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;

namespace EventPlatform.DataAccess.Repositories
{
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
                UserType.Volunteer => Get(e => e.NeedsVolunteers),
                UserType.Organizer => Get(),
                _ => new List<Event>(),
            };
    }
}
