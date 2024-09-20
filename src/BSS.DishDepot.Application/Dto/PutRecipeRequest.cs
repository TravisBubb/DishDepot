using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class PutRecipeRequest
{
    [DataMember]
    [Required]
    public PutRecipe Recipe { get; set; } = null!;
}
