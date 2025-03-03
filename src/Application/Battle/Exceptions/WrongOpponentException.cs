namespace Arturfie.Application.Battle.Exceptions;

public sealed class WrongOpponentException : Exception
{
    public WrongOpponentException() : base("characters of the same type should not fight")
    {
    }
}
