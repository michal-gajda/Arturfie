namespace Arturfie.Application.Battle.Models;

using Arturfie.Application.Battle.Enums;

public sealed record class Character
{
    public required string Name { get; init; }
    public required decimal Score { get; init; }
    public required CharacterType Type { get; init; }
    public string? Weakness { get; init; } = default;
}
