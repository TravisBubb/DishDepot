using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Recipes
{
    public sealed class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, Result<Recipe>>
    {
        private readonly ILogger<GetRecipeQueryHandler> _logger;
        private readonly IReadOnlyUnitOfWork _uow;

        public GetRecipeQueryHandler(ILogger<GetRecipeQueryHandler> logger, IReadOnlyUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Result<Recipe>> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.RecipeId, out var recipeId))
                    return Result<Recipe>.NotFound($"Recipe {request.RecipeId} not found.");

                var recipe = await _uow.Query<Recipe>()
                    .Include(r => r.Steps)
                    .Include(r => r.Ingredients)
                    .FirstOrDefaultAsync(r => r.Id == recipeId, cancellationToken);

                return recipe is not null
                    ? Result<Recipe>.Success(recipe)
                    : Result<Recipe>.NotFound($"Recipe {request.RecipeId} not found.");
            }
            catch (Exception ex)
            {
                const string msg = "An unexpected error occurred attempting to retrieve Recipe";
                _logger.LogError(ex, msg);
                return Result<Recipe>.Unexpected(msg);
            }
        }
    }
}
