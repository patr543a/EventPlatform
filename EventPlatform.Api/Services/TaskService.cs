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
    public PostResult<Task, TaskDto> AddTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .Add<Task, TaskDto, EventContext>(sessionToken, task);

    public DeleteResult<Task, TaskDto> DeleteTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .Delete<Task, TaskDto, EventContext>(sessionToken, task);

    public PutResult<Task, TaskDto> UpdateTask(Guid sessionToken, Task task)
        => _repositories
            .TaskRepository
            .Update<Task, TaskDto, EventContext>(sessionToken, task);
}
