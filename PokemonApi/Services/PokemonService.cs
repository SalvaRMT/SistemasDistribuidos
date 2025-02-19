using System.ServiceModel;
using PokemonApi.Dto;
using PokemonApi.Mappers;
using PokemonApi.Repositories;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken){
        var pokemon = await _pokemonRepository.GetByIdAsync(id, cancellationToken);
        if (pokemon is null){
            throw new FaultException("Pokemon not found:(");
        }
        return pokemon.ToDto();
    }
}