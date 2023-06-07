using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.DTO;

public class EventDto
    : IDto<EventDto>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NeedsVolunteers { get; set; }

    public string Organizer { get; set; } = null!;

    public string Description { get; set; } = null!;

    public EventDto(Event @event)
    {
        StartDate = @event.StartDate;
        EndDate = @event.EndDate;
        NeedsVolunteers = @event.NeedsVolunteers;
        Organizer = @event.OrganizerIdFk;
        Description = @event.Description;
    }
}
