namespace Arturfie.Infrastructure.GitHub;

using System.Diagnostics.CodeAnalysis;
using Arturfie.Application.Battle.Interfaces;
using Arturfie.Infrastructure.GitHub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddGitHub(this IServiceCollection services, IConfiguration configuration)
    {
        var characters = configuration.GetValue<Uri>(GitHubOptions.SectionName)!;
        var options = new GitHubOptions
        {
            Characters = characters,
        };

        services.AddSingleton(options);
        services.AddSingleton<ICharacterProvider, CharacterProvider>();
        services.AddHttpClient();

        return services;
    }
}
