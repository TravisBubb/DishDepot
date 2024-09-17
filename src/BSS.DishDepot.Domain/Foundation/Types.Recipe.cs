using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BSS.DishDepot.Domain.Foundation
{
    [JsonConverter(typeof(JsonStringEnumConverter<MeasurementType>))]
    public enum MeasurementType
    {
        [EnumMember(Value = "GRAMS")]
        Grams = 0,
        [EnumMember(Value = "KILOGRAMS")]
        Kilograms,
        [EnumMember(Value = "OUNCES")]
        Ounces,
        [EnumMember(Value = "POUNDS")]
        Pounds,
        [EnumMember(Value = "MILILITERS")]
        Mililiters,
        [EnumMember(Value = "LITERS")]
        Liters,
        [EnumMember(Value = "FLUID_OUNCES")]
        FluidOunces,
        [EnumMember(Value = "CUPS")]
        Cups,
        [EnumMember(Value = "GALLONS")]
        Gallons,
        [EnumMember(Value = "TEASPOON")]
        Teaspoon,
        [EnumMember(Value = "TABLESPOON")]
        Tablespoon
    }
}
