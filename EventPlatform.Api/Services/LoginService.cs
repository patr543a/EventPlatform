using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;
using EventPlatform.Entities.Models;

namespace EventPlatform.Api.Services;

public class LoginService
    : ServiceBase, ILoginService
{
    public LoginResult? GetSessionToken(string username, string password)
        => LoginHandler.Login(_repositories, username, password);
}
