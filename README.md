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
