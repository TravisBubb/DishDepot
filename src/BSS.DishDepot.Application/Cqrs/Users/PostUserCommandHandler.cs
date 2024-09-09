using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Users
{
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, Result<User>>
    {
        public Task<Result<User>> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
