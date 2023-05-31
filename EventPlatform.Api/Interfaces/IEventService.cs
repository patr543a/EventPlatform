using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Interfaces
{
    public interface IEventService
    {
        LoginResult? GetSessionToken(string username, string password);
        IEnumerable<EventDto> GetEvents(Guid sessionToken);
    }
}
