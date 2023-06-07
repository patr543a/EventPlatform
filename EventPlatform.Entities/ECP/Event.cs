using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.ECP;

public partial class Event
    : IDtoConversion<Event, EventDto>
{
    public int? EventId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NeedsVolunteers { get; set; }

    public string OrganizerIdFk { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual User? Organizer { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public EventDto ToDto()
        => new(this);
}
