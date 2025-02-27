namespace Arturfie.Infrastructure.GitHub;

internal sealed record class GitHubOptions
{
    public readonly static string SectionName = "Characters";
    public required Uri Characters { get; init; }
}
