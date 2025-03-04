namespace Arturfie.WebApi.Controllers;

using Arturfie.Application.Battle.Queries;
using Asp.Versioning;

[ApiController, ApiVersion("1.0"), ApiVersion("2.0"), Route("[controller]")]
public sealed class BattleController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetBattleResult")]
    public async Task<string> GetAsync([FromQuery] string character, [FromQuery] string rival, CancellationToken cancellationToken = default)
    {
        var query = new Duel
        {
            Character = character,
            Rival = rival,
        };
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }

    [HttpGet(Name = "GetBattleResult"), MapToApiVersion("2.0")]
    public async Task<string> GetAsyncV2([FromQuery] string hero, [FromQuery] string villain, CancellationToken cancellationToken = default)
    {
        var query = new Duel
        {
            Character = hero,
            Rival = villain,
        };
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }
}
