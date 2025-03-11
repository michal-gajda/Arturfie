namespace Arturfie.Application.Battle.Exceptions;

using System.Diagnostics.CodeAnalysis;
using Arturfie.Application.Battle.Models;

[ExcludeFromCodeCoverage]
public sealed class OpponentHimselfException : Exception
{
    public OpponentHimselfException(Character character) : base($"'{character.Name}' can't fight with himself")
    {
    }
}
