using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BootEshop.Models.Abstractions;

public abstract class ContextService<TEntity>(EshopContext dbContext) where TEntity : class
{
    protected readonly EshopContext _context = dbContext;


    public TEntity GetEntity<TKey>(TKey key)
    {
        return _context.Set<TEntity>().Find(key);
    }

    public TEntity? Foo<TKey>(TKey key) where TKey : notnull
    {
        return this._context.Find<TEntity>(key);
    }
    //public abstract IEnumerable<TEntity> GetEntities();
}