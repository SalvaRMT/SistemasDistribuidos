using PokedexApi.Exceptions;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Models;
using PokedexApi.Repositories;

namespace PokedexApi.Services;

public class PokemonService : IPokemonService {

    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }
    public async Task<Pokemon?> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
    }
    public async Task<List<Pokemon>> GetPokemonByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _pokemonRepository.GetPokemonByNameAsync(name, cancellationToken);
    }
    public async Task<bool> DeletePokemonByIdAsync(Guid id, CancellationToken cancellationToken)
{
    return await _pokemonRepository.DeletePokemonByIdAsync(id, cancellationToken);
}
public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(pokemon.Name) || string.IsNullOrWhiteSpace(pokemon.Type))
    {
        throw new PokemonValidationException("El nombre y el tipo del Pokémon son obligatorios.");
    }

    if (pokemon.Level < 1 || pokemon.Level > 100)
    {
        throw new PokemonValidationException("El nivel debe estar entre 1 y 100.");
    }

    if (pokemon.Attack <= 0 || pokemon.Defense <= 0 || pokemon.Speed <= 0)
    {
        throw new PokemonValidationException("Los valores de ataque, defensa y velocidad deben ser mayores que 0.");
    }

    return await _pokemonRepository.CreatePokemonAsync(pokemon, cancellationToken);
}
public async Task UpdatePokemonAsync(Guid id, Pokemon pokemon, CancellationToken cancellationToken)
{
    var pokemons = await _pokemonRepository.GetPokemonByNameAsync(pokemon.Name, cancellationToken); 
    if (pokemons.Any(s =>s.Name.ToLower() == pokemon.Name.ToLower() &&s.Id != id)){
        throw new PokemonConflictException("Pokemon with the same name already exists");
    }
    if (pokemon.Level <= 0)
    {
        throw new PokemonValidationException("Level must be greater than 0");
    }

    pokemon.Id = id;
    await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
}
}
