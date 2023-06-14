using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Interfaces;

public interface IUserService
    : IService
{
    IEnumerable<TaskDto> GetUserTaskHistory(string username);
    PutResult<User, UserDto> UpdateUserDescription(string username, string description);
    PutResult<User, UserDto> UpdateUserCandidate(string username, int taskId);
}
