using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.DataAccess.Services;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Services
{
    public class EventService
        : IEventService
    {
        private readonly Repositories _repositories = new();

        public LoginResult? GetSessionToken(string username, string password)
            => LoginService.Login(_repositories, username, password);

        public IEnumerable<EventDto> GetEvents(Guid sessionToken)
            => from e in _repositories.EventRepository
               .GetVisibleEvents(LoginService.GetUserPermissions(sessionToken))
               select new EventDto()
               {
                   StartDate = e.StartDate,
                   EndDate = e.EndDate,
                   NeedsVolunteers = e.NeedsVolunteers,
                   Organizer = e.OrganizerIdFk,
                   Description = e.Description,
               };
    }
}
