using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
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
}
