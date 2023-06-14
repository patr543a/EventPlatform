using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.DataAccess.Repositories;

public class UserRepository
    : Repository<EventContext, User>
{
    public UserRepository(EventContext context)
        : base(context)
    {
    }

    public IEnumerable<Task> GetUserTaskHistory(string username)
    {
        var user = Get(u => u.UserId == username, null, "TaskIdAssignment,TaskIdAssignment.Event").FirstOrDefault();

        if (user is null)
            return Enumerable.Empty<Task>();

        return user.TaskIdAssignment;
    }

    public PutResult<User, UserDto> UpdateUser(string userId, Action<User> change)
    {
        var user = GetByID(userId);

        if (user is null)
            return new PutResult<User, UserDto>(new User(), Status.NotFound);

        change(user);

        Save();

        return new PutResult<User, UserDto>(user, Status.Success);
    }
}
