using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.ECP;

namespace EventPlatform.DataAccess.Repositories
{
    public class UserRepository
        : Repository<EventContext, User>
    {
        public UserRepository(EventContext context)
            : base(context)
        {
        }
    }
}
