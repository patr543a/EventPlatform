using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Interfaces;

public interface IUserService
    : IService
{
    IEnumerable<TaskDto> GetUserTaskHistory(string username);
    PutResult<User, UserDto> UpdateUserDescription(string username, string description);
}
