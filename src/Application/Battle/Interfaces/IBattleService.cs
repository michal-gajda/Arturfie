namespace Arturfie.Application.Battle.Interfaces;

internal interface IBattleService
{
    Task<string> FightAsync(string characterName, string rivalName, CancellationToken cancellationToken = default);
}
