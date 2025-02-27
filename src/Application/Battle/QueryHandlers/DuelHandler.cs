namespace Arturfie.Application.Battle.QueryHandlers;

using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Queries;

internal sealed class DuelHandler(ILogger<DuelHandler> logger, IBattleService service) : IRequestHandler<Duel, string>
{
    public async Task<string> Handle(Duel request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Fight between {request.Character} and {request.Rival}");

        return await service.FightAsync(request.Character, request.Rival, cancellationToken);
    }
}
