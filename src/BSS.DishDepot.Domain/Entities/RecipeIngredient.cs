using BSS.DishDepot.Domain.Foundation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSS.DishDepot.Domain.Entities;

[Table("RecipeIngredients", Schema = "DishDepot")]
public class RecipeIngredient : Entity
{
    public Guid RecipeId { get; set; }
    public required string Name { get; set; }
    public MeasurementType MeasurementType { get; set; }
    public decimal MeasurementValue { get; set; }
}
