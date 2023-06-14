using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;
using EventPlatform.Entities.Enums;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Services;

public class UserService
    : ServiceBase, IUserService
{
    public IEnumerable<TaskDto> GetUserTaskHistory(string username)
        => from t in _repositories
            .UserRepository
            .GetUserTaskHistory(username)
            select new TaskDto(t);

    public PutResult<User, UserDto> UpdateUserDescription(string username, string description)
        => _repositories
            .UserRepository
            .UpdateUser(username, u => u.Description = description);

    public PutResult<User, UserDto> UpdateUserCandidate(string username, int taskId)
    {
        var user = _repositories.UserRepository.Get(u => u.UserId == username, null, "TaskIdCandidate").FirstOrDefault();

        if (user is null)
            return new PutResult<User, UserDto>(new User(), Status.NotFound);

        return _repositories.TaskRepository.UpdateTaskCandidate(taskId, user);
    }
}
