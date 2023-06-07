using EventPlatform.Entities.Enums;
using EventPlatform.Entities.Interfaces;
using EventPlatform.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlatform.DataAccess.Classes;

public static class RepositoryExtension
{
    public static PostResult<TEntity, TDto> Add<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository, 
        Guid sessionToken,
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        if (LoginHandler.GetUserPermissions(sessionToken) <= UserType.Organizer)
            return new(obj, Status.AccessDenied);

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

    public static DeleteResult<TEntity, TDto> Delete<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid sessionToken,
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        if (LoginHandler.GetUserPermissions(sessionToken) <= UserType.Organizer)
            return new(obj, Status.AccessDenied);

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

    public static PutResult<TEntity, TDto> Update<TEntity, TDto, TContext>(
        this Repository<TContext, TEntity> repository,
        Guid sessionToken,
        TEntity obj
    )
        where TEntity : class, IDtoConversion<TEntity, TDto>
        where TDto : class, IDto<TDto>
        where TContext : DbContext
    {
        if (LoginHandler.GetUserPermissions(sessionToken) <= UserType.Organizer)
            return new(obj, Status.AccessDenied);

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
}
