namespace Arturfie.WebApi.Controllers;

using Arturfie.Application.Battle.Queries;
using Asp.Versioning;

[ApiController, ApiVersion("2.0"), Route("battle")]
public sealed class BattleV2Controller(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetBattleResult"), MapToApiVersion("2.0"), ProducesResponseType<string>(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<string> GetAsyncV2([FromQuery] string hero, [FromQuery] string villain, CancellationToken cancellationToken = default)
    {
        var query = new DuelV2
        {
            Hero = hero,
            Villain = villain,
        };
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }
}
