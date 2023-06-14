using EventPlatform.Entities.Contexts;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.ECP;
using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;
using EventPlatform.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlatform.DataAccess.Classes;

public static class RepositoryExtension
{
    public static PostResult<TEntity, TDto> Add<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository, 
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        try
        {
            repository.Insert(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new(obj, Status.InvalidObject);
        }

        repository.Save();

        return new(obj, Status.Success);
    }

    public static PutResult<TEntity, TDto> Update<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        try
        {
            repository.Update(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new(obj, Status.InvalidObject);
        }

        repository.Save();

        return new(obj, Status.Success);
    }

    public static DeleteResult<TEntity, TDto> Delete<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        try
        {
            repository.Delete(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new(obj, Status.InvalidObject);
        }

        repository.Save();

        return new(obj, Status.Success);
    }

    public static PostResult<TEntity, TDto> AddWithIdentification<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid userId,
        TEntity entity,
        Func<TEntity, object> getId,
        Func<TEntity, string, bool> compareUsername
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        var result = repository.IdentifyUser<PostResult<TEntity, TDto>, TEntity, TDto, TContext>
            (userId, entity, getId, compareUsername, (e, s) => new(e, s));

        if (result is not null)
            return result;

        return repository.Add<TEntity, TDto, TContext>(entity);
    }

    public static PutResult<TEntity, TDto> UpdateWithIdentification<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid userId, 
        TEntity entity,
        Func<TEntity, object> getId,
        Func<TEntity, string, bool> compareUsername
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        var result = repository.IdentifyUser<PutResult<TEntity, TDto>, TEntity, TDto, TContext>
            (userId, entity, getId, compareUsername, (e, s) => new(e, s));

        if (result is not null)
            return result;

        return repository.Update<TEntity, TDto, TContext>(entity);
    }

    public static DeleteResult<TEntity, TDto> DeleteWithIdentification<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid userId,
        TEntity entity,
        Func<TEntity, object> getId,
        Func<TEntity, string, bool> compareUsername
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        var result = repository.IdentifyUser<DeleteResult<TEntity, TDto>, TEntity, TDto, TContext>
            (userId, entity, getId, compareUsername, (e, s) => new(e, s));

        if (result is not null)
            return result;

        return repository.Delete<TEntity, TDto, TContext>(entity);
    }

    private static TDtoResult? IdentifyUser<TDtoResult, TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid userId,
        TEntity entity,
        Func<TEntity, object> getId,
        Func<TEntity, string, bool> compareUsername,
        Func<TEntity, Status, TDtoResult> createDto
    )
        where TDtoResult : DtoResult<TEntity, TDto>
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        var username = LoginHandler.GetUsername(userId);

        if (username is null)
            return createDto(entity, Status.AccessDenied);

        var ev = repository.GetByID(getId(entity));

        if (ev is not null)
            repository.Detach(ev);
        else
            return createDto(entity, Status.NotFound);

        if (!compareUsername(entity, username))
            return createDto(entity, Status.AccessDenied);

        return null;
    }
}
