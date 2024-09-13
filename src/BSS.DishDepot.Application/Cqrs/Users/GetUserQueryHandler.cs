using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<User>>
    {
        private readonly ILogger<GetUserQueryHandler> _logger;
        private readonly IReadOnlyUnitOfWork _uow;

        public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, IReadOnlyUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.UserId, out var userId))
                    return Result<User>.NotFound($"User {request.UserId} not found");

                var user = await _uow.Query<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

                return user is not null
                    ? Result<User>.Success(user)
                    : Result<User>.NotFound($"User {userId} not found.");
            }
            catch (Exception ex)
            {
                const string msg = "An unexpected error occurred attempting to get user";
                _logger.LogError(ex, msg);
                return Result<User>.Unexpected(msg);
            }
        }
    }
}
