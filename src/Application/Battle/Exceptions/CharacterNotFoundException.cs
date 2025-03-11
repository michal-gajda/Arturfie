namespace Arturfie.Application.Battle.Exceptions;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class CharacterNotFoundException : Exception
{
    public CharacterNotFoundException(string name) : base($"'{name}' can't be found")
    {
    }
}
