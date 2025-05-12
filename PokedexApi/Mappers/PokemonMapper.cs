using PokedexApi.Models;
using PokedexApi.Dtos;
using PokedexApi.Infrastructure.Soap.Dtos;
using Microsoft.AspNetCore.StaticFiles;
using System.CodeDom;

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
    public static Pokemon ToModel(this CreatePokemonRequest pokemon)
    {
        return new Pokemon {
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Attack = pokemon.Attack,
            Defense = pokemon.Defense,
            Speed = pokemon.Speed
        };
    }
    public static CreatePokemonDto ToSoapDto(this Pokemon pokemon)
    {
        return new CreatePokemonDto {
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto {
                Attack = pokemon.Attack,
                Defense = pokemon.Defense,
                Speed = pokemon.Speed
        }
    };
}
public static Pokemon ToModel(this UpdatePokemonRequest pokemon)
{
    return new Pokemon {
        Name = pokemon.Name,
        Type = pokemon.Type,
        Level = pokemon.Level,
        Attack = pokemon.Attack,
        Defense = pokemon.Defense,
        Speed = pokemon.Speed
    };
}
public static UpdatePokemonDto ToUpdateSoapDto(this Pokemon pokemon)
{
    return new UpdatePokemonDto {
        Id = pokemon.Id,
        Name = pokemon.Name,
        Type = pokemon.Type,
        Level = pokemon.Level,
        Stats = new StatsDto {
            Attack = pokemon.Attack,
            Defense = pokemon.Defense,
            Speed = pokemon.Speed
        }
    };
}
}