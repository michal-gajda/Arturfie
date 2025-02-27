namespace Arturfie.Application.Battle.Interfaces;

using Arturfie.Application.Battle.Models;

internal interface IBattleService
{
    Task<string> FightAsync(string characterName, string rivalName, CancellationToken cancellationToken = default);
}
