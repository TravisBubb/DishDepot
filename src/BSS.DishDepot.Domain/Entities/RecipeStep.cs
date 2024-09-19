using System.ComponentModel.DataAnnotations.Schema;

namespace BSS.DishDepot.Domain.Entities
{
    [Table("RecipeSteps", Schema = "DishDepot")]
    public class RecipeStep : Entity
    {
        public Guid RecipeId { get; set; }
        public required string Description { get; set; }
        public int Sequence { get; set; }
    }
}
