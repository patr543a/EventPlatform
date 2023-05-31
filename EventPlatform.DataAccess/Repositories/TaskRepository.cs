using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;

namespace EventPlatform.DataAccess.Repositories
{
    public class TaskRepository
        : Repository<EventContext, Entities.ECP.Task>
    {
        public TaskRepository(EventContext context)
            : base(context)
        {
        }
    }
}
