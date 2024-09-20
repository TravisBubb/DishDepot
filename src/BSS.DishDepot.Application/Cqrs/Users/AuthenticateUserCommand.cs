using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Users;

public sealed record AuthenticateUserCommand(AuthenticateUserRequest Request) : IRequest<Result<AccessToken>>;

