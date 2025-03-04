namespace Arturfie.Infrastructure.GitHub.Services;

using System.Net.Http.Json;
using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Models;
using Arturfie.Infrastructure.GitHub.Models;
using Microsoft.Extensions.Logging;

internal sealed partial class CharacterProvider(ILogger<CharacterProvider> logger, HttpClient httpClient, GitHubOptions options) : ICharacterProvider
{
    private readonly Dictionary<string, Character> pairs = [];

    public async Task<Character?> GetCharacterByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (pairs is { Count: 0, })
        {
            this.LogRetrieving();

            try
            {
                var response = await httpClient.GetFromJsonAsync<CharactersResponse>(options.Characters, cancellationToken);

                if (response is not null)
                {
                    this.LogRetrieved(response.Items.Count);

                    foreach (var item in response.Items)
                    {
                        pairs.Add(item.Name, (Character)item);
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "{Message}", exception.Message);
            }
        }

        if (this.pairs.TryGetValue(name, out var character))
        {
            return character;
        }

        return default;
    }

    [LoggerMessage(LogLevel.Information, Message = "Retrieving")]
    private partial void LogRetrieving();

    [LoggerMessage(LogLevel.Information, Message = "Retrieved {Items} items")]
    private partial void LogRetrieved(int Items);
}
