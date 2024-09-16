using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using BSS.DishDepot.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using User = BSS.DishDepot.Domain.Entities.User;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Result<AccessToken>>
    {
        private readonly ILogger<AuthenticateUserCommandHandler> _logger;
        private readonly IReadOnlyUnitOfWork _uow;
        private readonly ITokenService _tokenService;

        public AuthenticateUserCommandHandler(
            ILogger<AuthenticateUserCommandHandler> logger, 
            IReadOnlyUnitOfWork uow, 
            ITokenService tokenService)
        {
            _logger = logger;
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Result<AccessToken>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Create a NormalizedEmail property on the user and use that to compare instead of the case-sensitive email property
                var user = await _uow.Query<User>().FirstOrDefaultAsync(u => 
                    u.Email == request.Request.Email, cancellationToken);

                if (user is null)
                    return Result<AccessToken>.Unauthorized("The email and password combination is incorrect.");

                if (!PasswordHasher.VerifyHashedPassword(user.PasswordHash, request.Request.Password))
                    return Result<AccessToken>.Unauthorized("The email and password combination is incorrect.");

                var userClaims = new List<Claim>
                {
                    new("UserId", user.Id.ToString())
                };

                var tokenValue =  _tokenService.GetToken(userClaims);
                var token = new AccessToken { Token = tokenValue };

                return Result<AccessToken>.Success(token);
            }
            catch (Exception ex)
            {
                const string msg = "An unexpected error occurred attempting to authenticate user.";
                _logger.LogError(ex, msg);
                return Result<AccessToken>.Unexpected(msg);
            }
        }
    }
}
