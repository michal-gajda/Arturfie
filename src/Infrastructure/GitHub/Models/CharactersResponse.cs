namespace Arturfie.Infrastructure.GitHub.Models;

using System.Text.Json.Serialization;

internal sealed record class CharactersResponse
{
    [JsonPropertyName("items")]
    public List<CharacterItem> Items { get; set; } = new();
}
