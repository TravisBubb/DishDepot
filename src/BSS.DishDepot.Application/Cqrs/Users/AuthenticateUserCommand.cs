using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public class AuthenticateUserCommand : IRequest<Result<AccessToken>>
    {
        public AuthenticateUserCommand(AuthenticateUserRequest request)
        {
            Request = request; 
        }

        public AuthenticateUserRequest Request { get; }
    }
}
