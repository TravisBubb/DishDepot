using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class Recipe
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public string? ETag { get; set; }

        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public string? Description { get; set; }

        [DataMember]
        public int PrepTime { get; set; }

        [DataMember]
        public int CookTime { get; set; }

        [DataMember]
        public int Servings { get; set; }

        [DataMember]
        public List<RecipeStep>? Steps { get; set; }

        [DataMember]
        public List<RecipeIngredient>? Ingredients { get; set; }
    }
}
