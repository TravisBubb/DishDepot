using BSS.DishDepot.Application.Dto;
using Mapster;
using User = BSS.DishDepot.Domain.Entities.User;

namespace BSS.DishDepot.Application.Mappers
{
    public class UserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<User, UserResponse>()
                .Map(dest => dest.User, src => src);

            config.ForType<User, Dto.User>()
                .Map(dest => dest.ETag, src => Convert.ToBase64String(src.ETag));
        }
    }

    public static class UserMapperExtensions
    {
        public static User ToEntity(this PostUser source, string passwordHash)
        {
            return new User
            {
                Email = source.Email,
                PasswordHash = passwordHash
            };
        }
    }
}
