using BSS.DishDepot.Domain.Entities;

namespace BSS.DishDepot.Domain.Interfaces
{
    public abstract class UserEntity : Entity, IUser
    {
        public Guid UserId { get; set; }
    }
}
