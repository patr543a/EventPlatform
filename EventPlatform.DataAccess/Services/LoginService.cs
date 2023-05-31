using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Models;
using System.Collections;

namespace EventPlatform.DataAccess.Services
{
    public static class LoginService
    {
        private static readonly Hashtable _tokens = new();
        private static readonly Hashtable _usernames = new();
        private static readonly Hashtable _permissions = new();

        public static LoginResult? Login(Classes.Repositories repositories, string username, string password)
        {
            var user = repositories.UserRepository.GetByID(username);

            if (user is null || user.Password != password)
                return null;

            var guid = (Guid?)_usernames[username] ?? GetGuid();

            _tokens[guid] = username;
            _usernames[username] = guid;
            _permissions[guid] = user.Permissions;

            return new(username, guid);
        }

        public static bool ValidateSessionToken(Guid token)
            => _tokens.ContainsKey(token);

        public static UserType GetUserPermissions(Guid token)
            => (UserType)(_permissions[token] ?? 0);

        private static Guid GetGuid()
        {
            var guid = Guid.NewGuid();

            if (_tokens.ContainsKey(guid))
                return GetGuid();

            return guid;
        }
    }
}
