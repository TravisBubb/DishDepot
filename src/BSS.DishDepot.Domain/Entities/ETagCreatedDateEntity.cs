using BSS.DishDepot.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BSS.DishDepot.Domain.Entities;

public class ETagCreatedDateEntity : CreatedDateEntity, IETag
{
    [Timestamp]
    public byte[] ETag { get; set; } = null!;
}