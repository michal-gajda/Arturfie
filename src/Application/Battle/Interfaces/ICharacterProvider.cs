namespace Arturfie.Application.Battle.Interfaces;

using Arturfie.Application.Battle.Models;

public interface ICharacterProvider
{
    Task<Character?> GetCharacterByNameAsync(string name, CancellationToken cancellationToken = default);
}
