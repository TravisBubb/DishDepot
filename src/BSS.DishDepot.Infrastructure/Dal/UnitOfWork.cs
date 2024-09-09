using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.Infrastructure.Dal;

public class ReadOnlyUnitOfWork<TContext> : IReadOnlyUnitOfWork<TContext>
    where TContext : DbContext
{
    protected readonly TContext Context;

    public ReadOnlyUnitOfWork(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        => Context.Set<TEntity>().AsNoTracking().AsQueryable();
}

public class UnitOfWork<TContext> : ReadOnlyUnitOfWork<TContext>, IUnitOfWork<TContext>
    where TContext : DbContext
{
    public UnitOfWork(TContext context) : base(context) 
    { 
    }

    public void Insert<TEntity>(TEntity entity) where TEntity : Entity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (entity.CreatedDateTime == default)
            entity.CreatedDateTime = DateTime.UtcNow;

        Context.Add(entity);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : Entity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        Context.Update(entity);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : Entity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        Context.Remove(entity);
    }

    public void ClearChanges()
    {
        Context.ChangeTracker.Clear();
    }

    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            ClearChanges();
            throw;
        }
    }
}