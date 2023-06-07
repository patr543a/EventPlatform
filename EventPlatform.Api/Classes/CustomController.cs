using EventPlatform.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlatform.Api.Classes;

public class CustomController<TService>
    : ControllerBase
    where TService : IService
{
    protected readonly TService _service;

    public CustomController(TService service)
        => _service = service;
}
