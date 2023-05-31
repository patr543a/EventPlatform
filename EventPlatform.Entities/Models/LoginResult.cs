using EventPlatform.Entities.ECP;

namespace EventPlatform.Entities.Models
{
    public class LoginResult
    {
        public string Username { get; set; } = null!;
        public Guid SessionToken { get; set; }

        public LoginResult(string username, Guid sessionToken)
        {
            Username = username;
            SessionToken = sessionToken;
        }
    }
}
