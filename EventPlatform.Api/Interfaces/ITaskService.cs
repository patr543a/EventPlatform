using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Interfaces;

public interface ITaskService
    : IService
{
    IEnumerable<TaskDto> GetTasksFromEvent(int eventId);
    PostResult<Task, TaskDto> AddTask(Guid sessionToken, Task task);
    DeleteResult<Task, TaskDto> DeleteTask(Guid sessionToken, Task task);
    PutResult<Task, TaskDto> UpdateTask(Guid sessionToken, Task task);
}
