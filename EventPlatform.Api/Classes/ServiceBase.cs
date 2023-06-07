using EventPlatform.Api.Interfaces;
using EventPlatform.DataAccess.Classes;

namespace EventPlatform.Api.Classes;

public class ServiceBase
{
    protected static readonly UnitOfWork _repositories = new();
}
