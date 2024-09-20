using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto;

[DataContract]
public class RecipeStep
{
    [DataMember]
    public string? Id { get; set; }

    [DataMember]
    public int? Sequence { get; set; }

    [DataMember]
    public string? Description { get; set; }
}
