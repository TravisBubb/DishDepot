using BSS.DishDepot.Infrastructure.Dal.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.Infrastructure.Dal;

public class DishDepotDbContext : DbContext
{
    public DishDepotDbContext(DbContextOptions<DishDepotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        base.OnModelCreating(builder);
    }
}