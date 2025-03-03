namespace Arturfie.WebApi.Controllers;

using Arturfie.Application.Battle.Queries;

[ApiController, Route("[controller]")]
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
}
