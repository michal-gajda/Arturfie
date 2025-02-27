namespace Arturfie.Infrastructure.GitHub;

using Arturfie.Application.Battle.Interfaces;
using Arturfie.Infrastructure.GitHub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddGitHub(this IServiceCollection services, IConfiguration configuration)
    {
        var characters = configuration.GetValue<Uri>("Characters")!;
        var options = new GitHubOptions
        {
            Characters = characters,
        };

        services.AddSingleton<GitHubOptions>(options);
        services.AddSingleton<ICharacterProvider, CharacterProvider>();
        services.AddHttpClient();

        return services;
    }
}
