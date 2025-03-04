namespace Arturfie.Application.Battle.Queries;

public sealed record class DuelV2 : IRequest<string>
{
    public required string Hero { get; init; }
    public required string Villain { get; init; }
}
