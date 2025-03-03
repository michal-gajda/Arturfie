namespace Arturfie.WebApi;

using Arturfie.Application;
using Arturfie.Infrastructure;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public sealed class Program
{
    const int EXIT_SUCCESS = 0;
    const string SERVICE_NAME = "Arturfie";
    const string SERVICE_VERSION = "1.0.0";

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var serviceInstanceId = builder.Environment.EnvironmentName;

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddProblemDetails();

        builder.Services.AddExceptionHandler<BattleExceptionsHandler>();

        builder.Services.AddHealthChecks();

        builder.Logging.AddOpenTelemetry(options =>
        {
            var resourceBuilder = ResourceBuilder.CreateDefault()
                .AddService(serviceName: SERVICE_NAME, serviceVersion: SERVICE_VERSION, autoGenerateServiceInstanceId: false, serviceInstanceId: serviceInstanceId);

            options.SetResourceBuilder(resourceBuilder)
                .AddOtlpExporter();
        });

        builder.Services.AddOpenTelemetry()
              .ConfigureResource(resource => resource.AddService(serviceName: SERVICE_NAME, serviceVersion: SERVICE_VERSION, autoGenerateServiceInstanceId: false, serviceInstanceId: serviceInstanceId))
              .WithTracing(tracing => tracing
                  .AddAspNetCoreInstrumentation()
                  .AddHttpClientInstrumentation()
                  .AddOtlpExporter())
              .WithMetrics(metrics => metrics
                  .AddAspNetCoreInstrumentation()
                  .AddHttpClientInstrumentation()
                  .AddRuntimeInstrumentation()
                  .AddOtlpExporter());

        builder.Services.AddSingleton(TracerProvider.Default.GetTracer(SERVICE_NAME, SERVICE_VERSION));

        builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.UseHealthChecks("/health");

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
