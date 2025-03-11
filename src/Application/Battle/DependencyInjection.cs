namespace Arturfie.Application.Battle;

using System.Diagnostics.CodeAnalysis;
using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Services;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddBattle(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IBattleService, BattleService>("v1");
        services.AddKeyedSingleton<IBattleService, BattleServiceV2>("v2");

        return services;
    }
}
