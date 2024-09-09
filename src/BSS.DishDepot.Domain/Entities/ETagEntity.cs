using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Entities;

public class ETagEntity : Entity, IETag
{
    public byte[] ETag { get; set; } = null!;
}