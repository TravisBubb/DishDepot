using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using MediatR;
using User = BSS.DishDepot.Domain.Entities.User;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public record PostUserCommand(PostUserRequest Request) : IRequest<Result<User>>;
}
