using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.Domain.Interfaces;

public interface IReadOnlyUnitOfWork
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;
}

public interface IReadOnlyUnitOfWork<TContext> : IReadOnlyUnitOfWork where TContext : DbContext
{ }

public interface IUnitOfWork : IReadOnlyUnitOfWork
{
    void Insert<TEntity>(TEntity entity) where TEntity : Entity;
    void Update<TEntity>(TEntity entity) where TEntity : Entity;
    void Delete<TEntity>(TEntity entity) where TEntity : Entity;

    void ClearChanges();
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{ }