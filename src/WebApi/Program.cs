namespace Arturfie.WebApi;

using Arturfie.Application;
using Arturfie.Infrastructure;

public sealed class Program
{
    const int EXIT_SUCCESS = 0;

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddProblemDetails();

        builder.Services.AddExceptionHandler<BattleExceptionsHandler>();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseAuthorization();

        app.UseExceptionHandler();

        app.MapControllers();

        await app.RunAsync();

        return EXIT_SUCCESS;
    }
}
