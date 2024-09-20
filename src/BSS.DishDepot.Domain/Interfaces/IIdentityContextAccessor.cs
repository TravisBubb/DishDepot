namespace BSS.DishDepot.Domain.Interfaces;

public interface IIdentityContextAccessor
{
    IIdentityContext IdentityContext { get; set; }
}
