using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public class GetUserQuery : IRequest<Result<User>>
    {
        public GetUserQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
