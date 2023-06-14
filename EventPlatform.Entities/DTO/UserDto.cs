using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.DTO;

public class UserDto
    : IDto<UserDto>
{
    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Permissions { get; set; }

    public UserDto(User user)
    {
        Username = user.UserId;
        Name = user.Name;
        Description = user.Description;
        Permissions = user.Permissions;
    }
}
