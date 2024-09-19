using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class RecipesResponse
    {
        [DataMember]
        public List<Recipe>? Recipes { get; set; }
    }
}
