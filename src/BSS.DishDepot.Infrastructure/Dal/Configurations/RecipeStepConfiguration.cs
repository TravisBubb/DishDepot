using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSS.DishDepot.Infrastructure.Dal.Configurations;

public sealed class RecipeStepConfiguration : EntityConfigurationMapper<RecipeStep>
{
    public override void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
    }
}
