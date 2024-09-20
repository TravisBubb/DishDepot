using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Users;

public sealed record GetMyUserQuery : IRequest<Result<User>>;
