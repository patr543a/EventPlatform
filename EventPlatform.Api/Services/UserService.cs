using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;
using EventPlatform.Entities.Enums;
using Task = EventPlatform.Entities.ECP.Task;
using EventPlatform.DataAccess.Classes;

namespace EventPlatform.Api.Services;

public class UserService
    : ServiceBase, IUserService
{
    public IEnumerable<TaskDto> GetUserTaskHistory(string username)
        => from t in _repositories
            .UserRepository
            .GetUserTaskHistory(username)
            select new TaskDto(t);

    public IEnumerable<UserDto> GetCandidatesOfEvent(Guid sessionToken, int eventId)
        => _repositories
            .UserRepository
            .GetCandidatesWithIdentification(sessionToken, new Event() { EventId = eventId });

    public IEnumerable<UserDto> GetVolunteersOfTask(Guid sessionToken, int taskId)
        => _repositories
            .UserRepository
            .GetVolunteersOfTaskWithIdentification(sessionToken, new Task() { TaskId = taskId });

    public IEnumerable<TaskDto> GetTasksUserVolunteerFor(Guid sessionToken, string username, int eventId)
        => _repositories
            .TaskRepository
            .GetTasksUserVolunteerForWithIdentification(sessionToken, username, new Event() { EventId = eventId });

    public PutResult<User, UserDto> UpdateUserDescription(string username, string description)
        => _repositories
            .UserRepository
            .UpdateUser(username, u => u.Description = description);

    public PutResult<User, UserDto> UpdateUserCandidate(string username, int taskId)
    {
        var user = _repositories.UserRepository.GetByID(username);

        if (user is null)
            return new PutResult<User, UserDto>(new User(), Status.NotFound);

        return _repositories.TaskRepository.UpdateTaskCandidate(taskId, user);
    }

    public PutResult<User, UserDto> UpdateUserAssignment(Guid sessionToken, string username, int taskId)
    {
        var user = _repositories.UserRepository.GetByID(username);

        if (user is null)
            return new PutResult<User, UserDto>(new User(), Status.NotFound);

        return _repositories.TaskRepository.UpdateTaskAssignment(sessionToken, taskId, user);
    }

    public DeleteResult<User, UserDto> DeleteUserAssignment(Guid sessionToken, string username, int taskId)
    {
        var user = _repositories.UserRepository.GetByID(username);

        if (user is null)
            return new DeleteResult<User, UserDto>(new User(), Status.NotFound);

        return _repositories.TaskRepository.DeleteTaskAssignment(sessionToken, taskId, user);
    }
}
