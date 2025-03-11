namespace Arturfie.Infrastructure.GitHub;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal sealed record class GitHubOptions
{
    public readonly static string SectionName = "Characters";
    public required Uri Characters { get; init; }
}
