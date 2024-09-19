using BSS.DishDepot.Application.Mappers;
using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Recipes;

public sealed class PutRecipeCommandHandler : IRequestHandler<PutRecipeCommand, Result<Recipe>>
{
    private readonly ILogger<PutRecipeCommandHandler> _logger;
    private readonly IUnitOfWork _uow;

    public PutRecipeCommandHandler(ILogger<PutRecipeCommandHandler> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public async Task<Result<Recipe>> Handle(PutRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!Guid.TryParse(request.RecipeId, out var recipeId))
                return Result<Recipe>.NotFound($"Recipe {request.RecipeId} not found.");

            var recipe = await _uow.Query<Recipe>().FirstOrDefaultAsync(r => r.Id == recipeId, cancellationToken);
            if (recipe is null)
                return Result<Recipe>.NotFound($"Recipe {request.RecipeId} not found.");

            recipe.UpdateFromDto(request.Request.Recipe);

            _uow.Update(recipe);
            await _uow.SaveChanges(cancellationToken);

            return Result<Recipe>.Success(recipe);
        }
        catch (ETagMismatchException ex)
        {
            const string msg = "Unable to update Recipe: ETag Mismatch.";
            _logger.LogWarning(ex, msg);
            return Result<Recipe>.Invalid(msg);
        }
        catch (Exception ex)
        {
            const string msg = "An unexpected error occurred attempting to update Recipe.";
            _logger.LogError(ex, msg);
            return Result<Recipe>.Unexpected(msg);
        }
    }
}
