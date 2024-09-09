using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class User
    {
        public Guid? Id { get; set; }
        public string? ETag { get; set; }

        public string? Email { get; set; }
    }
}
