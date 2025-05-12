using PokedexApi.Models;

namespace PokedexApi.Repositories;

public interface IHobbyRepository
{
    Task<Hobby?> GetHobbyByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Hobby>> GetHobbyByNameAsync(string name, CancellationToken cancellationToken);
}