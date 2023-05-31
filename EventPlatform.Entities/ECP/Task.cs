namespace EventPlatform.Entities.ECP;

public partial class Task
{
    public int TaskId { get; set; }

    public string Description { get; set; } = null!;

    public int EventIdFk { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<User> VolunteerIdAssignment { get; set; } = new List<User>();

    public virtual ICollection<User> VolunteerIdCandidate { get; set; } = new List<User>();
}
