using PokemonApi.Dtos;
using PokemonApi.Repositories;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    //TODO - Unit Test (Test driven development)

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken)
{
    var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);

    if (pokemon is null)
    {
        return null; 
    }

    return new PokemonResponseDto(pokemon.Id, pokemon.Name, pokemon.Type);
}
}                       