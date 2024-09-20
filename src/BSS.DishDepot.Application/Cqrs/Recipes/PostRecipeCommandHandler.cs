using BSS.DishDepot.Application.Mappers;
using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Recipes;

public sealed class PostRecipeCommandHandler : IRequestHandler<PostRecipeCommand, Result<Recipe>>
{
    private readonly ILogger<PostRecipeCommandHandler> _logger;
    private readonly IUnitOfWork _uow;

    public PostRecipeCommandHandler(ILogger<PostRecipeCommandHandler> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public async Task<Result<Recipe>> Handle(PostRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = request.Request.Recipe.ToEntity();

            _uow.Insert(entity);
            await _uow.SaveChanges(cancellationToken);

            return Result<Recipe>.Success(entity);
        } 
        catch (Exception ex)
        {
            const string msg = "An unexpected error occurred attempting to create Recipe";
            _logger.LogError(ex, msg);
            return Result<Recipe>.Unexpected(msg);
        }
    }
}
