using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSS.DishDepot.Infrastructure.Dal.Configurations;

public class UserConfiguration : EntityConfigurationMapper<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
    }
}
