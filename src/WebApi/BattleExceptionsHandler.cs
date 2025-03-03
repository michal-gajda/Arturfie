namespace Arturfie.WebApi;

using System.Net;
using Arturfie.Application.Battle.Exceptions;
using Microsoft.AspNetCore.Http;

public sealed class BattleExceptionsHandler(IProblemDetailsService problemDetailsService) : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            CharacterNotFoundException characterNotFoundException => HttpStatusCode.NotFound,
            OpponentHimselfException opponentHimselfException => HttpStatusCode.BadRequest,
            WrongOpponentException wrongOpponentException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };

        httpContext.Response.StatusCode = (int)statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Detail = exception.Message,
            },
            Exception = exception,
        });
    }
}
