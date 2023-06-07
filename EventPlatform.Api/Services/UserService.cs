using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;

namespace EventPlatform.Api.Services;

public class UserService
    : ServiceBase, IUserService
{
    public IEnumerable<TaskDto> GetUserTaskHistory(string username, Guid sessionToken)
        => from t in _repositories.UserRepository
            .GetUserTaskHistory(username, sessionToken)
            select new TaskDto(t);
}
