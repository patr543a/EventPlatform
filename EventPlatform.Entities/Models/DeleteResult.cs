using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.Models;
public class DeleteResult<TClass, TDto>
    : DtoResult<TClass, TDto>
    where TClass : class, IDtoConversion<TClass, TDto>
    where TDto : class, IDto<TDto>
{
    public TDto Delete => _dto;

    public DeleteResult(TClass c, Status status)
        : base(c, status)
    {        
    }

    public DeleteResult(TDto dto, Status status)
        : base(dto, status)
    {
    }
}
