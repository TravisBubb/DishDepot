using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSS.DishDepot.Infrastructure.Dal.Configurations;

public sealed class RecipeConfiguration : EntityConfigurationMapper<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasMany(r => r.Steps)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasMany(r => r.Ingredients)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
