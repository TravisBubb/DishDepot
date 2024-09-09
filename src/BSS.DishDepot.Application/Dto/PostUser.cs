using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class PostUser
{
    [DataMember]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [DataMember]
    [Required]
    public string Password { get; set; } = null!;
}