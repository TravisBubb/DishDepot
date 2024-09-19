using System.ComponentModel.DataAnnotations.Schema;

namespace BSS.DishDepot.Domain.Entities
{
    [Table("Recipes", Schema = "DishDepot")]
    public class Recipe : UserETagCreatedDateEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }

        public List<RecipeStep>? Steps { get; set; }
        public List<RecipeIngredient>? Ingredients { get; set; }
    }
}
