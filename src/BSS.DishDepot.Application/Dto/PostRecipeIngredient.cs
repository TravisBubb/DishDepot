using BSS.DishDepot.Domain.Foundation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class PostRecipeIngredient
{
    [DataMember]
    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string Name { get; set; } = null!;

    [DataMember]
    public MeasurementType MeasurementType { get; set; }

    [DataMember]
    public decimal MeasurementValue { get; set; }
}
