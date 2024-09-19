using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class RecipeResponse
    {
        [DataMember]
        public Recipe? Recipe { get; set; }
    }
}
