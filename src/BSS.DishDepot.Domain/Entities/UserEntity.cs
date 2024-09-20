using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Entities;

public abstract class UserEntity : Entity, IUser
{
    public Guid UserId { get; set; }
}
