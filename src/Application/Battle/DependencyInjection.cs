namespace Arturfie.Application.Battle;

using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Services;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddBattle(this IServiceCollection services)
    {
        services.AddSingleton<IBattleService, BattleService>();

        return services;
    }
}
