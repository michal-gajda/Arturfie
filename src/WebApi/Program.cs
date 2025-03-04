namespace Arturfie.WebApi;

using Arturfie.Application;
using Arturfie.Infrastructure;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

internal sealed class Program
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

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Arturfie API",
                Version = "1.0",
                Description = "Arturfie API"
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Arturfie API",
                Version = "2.0",
                Description = "Arturfie API"
            });
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("X-Api-Version");
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        var app = builder.Build();

        app.UseHealthChecks("/health");

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Arturfie API v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Arturfie API v2");
            });
        }

        app.UseAuthorization();

        app.UseExceptionHandler();

        app.MapControllers();

        await app.RunAsync();

        return EXIT_SUCCESS;
    }
}
