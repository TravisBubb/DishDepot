using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using MediatR;
using Recipe = BSS.DishDepot.Domain.Entities.Recipe;

namespace BSS.DishDepot.Application.Cqrs.Recipes;

public sealed record PutRecipeCommand(string RecipeId, PutRecipeRequest Request) : IRequest<Result<Recipe>>;
