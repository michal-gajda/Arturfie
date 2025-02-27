namespace Arturfie.Application.Battle.Exceptions;

using Arturfie.Application.Battle.Models;

public sealed class OpponentHimselfException : Exception
{
    public OpponentHimselfException(Character character) : base($"'{character.Name}' can't fight with himself")
    {
    }
}
