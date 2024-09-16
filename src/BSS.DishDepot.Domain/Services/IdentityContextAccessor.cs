using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Services
{
    public class IdentityContextAccessor : IIdentityContextAccessor
    {
        public required IIdentityContext IdentityContext { get; set; }
    }
}
