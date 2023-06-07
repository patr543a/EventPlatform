using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.Models;

public class PostResult<TClass, TDto>
    : DtoResult<TClass, TDto>
    where TClass : class, IDtoConversion<TClass, TDto>
    where TDto : class, IDto<TDto>
{
    public TDto Post => _dto;

    public PostResult(TClass c, Status status)
        : base(c, status)
    {
    }

    public PostResult(TDto dto, Status status)
        : base(dto, status)
    {
    }
}
