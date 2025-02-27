# Arturfie

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
