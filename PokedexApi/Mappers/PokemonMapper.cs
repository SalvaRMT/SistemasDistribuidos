using PokedexApi.Models;
using PokedexApi.Dtos;
using PokedexApi.Infrastructure.Soap.Dtos;

namespace PokedexApi.Mappers;

public static class PokemonMappers
{
    public static PokemonResponse ToDto(this Pokemon pokemon) {
        return new PokemonResponse {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsResponse {
                Attack = pokemon.Attack,
                Defense = pokemon.Defense,
                Speed = pokemon.Speed
            }
        };
    }
    public static Pokemon ToModel(this PokemonResponseDto pokemon) {
        return new Pokemon {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Attack = pokemon.Stats.Attack,
            Defense = pokemon.Stats.Defense,
            Speed = pokemon.Stats.Speed
        };
    }
    public static List<Pokemon> ToModelList(this List<PokemonResponseDto> pokemon)
    {
        return pokemon?.Select(e => e.ToModel()).ToList() ?? new List<Pokemon>();
    }
    public static List<PokemonResponse> ToDtoList(this List<Pokemon> pokemon)
    {
        return pokemon?.Select(b => b.ToDto()).ToList() ?? new List<PokemonResponse>();
    }
}