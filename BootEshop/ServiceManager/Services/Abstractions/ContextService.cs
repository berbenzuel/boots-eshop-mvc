using BootEshop.ViewArgs;
using Database;
using Database;
using Microsoft.EntityFrameworkCore;

namespace BootEshop.Models.Abstractions;

public abstract class ContextService<TEntity>(EshopContext dbContext) where TEntity : class
{
    protected readonly DbContext _context = dbContext;


    public TEntity? GetEntity<TKey>(TKey key)
    {
        return _context.Set<TEntity>().Find(key);
    }

    public bool AddEntity(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        return _context.SaveChanges() == 1;
    }

    public bool DeleteEntity(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return _context.SaveChanges() == 1;
    }
    public IQueryable<TEntity> GetEntities()
    {
        return _context.Set<TEntity>();
    }

    public bool UpdateEntity(TEntity entity)
    {
        if (_context.Entry(entity).Equals(entity))
            return true;
        _context.Set<TEntity>().Update(entity);
        _context.Entry<TEntity>(entity).State = EntityState.Modified;
        return _context.SaveChanges() == 1;
    }
    //public abstract IEnumerable<TEntity> GetEntities();
}