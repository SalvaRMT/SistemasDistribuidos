using PokemonApi.Models;

namespace PokemonApi.Repositories
{
    public interface IPokemonRepository
    {
        Task GetPokemonById(Guid id, CancellationToken cancellationToken);
        Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}