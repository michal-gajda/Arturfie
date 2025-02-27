namespace Arturfie.Application.FunctionalTests;

using Arturfie.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public abstract class ApplicationBootstrapperBase
{
    protected readonly ServiceProvider serviceProvider;
    protected ApplicationBootstrapperBase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<ApplicationBootstrapperBase>()
            .Build();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        this.serviceProvider = services.BuildServiceProvider();
    }
}
