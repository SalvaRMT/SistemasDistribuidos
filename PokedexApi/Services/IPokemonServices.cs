using PokedexApi.Models;


namespace PokedexApi.Services;

public interface IPokemonService
{
    Task<Pokemon?> GetPokemonById(Guid id, CancellationToken cancellationToken);
    Task<List<Pokemon>> GetPokemonByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> DeletePokemonByIdAsync(Guid id, CancellationToken cancellationToken);
}