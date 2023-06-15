using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
using Microsoft.Extensions.Logging;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.DataAccess.Repositories;

public class TaskRepository
    : Repository<EventContext, Task>
{
    public TaskRepository(EventContext context)
        : base(context)
    {
    }

    public PostResult<Task, TaskDto> AddWithIdentification(Guid userId, Task task)
        => this.AddWithIdentification<Task, TaskDto, EventContext>(
            userId,
            task,
            t => t.TaskId ?? -1,
            (t, s) => t.Event?.OrganizerIdFk == s
        );

    public PutResult<Task, TaskDto> UpdateWithIdentification(Guid userId, Task task)
        => this.UpdateWithIdentification<Task, TaskDto, EventContext>(
            userId,
            task,
            t => t.TaskId ?? -1,
            (t, s) => t.Event?.OrganizerIdFk == s
        );

    public DeleteResult<Task, TaskDto> DeleteWithIdentification(Guid userId, Task task)
        => this.DeleteWithIdentification<Task, TaskDto, EventContext>(
            userId,
            task,
            t => t.TaskId ?? -1,
            (t, s) => t.Event?.OrganizerIdFk == s
        );

    public PutResult<User, UserDto> UpdateTaskCandidate(int taskId, User user)
    {
        var task = Get(t => t.TaskId == taskId, null, "VolunteerIdCandidate").FirstOrDefault();

        if (task is null)
            return new PutResult<User, UserDto>(user, Status.NotFound);

        if (task.VolunteerIdCandidate!.Where(u => u.UserId == user.UserId).Any())
            return new PutResult<User, UserDto>(user, Status.AlreadyPresent);
        
        task.VolunteerIdCandidate!.Add(user);

        Save();

        return new PutResult<User, UserDto>(user, Status.Success);
    }

    public PutResult<User, UserDto> UpdateTaskAssignment(Guid sessionToken, int taskId, User user)
    {
        var username = LoginHandler.GetUsername(sessionToken);

        if (username is null)
            return new(user, Status.AccessDenied);

        var task = Get(t => t.TaskId == taskId, null, "Event,VolunteerIdAssignment,VolunteerIdCandidate").FirstOrDefault();

        if (task is null)
            return new(user, Status.NotFound);

        if (task.Event?.OrganizerIdFk != username)
            return new(user, Status.AccessDenied);

        if (task.VolunteerIdAssignment!.Where(u => u.UserId == user.UserId).Any())
            return new PutResult<User, UserDto>(user, Status.AlreadyPresent);

        if (!task.VolunteerIdCandidate!.Where(u => u.UserId == user.UserId).Any())
            return new PutResult<User, UserDto>(user, Status.NotFound);

        task.VolunteerIdAssignment!.Add(user);

        Save();

        return new PutResult<User, UserDto>(user, Status.Success);
    }

    public DeleteResult<User, UserDto> DeleteTaskAssignment(Guid sessionToken, int taskId, User user)
    {
        var username = LoginHandler.GetUsername(sessionToken);

        if (username is null)
            return new(user, Status.AccessDenied);

        var task = Get(t => t.TaskId == taskId, null, "Event,VolunteerIdAssignment").FirstOrDefault();

        if (task is null)
            return new(user, Status.NotFound);

        if (task.Event?.OrganizerIdFk != username)
            return new(user, Status.AccessDenied);

        if (!task.VolunteerIdAssignment!.Where(u => u.UserId == user.UserId).Any())
            return new DeleteResult<User, UserDto>(user, Status.NotFound);

        task.VolunteerIdAssignment!.Remove(user);

        Save();

        return new DeleteResult<User, UserDto>(user, Status.Success);
    }

    public IEnumerable<TaskDto> GetTasksUserVolunteerForWithIdentification(Guid sessionToken, string user, Event @event)
    {
        var username = LoginHandler.GetUsername(sessionToken);

        if (username is null)
            return Array.Empty<TaskDto>();

        var ev = _context.Events.Find(@event.EventId);

        if (ev is not null)
            Detach(ev);
        else
            return Array.Empty<TaskDto>();

        if (ev.OrganizerIdFk != username)
            return Array.Empty<TaskDto>();

        return Get(
            t => t.EventIdFk == @event.EventId && t.VolunteerIdCandidate!.Where(u => u.UserId == user).Any(),
            null,
            "VolunteerIdCandidate")
                .Select(i => i.ToDto());
    }
}
