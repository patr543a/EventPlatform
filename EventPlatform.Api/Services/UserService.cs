using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;

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
}
