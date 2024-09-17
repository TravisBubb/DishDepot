using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class PostRecipeStep
    {
        [DataMember]
        [Required]
        [MinLength(1)]
        [MaxLength(512)]
        public string Description { get; set; } = null!;

        [DataMember]
        public int Sequence { get; set; }
    }
}
