using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class PostRecipeRequest
    {
        [DataMember]
        [Required]
        public PostRecipe Recipe { get; set; } = null!;
    }
}
