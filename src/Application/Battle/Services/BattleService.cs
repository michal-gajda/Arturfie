namespace Arturfie.Application.Battle.Services;

using Arturfie.Application.Battle.Exceptions;
using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Models;
using Microsoft.Extensions.Logging;

internal sealed partial class BattleService(ILogger<BattleService> logger, ICharacterProvider provider) : IBattleService
{
    public async Task<string> FightAsync(string characterName, string rivalName, CancellationToken cancellationToken = default)
    {
        var character = await provider.GetCharacterByNameAsync(characterName, cancellationToken);

        if (character is null)
        {
            throw new CharacterNotFoundException(characterName);
        }

        var rival = await provider.GetCharacterByNameAsync(rivalName, cancellationToken);

        if (rival is null)
        {
            throw new CharacterNotFoundException(rivalName);
        }

        if (character.Name == rival.Name)
        {
            throw new OpponentHimselfException(character);
        }

        if (character.Type == rival.Type)
        {
            throw new WrongOpponentException();
        }

        if (character.Score > rival.Score)
        {
            this.LogCharacter(character.Name);

            return character.Name;
        }

        this.LogCharacter(rival.Name);
        return rival.Name;
    }

    [LoggerMessage(LogLevel.Debug, Message = "{characterName}")]
    private partial void LogCharacter(string characterName);
}
