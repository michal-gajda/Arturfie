namespace Arturfie.Application.FunctionalTests;

using Arturfie.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public abstract class ApplicationBootstrapperBase
{
    protected readonly ServiceProvider serviceProvider;
    protected ApplicationBootstrapperBase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddUserSecrets<ApplicationBootstrapperBase>()
            .Build();

        var services = new ServiceCollection();

        services.AddLogging(cfg => cfg.AddConsole());

        services.AddApplication();
        services.AddInfrastructure(configuration);

        this.serviceProvider = services.BuildServiceProvider();
    }
}
