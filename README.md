# Arturfie

- [Superheroes Tech Test](https://gist.github.com/arturfie/417061f4ca9627abc176fd905b24cf2b)
- [Characters](https://gist.githubusercontent.com/arturfie/1594a132dbf76a977503136a5b928e92/raw/a83cdb719e0d80093ce69100009477692a06e4be/characters.json)

## sln

```powershell
dotnet new gitignore
git init
dotnet new sln --name Arturfie
```

## WebApi

```powershell
dotnet new webapi --no-https --use-controllers --use-program-main --output src/WebApi --name Arturfie.WebApi
dotnet sln add src/WebApi
dotnet user-secrets init --project src/WebApi
```

## Application

```powershell
dotnet new classlib --output src/Application --name Arturfie.Application
dotnet sln add src/Application
dotnet add src/Application package MediatR
dotnet add src/Application package Microsoft.Extensions.Logging.Abstractions
```

## Infrastructure

```powershell
dotnet new classlib --output src/Infrastructure --name Arturfie.Infrastructure
dotnet sln add src/Infrastructure
dotnet add src/Infrastructure reference src/Application
dotnet add src/WebApi reference src/Infrastructure
dotnet add src/Infrastructure package Microsoft.Extensions.Configuration.Binder
```

## Clean Architecture

### DependencyInjection in Application (DependencyInjection.cs)

```csharp
namespace Arturfie.Application;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
```

### DependencyInjection in Infrastructure (DependencyInjection.cs)

```csharp
namespace Arturfie.Infrastructure;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
```

### Registering Application and Infrastructure in WebApi

```csharp
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
```

## MSTest

```powershell
dotnet new mstest --output tests/Application.FunctionalTests --name Arturfie.Application.FunctionalTests
dotnet sln add tests/Application.FunctionalTests
dotnet add tests/Application.FunctionalTests reference src/Infrastructure
```

### Create FunctionalTests Bootstrapper

```powershell
dotnet add tests/Application.FunctionalTests package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add tests/Application.FunctionalTests package Microsoft.Extensions.Configuration.Json
dotnet add tests/Application.FunctionalTests package Microsoft.Extensions.Configuration.UserSecrets
dotnet add tests/Application.FunctionalTests package Microsoft.Extensions.DependencyInjection
dotnet add tests/Application.FunctionalTests package Microsoft.Extensions.Logging
```

### Add FluentAssertions

```powershell
dotnet add tests/Application.FunctionalTests package FluentAssertions
```

```csharp
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
            .AddEnvironmentVariables()
            .AddUserSecrets<ApplicationBootstrapperBase>()
            .Build();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        this.serviceProvider = services.BuildServiceProvider();
    }
}
```

```xml
<ItemGroup>
  <Content Include="appsettings.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

```powershell
dotnet user-secrets init
```

## Add Application UnitTests

```powershell
dotnet new mstest --output tests/Application.UnitTests --name Arturfie.Application.UnitTests
dotnet sln add tests/Application.UnitTests
dotnet add tests/Application.UnitTests reference src/Application
dotnet add tests/Application.UnitTests package Microsoft.Extensions.Logging
```

```powershell
dotnet user-secrets set "Characters" "" --project src/WebApi
dotnet user-secrets set "Characters" "" --project tests/Application.FunctionalTests
```

```powershell
dotnet test --logger "console;verbosity=detailed"
```

```powershell
dotnet new mstest --output tests/WebApi.IntegrationTests --name Arturfie.WebApi.IntegrationTests
dotnet sln add tests/WebApi.IntegrationTests
dotnet add tests/WebApi.IntegrationTests reference src/WebApi
dotnet add tests/WebApi.IntegrationTests package FluentAssertions
dotnet add tests/WebApi.IntegrationTests package Microsoft.AspNetCore.Mvc.Testing
```

## OpenTelemetry

```powershell
dotnet add src/WebApi package OpenTelemetry.Exporter.OpenTelemetryProtocol
dotnet add src/WebApi package OpenTelemetry.Extensions.Hosting
dotnet add src/WebApi package OpenTelemetry.Instrumentation.AspNetCore
dotnet add src/WebApi package OpenTelemetry.Instrumentation.Http
dotnet add src/WebApi package OpenTelemetry.Instrumentation.Runtime
```
