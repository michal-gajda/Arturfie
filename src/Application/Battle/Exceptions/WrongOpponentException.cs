namespace Arturfie.Application.Battle.Exceptions;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class WrongOpponentException : Exception
{
    public WrongOpponentException() : base("characters of the same type should not fight")
    {
    }
}
