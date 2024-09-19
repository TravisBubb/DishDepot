using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Entities;

public class Entity : IEntity<Guid>
{
    public Guid Id { get; set; }
}
