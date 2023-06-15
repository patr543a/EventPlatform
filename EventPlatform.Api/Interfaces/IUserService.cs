using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Models;
using Task = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Interfaces;

public interface IUserService
    : IService
{
    IEnumerable<TaskDto> GetUserTaskHistory(string username);
    IEnumerable<UserDto> GetCandidatesOfEvent(Guid sessionToken, int eventId);
    IEnumerable<UserDto> GetVolunteersOfTask(Guid sessionToken, int taskId);
    IEnumerable<TaskDto> GetTasksUserVolunteerFor(Guid sessionToken, string username, int eventId);
    PutResult<User, UserDto> UpdateUserDescription(string username, string description);
    PutResult<User, UserDto> UpdateUserCandidate(string username, int taskId);
    PutResult<User, UserDto> UpdateUserAssignment(Guid sessionToken, string username, int taskId);
    DeleteResult<User, UserDto> DeleteUserAssignment(Guid sessionToken, string username, int taskId);
}
