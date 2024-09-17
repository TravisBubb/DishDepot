using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.Infrastructure.Dal;

public class ReadOnlyUnitOfWork<TContext> : IReadOnlyUnitOfWork<TContext>
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected readonly IIdentityContextAccessor Accessor;

    public ReadOnlyUnitOfWork(TContext context, IIdentityContextAccessor accessor)
    {
        Context = context;
        Accessor = accessor;
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
    {
        var query = Context.Set<TEntity>().AsNoTracking().AsQueryable();

        if (!typeof(IUser).IsAssignableFrom(typeof(TEntity)))
            return query;

        if (Accessor.IdentityContext.UserId == default)
            throw new Exception("UserId not set in identity context.");

        return query.Where(e => ((IUser)e).UserId == Accessor.IdentityContext.UserId);
    }
}

public class UnitOfWork<TContext> : ReadOnlyUnitOfWork<TContext>, IUnitOfWork<TContext>
    where TContext : DbContext
{
    public UnitOfWork(TContext context, IIdentityContextAccessor accessor) : base(context, accessor) 
    { 
    }

    public void Insert<TEntity>(TEntity entity) where TEntity : Entity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (entity is ICreatedDate createdDateEntity && createdDateEntity.CreatedDateTime == default)
            createdDateEntity.CreatedDateTime = DateTime.UtcNow;

        if (entity is IUser userEntity)
            userEntity.UserId = Accessor.IdentityContext.UserId;

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