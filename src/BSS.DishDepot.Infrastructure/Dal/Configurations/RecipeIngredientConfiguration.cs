﻿using BSS.DishDepot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BSS.DishDepot.Infrastructure.Dal.Configurations
{
    public class RecipeIngredientConfiugration : EntityConfigurationMapper<RecipeIngredient>
    {
        public override void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
        }
    }
}
