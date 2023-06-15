using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;
using EventPlatform.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    public IEnumerable<UserDto> GetCandidatesWithIdentification(Guid sessionToken, Event @event)
    {
        var username = LoginHandler.GetUsername(sessionToken);

        if (username is null)
            return Array.Empty<UserDto>();

        var ev = _context.Events.Find(@event.EventId);

        if (ev is not null)
            Detach(ev);
        else
            return Array.Empty<UserDto>();

        if (ev.OrganizerIdFk != username)
            return Array.Empty<UserDto>();

        return Get(
            u => u.TaskIdCandidate.Where(t => t.EventIdFk == @event.EventId).Any(),
            null,
            "TaskIdCandidate")
                .Select(i => i.ToDto());
    }

    public IEnumerable<UserDto> GetVolunteersOfTaskWithIdentification(Guid sessionToken, Task task)
    {
        var username = LoginHandler.GetUsername(sessionToken);

        if (username is null)
            return Array.Empty<UserDto>();

        var ev = _context.Tasks.Include("Event").First(t => t.TaskId == task.TaskId);

        if (ev is not null)
            Detach(ev);
        else
            return Array.Empty<UserDto>();

        if ((ev.Event ?? new()).OrganizerIdFk != username)
            return Array.Empty<UserDto>();
        return Get(
            u => u.TaskIdCandidate.Where(t => t.TaskId == task.TaskId).Any(),
            null,
            "TaskIdCandidate")
                .Select(i => i.ToDto());
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
