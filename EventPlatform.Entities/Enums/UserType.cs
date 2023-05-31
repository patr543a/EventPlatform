namespace EventPlatform.Entities.Enums
{
    [Flags]
    public enum UserType
    {
        None = 0,
        Volunteer = 1,
        Organizer = 2 | Volunteer,
    }
}
