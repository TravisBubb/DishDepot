namespace BSS.DishDepot.Domain.Foundation
{
    public sealed class ETagMismatchException : Exception
    {
        public ETagMismatchException(string message) : base(message) { }
    }
}
