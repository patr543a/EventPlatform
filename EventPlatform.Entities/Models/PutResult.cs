using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.Models;
public class PutResult<TClass, TDto>
    : DtoResult<TClass, TDto>
    where TClass : class, IDtoConversion<TClass, TDto>
    where TDto : class, IDto<TDto>
{
    public TDto Put => _dto;

    public PutResult(TClass c, Status status)
        : base(c, status)
    {
    }

    public PutResult(TDto dto, Status status)
        : base(dto, status)
    {
    }
}
