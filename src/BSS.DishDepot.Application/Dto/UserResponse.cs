using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class UserResponse
    {
        public User? User { get; set; }
    }
}
