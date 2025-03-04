namespace Arturfie.Application.Battle.Queries;

public sealed record class DuelV2 : IRequest<string>
{
    public required string Character { get; init; }
    public required string Rival { get; init; }
}
