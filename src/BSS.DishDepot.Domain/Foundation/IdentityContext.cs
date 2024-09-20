using BSS.DishDepot.Domain.Interfaces;

namespace BSS.DishDepot.Domain.Foundation;

public sealed class IdentityContext : IIdentityContext
{
    public required Guid UserId { get; set; }
    public required string UserEmail { get; set; }
}
