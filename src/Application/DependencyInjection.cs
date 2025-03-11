namespace Arturfie.Application;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Arturfie.Application.Battle;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddBattle();

        return services;
    }
}
