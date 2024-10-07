namespace BSS.DishDepot.Domain.Interfaces;

public interface IIdentityContext
{
    Guid UserId { get; }
    string? UserEmail { get; } 
}
