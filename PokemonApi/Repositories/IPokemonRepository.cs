using System;
using System.Threading.Tasks;
using System.Threading;
using PokemonApi.Models;

namespace PokemonApi.Repositories;

    public interface IPokemonRepository
    {
        Task<Pokemon> GetByIdAsync(Guid id, CancellationToken cancellationToken);
         Task DeleteAsync(Pokemon pokemon, CancellationToken cancellationToken);
    }

