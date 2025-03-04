namespace Arturfie.WebApi.Controllers;

using Arturfie.Application.Battle.Queries;
using Asp.Versioning;

[ApiController, ApiVersion("1.0"), Route("battle")]
public sealed class BattleController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetBattleResult"), ProducesResponseType<string>(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
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
