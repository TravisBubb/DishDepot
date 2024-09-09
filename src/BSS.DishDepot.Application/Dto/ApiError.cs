using System.Runtime.Serialization;

namespace BSS.DishDepot.Application.Dto
{
    [DataContract]
    public class ApiError
    {
        [DataMember]
        public int? HttpStatusCode { get; set; }

        [DataMember]
        public string? Message { get; set; }

        public ApiError() { }

        public ApiError(int httpStatusCode, string? message)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
        }
    }
}
