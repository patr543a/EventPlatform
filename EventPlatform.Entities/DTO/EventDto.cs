namespace EventPlatform.Entities.DTO
{
    public class EventDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NeedsVolunteers { get; set; }

        public string Organizer { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
