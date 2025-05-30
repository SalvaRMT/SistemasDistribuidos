using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using PokemonApi.Dto;
using PokemonApi.Mappers;
using PokemonApi.Repositories;
using PokemonApi.Validators;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetByIdAsync(id, cancellationToken);
        if (pokemon is null)
        {
            throw new InvalidOperationException("Pokemon not found :(");
        }
        return pokemon.ToDto();
    }

    public async Task<bool> DeletePokemon(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetByIdAsync(id, cancellationToken);
        if (pokemon is null)
        {
            throw new InvalidOperationException("Pokemon not found :(");
        }
        await _pokemonRepository.DeleteAsync(pokemon, cancellationToken);
        return true;
    }

    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken)
    {
        var pokemonToCreate = pokemon.ToModel();  

        pokemonToCreate.ValidateName()
                       .ValidateLevel()
                       .ValidateType();

        await _pokemonRepository.AddAsync(pokemonToCreate, cancellationToken);
        return pokemonToCreate.ToDto();}
    
    public async Task<PokemonResponseDto> UpdatePokemonAsync(UpdatePokemonDto pokemon, CancellationToken cancellationToken)
    {
        var pokemonToUpdate = await _pokemonRepository.GetByIdAsync(pokemon.Id, cancellationToken);
        if (pokemonToUpdate is null)
        {
            throw new FaultException("Pokemon not found :(");
        }
        pokemonToUpdate.Name = pokemon.Name;
        pokemonToUpdate.Level = pokemon.Level;
        pokemonToUpdate.Type = pokemon.Type;
        pokemonToUpdate.Stats.Attack = pokemon.Stats.Attack;
        pokemonToUpdate.Stats.Defense = pokemon.Stats.Defense;
        pokemonToUpdate.Stats.Speed = pokemon.Stats.Speed;
        pokemonToUpdate.Stats.weitgh = pokemon.Stats.weitgh;

        await _pokemonRepository.UpdateAsync(pokemonToUpdate, cancellationToken);
        return pokemonToUpdate.ToDto();
    }
    public async Task<List<PokemonResponseDto>> GetPokemonByName(string name,CancellationToken cancellationToken){


    var pokemons = await _pokemonRepository.GetPokemonsByNameAsync(name, cancellationToken);


    if (pokemons == null || !pokemons.Any())
    {
        return new List<PokemonResponseDto>();
    }

    return pokemons.Select(h => h.ToDto()).ToList();
    }
}
