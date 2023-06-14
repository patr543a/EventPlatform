using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
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
}
