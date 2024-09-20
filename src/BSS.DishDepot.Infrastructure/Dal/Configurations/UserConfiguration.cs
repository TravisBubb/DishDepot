using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSS.DishDepot.Infrastructure.Dal.Configurations;

public sealed class UserConfiguration : EntityConfigurationMapper<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
    }
}
