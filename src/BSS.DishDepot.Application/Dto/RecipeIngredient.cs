using BSS.DishDepot.Domain.Foundation;
using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class RecipeIngredient
    {
        [DataMember]
        public string? Id { get; set; }

        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public MeasurementType? MeasurementType { get; set; }

        [DataMember]
        public decimal? MeasurementValue { get; set; }
    }
}
