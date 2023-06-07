using EventPlatform.Entities.DTO;

namespace EventPlatform.Api.Interfaces;

public interface IUserService
    : IService
{
    IEnumerable<TaskDto> GetUserTaskHistory(string username, Guid sessionToken);
}
