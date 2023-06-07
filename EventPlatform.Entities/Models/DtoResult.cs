using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;
using System.Text.Json.Serialization;

namespace EventPlatform.Entities.Models;

public abstract class DtoResult<TClass, TDto>
    where TClass : class, IDtoConversion<TClass, TDto>
    where TDto : class, IDto<TDto>
{
    protected readonly TDto _dto;
    protected readonly Status _status;

    [JsonIgnore]
    public TDto Dto => _dto;
    public Status Status => _status;

    public DtoResult(TClass c, Status status)
    {
        _dto = c.ToDto();
        _status = status;
    }

    public DtoResult(TDto dto, Status status)
    {
        _dto = dto;
        _status = status;
    }
}