using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class PostRecipe
    {
        [DataMember]
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [DataMember]
        [MinLength(1)]
        [MaxLength(256)]
        public string? Description { get; set; }

        [DataMember]
        public int PrepTime { get; set; }

        [DataMember]
        public int CookTime { get; set; }

        [DataMember]
        public int Servings { get; set; }

        [DataMember]
        public List<PostRecipeStep>? Steps { get; set; }

        [DataMember]
        public List<PostRecipeIngredient>? Ingredients { get; set; }
    }
}
