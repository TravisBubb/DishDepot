using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class AuthenticateUserRequest
{
    [Required]
    [EmailAddress]
    [DataMember]
    public string Email { get; set; } = null!;

    [Required]
    [DataMember]
    public string Password { get; set; } = null!;
}
