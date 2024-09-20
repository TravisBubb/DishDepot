using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Users;

public sealed class GetMyUserQueryHandler : IRequestHandler<GetMyUserQuery, Result<User>>
{
    private readonly ILogger<GetMyUserQueryHandler> _logger;
    private readonly IReadOnlyUnitOfWork _uow;
    private readonly IIdentityContextAccessor _accessor;

    public GetMyUserQueryHandler(
        ILogger<GetMyUserQueryHandler> logger, 
        IReadOnlyUnitOfWork uow,
        IIdentityContextAccessor accessor)
    {
        _logger = logger;
        _uow = uow;
        _accessor = accessor;
    }

    public async Task<Result<User>> Handle(GetMyUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _uow.Query<User>().FirstOrDefaultAsync(u => 
                u.Id == _accessor.IdentityContext.UserId, cancellationToken);

            return user is not null
                ? Result<User>.Success(user)
                : Result<User>.NotFound($"User not found.");
        }
        catch (Exception ex)
        {
            const string msg = "An unexpected error occurred attempting to get user";
            _logger.LogError(ex, msg);
            return Result<User>.Unexpected(msg);
        }
    }
}
