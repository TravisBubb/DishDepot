using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Entities;

public class CreatedDateEntity : Entity, ICreatedDate
{
    public DateTime CreatedDateTime { get; set; }
}