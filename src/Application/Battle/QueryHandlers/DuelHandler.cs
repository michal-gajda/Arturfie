namespace Arturfie.Application.Battle.QueryHandlers;

using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Queries;
using Microsoft.Extensions.DependencyInjection;

internal sealed class DuelHandler(ILogger<DuelHandler> logger, [FromKeyedServices("v1")] IBattleService service) : IRequestHandler<Duel, string>
{
    public async Task<string> Handle(Duel request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fight between {Character} and {Rival}", request.Character, request.Rival);

        return await service.FightAsync(request.Character, request.Rival, cancellationToken);
    }
}
