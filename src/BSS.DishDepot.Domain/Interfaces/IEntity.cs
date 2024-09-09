namespace BSS.DishDepot.Domain.Interfaces;

public interface IEntity<T>
{
    T Id { get; }
    DateTime CreatedDateTime { get; }
}
