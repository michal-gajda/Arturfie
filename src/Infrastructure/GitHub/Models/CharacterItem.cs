namespace Arturfie.Infrastructure.GitHub.Models;

using System.Text.Json.Serialization;
using Arturfie.Application.Battle.Enums;
using Arturfie.Application.Battle.Models;

internal sealed record class CharacterItem
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("score")] public required decimal Score { get; init; }
    [JsonPropertyName("type")] public required string Type { get; init; }
    [JsonPropertyName("weakness")] public string? Weakness { get; init; } = default;

    public static explicit operator Character(CharacterItem item)
    {
        return new Character
        {
            Name = item.Name,
            Score = item.Score,
            Type = item.Type switch
            {
                "hero" => CharacterType.Hero,
                "villain" => CharacterType.Villain,
                _ => throw new ArgumentException($"Unknown type: {item.Type}")
            },
            Weakness = item.Weakness
        };
    }
}
