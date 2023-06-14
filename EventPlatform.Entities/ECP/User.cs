using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.ECP;

public partial class User
    : IDtoConversion<User, UserDto>
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Permissions { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Task> TaskIdAssignment { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskIdCandidate { get; set; } = new List<Task>();

    public UserDto ToDto()
        => new(this);
}
