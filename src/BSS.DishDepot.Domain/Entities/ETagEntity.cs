using BSS.DishDepot.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BSS.DishDepot.Domain.Entities;

public class ETagEntity : Entity, IETag
{
    [Timestamp]
    public byte[] ETag { get; set; } = null!;
}