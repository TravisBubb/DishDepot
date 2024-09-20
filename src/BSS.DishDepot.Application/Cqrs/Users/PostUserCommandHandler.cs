using BSS.DishDepot.Application.Mappers;
using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using BSS.DishDepot.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BSS.DishDepot.Application.Cqrs.Users;

public sealed class PostUserCommandHandler : IRequestHandler<PostUserCommand, Result<User>>
{
    private readonly ILogger<PostUserCommandHandler> _logger;
    private readonly IUnitOfWork _uow;

    public PostUserCommandHandler(ILogger<PostUserCommandHandler> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public async Task<Result<User>> Handle(PostUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHash = PasswordHasher.HashPassword(request.Request.User.Password);
            var entity = request.Request.User.ToEntity(passwordHash);

            _uow.Insert(entity);

            await _uow.SaveChanges(cancellationToken);

            return Result<User>.Success(entity); 
        }
        catch (Exception ex)
        {
            const string msg = "An unexpected error occurred attempting to create User.";
            _logger.LogError(ex, msg);
            return Result<User>.Unexpected(msg);
        }
    }
}
