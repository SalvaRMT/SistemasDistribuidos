using PokedexApi.Models;

namespace PokedexApi.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Pokemon>> GetPokemonByNameAsync(string name, CancellationToken cancellationToken);
}