using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class PostUserRequest
{
    [DataMember]
    [Required]
    public PostUser User { get; set; } = null!;
}
