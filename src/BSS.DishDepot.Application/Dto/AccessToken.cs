using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class AccessToken
{
    [DataMember]
    public string? Token { get; set; }
}
