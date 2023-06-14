using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventPlatform.DataAccess.Classes;

public abstract class Repository<TContext, TEntity>
    : IDisposable
    where TContext : DbContext
    where TEntity : class
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        var query = _dbSet as IQueryable<TEntity>;

        if (includeProperties != string.Empty)
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        if (filter is not null)
            query = query.Where(filter);

        return orderBy is null ? query : orderBy(query);
    }

    public virtual TEntity? GetByID(object id)
        => _dbSet.Find(id);

    public virtual void Insert(TEntity entity)
        => _dbSet.Add(entity);

    protected virtual void Delete(object id)
    {
        var entityToDelete = _dbSet.Find(id);

        if (entityToDelete is null)
            return;

        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
            _dbSet.Attach(entityToDelete);

        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);

        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void Save()
        => _context.SaveChanges();

    public void Dispose()
    {
        _context.SaveChanges();
        _context.Dispose();

        GC.SuppressFinalize(this);
    }

    public void Detach(TEntity entity)
        => _context.Entry(entity).State = EntityState.Detached;

    ~Repository()
        => Dispose();
}
