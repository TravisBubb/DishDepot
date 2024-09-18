using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Recipes
{
    public sealed class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, Result<List<Recipe>>>
    {
        private readonly ILogger<GetRecipeQueryHandler> _logger;
        private readonly IReadOnlyUnitOfWork _uow;

        public GetRecipesQueryHandler(ILogger<GetRecipeQueryHandler> logger, IReadOnlyUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Result<List<Recipe>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Pagination and Filtering

                var recipes = await _uow.Query<Recipe>()
                    .Include(r => r.Steps)
                    .Include(r => r.Ingredients)
                    .ToListAsync(cancellationToken);

                return Result<List<Recipe>>.Success(recipes);
            }
            catch (Exception ex)
            {
                const string msg = "An unexpected error occurred attempting to retrieve Recipes";
                _logger.LogError(ex, msg);
                return Result<List<Recipe>>.Unexpected(msg);
            }
        }
    }
}
