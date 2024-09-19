using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Entities
{
    public abstract class UserETagCreatedDateEntity : ETagCreatedDateEntity, IUser
    {
        public Guid UserId { get; set; }
    }
}
