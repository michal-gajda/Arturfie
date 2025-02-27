namespace Arturfie.Application.Battle.Exceptions;

using Arturfie.Application.Battle.Models;

public sealed class WrongOpponentException : Exception
{
    public WrongOpponentException(Character character, Character rival) : base($"'{character.Name}' can't fight with {rival.Name}")
    {
    }
}
