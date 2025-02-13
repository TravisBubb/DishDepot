﻿using BSS.DishDepot.Application.Dto;
using Mapster;
using Recipe = BSS.DishDepot.Domain.Entities.Recipe;
using RecipeIngredient = BSS.DishDepot.Domain.Entities.RecipeIngredient;
using RecipeStep = BSS.DishDepot.Domain.Entities.RecipeStep;

namespace BSS.DishDepot.Application.Mappers;

public sealed class RecipeMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Recipe, RecipeResponse>()
            .Map(dest => dest.Recipe, src => src);

        config.ForType<Recipe, Dto.Recipe>()
            .Map(dest => dest.ETag, src => Convert.ToBase64String(src.ETag));

        config.ForType<List<Recipe>, RecipesResponse>()
            .Map(dest => dest.Recipes, src => src);

        config.ForType<Dto.Recipe, PutRecipe>()
            .Map(dest => dest.ETag, src => Convert.FromBase64String(src.ETag));
    }
}

public static class RecipeMapperExtensions
{
    public static Recipe ToEntity(this PostRecipe source)
    {
        var recipeId = Guid.NewGuid();

        return new Recipe
        {
            Id = recipeId,
            Name = source.Name,
            Description = source.Description,
            PrepTime = source.PrepTime,
            CookTime = source.CookTime,
            Servings = source.Servings,
            Steps = source.Steps?.Select(s => s.ToEntity(recipeId))?.ToList(),
            Ingredients = source.Ingredients?.Select(i => i.ToEntity(recipeId))?.ToList()
        };
    }

    public static RecipeStep ToEntity(this PostRecipeStep source, Guid recipeId)
    {
        return new RecipeStep
        {
            RecipeId = recipeId,
            Description = source.Description,
            Sequence = source.Sequence
        };
    }

    public static RecipeIngredient ToEntity(this PostRecipeIngredient source, Guid recipeId)
    {
        return new RecipeIngredient
        {
            RecipeId = recipeId,
            Name = source.Name,
            MeasurementType = source.MeasurementType,
            MeasurementValue = source.MeasurementValue
        };
    }

    public static void UpdateFromDto(this Recipe recipe, PutRecipe source)
    {
        recipe.ETag = source.ETag;
        recipe.Name = source.Name;
        recipe.Description = source.Description;
        recipe.PrepTime = source.PrepTime;
        recipe.CookTime = source.CookTime;
        recipe.Servings = source.Servings;

        recipe.Ingredients = source.Ingredients?.Select(i => i.ToEntity(recipe.Id)).ToList();
        recipe.Steps = source.Steps?.Select(s => s.ToEntity(recipe.Id)).ToList();
    }
}
