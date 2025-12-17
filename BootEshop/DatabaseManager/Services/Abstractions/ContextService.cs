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

    public IQueryable<TEntity> GetEntities()
    {
        return _context.Set<TEntity>();
    }
    //public abstract IEnumerable<TEntity> GetEntities();
}