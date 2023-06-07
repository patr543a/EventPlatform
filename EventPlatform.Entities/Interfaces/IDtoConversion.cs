namespace EventPlatform.Entities.Interfaces;

public interface IDtoConversion<in TFrom, out TTo>
    where TFrom : class, IDtoConversion<TFrom, TTo>
    where TTo : class, IDto<TTo>
{
    TTo ToDto();
}
