using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using MediatR;
using Recipe = BSS.DishDepot.Domain.Entities.Recipe;

namespace BSS.DishDepot.Application.Cqrs.Recipes
{
    public sealed record PostRecipeCommand(PostRecipeRequest Request) : IRequest<Result<Recipe>>;
}
