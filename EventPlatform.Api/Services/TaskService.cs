using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Services;

public class TaskService
    : ServiceBase, ITaskService
{
    public IEnumerable<TaskDto> GetTasksFromEvent(int eventId)
        => _repositories
            .TaskRepository
            .Get(t => t.Event!.EventId == eventId, null, "Event")
            .Select(t => t.ToDto());

    public PostResult<Task, TaskDto> AddTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .AddWithIdentification(sessionToken, task);

    public DeleteResult<Task, TaskDto> DeleteTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .DeleteWithIdentification(sessionToken, task);

    public PutResult<Task, TaskDto> UpdateTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .UpdateWithIdentification(sessionToken, task);
}
