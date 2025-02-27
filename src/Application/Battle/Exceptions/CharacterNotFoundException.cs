namespace Arturfie.Application.Battle.Exceptions;

public sealed class CharacterNotFoundException : Exception
{
    public CharacterNotFoundException(string name) : base($"'{name}' can't be found")
    {
    }
}
