using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;

namespace EventPlatform.DataAccess.Repositories;

public class UserRepository
    : Repository<EventContext, User>
{
    public UserRepository(EventContext context)
        : base(context)
    {
    }

    public IEnumerable<Entities.ECP.Task> GetUserTaskHistory(string username, Guid sessionToken)
    {
        if (LoginHandler.GetUserPermissions(sessionToken) == UserTypes.None)
            return Enumerable.Empty<Entities.ECP.Task>();

        var user = Get(u => u.UserId == username, null, "TaskIdAssignment,TaskIdAssignment.Event").FirstOrDefault();

        if (user is null)
            return Enumerable.Empty<Entities.ECP.Task>();

        return user.TaskIdAssignment;
    }

    public User? GetUserById(string username)
        => GetByID(username);
}
