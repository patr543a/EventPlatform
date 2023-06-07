using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Interfaces;

public interface ILoginService
    : IService
{
    LoginResult? GetSessionToken(string username, string password);
}
