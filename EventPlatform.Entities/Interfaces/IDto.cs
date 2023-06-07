namespace EventPlatform.Entities.Interfaces;

public interface IDto<TDto>
    where TDto : class, IDto<TDto>
{
}
