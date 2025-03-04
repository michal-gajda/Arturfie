namespace Arturfie.Application.Battle.QueryHandlers;

using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Queries;
using Microsoft.Extensions.DependencyInjection;

internal sealed class DuelV2Handler(ILogger<DuelHandler> logger, [FromKeyedServices("v2")] IBattleService service) : IRequestHandler<DuelV2, string>
{
    public async Task<string> Handle(DuelV2 request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Fight between {request.Hero} and {request.Villain}");

        return await service.FightAsync(request.Hero, request.Villain, cancellationToken);
    }
}
