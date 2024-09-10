using BSS.DishDepot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BSS.DishDepot.Infrastructure.Dal;

public static class ServiceExtensions
{
    public static IServiceCollection AddSqlUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IUnitOfWork<TContext>>());

        return services;
    }

    public static IServiceCollection AddReadOnlyUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped(typeof(IReadOnlyUnitOfWork<>), typeof(ReadOnlyUnitOfWork<>));
        services.AddScoped<IReadOnlyUnitOfWork<TContext>, ReadOnlyUnitOfWork<TContext>>();
        services.AddScoped<IReadOnlyUnitOfWork>(sp => sp.GetRequiredService<IReadOnlyUnitOfWork<TContext>>());

        return services;
    }
}