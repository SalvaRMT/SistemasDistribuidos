using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;
using PokemonApi.Dto;

namespace PokemonApi.Mappers;

public static class PokemonMapper
{
        public static PokemonEntity ToEntity(this Pokemon pokemon)
        {
            return new PokemonEntity
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Level = pokemon.Level,
                Type = pokemon.Type,
                Attack = pokemon.Stats.Attack,
                Desense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed,
                weitgh = pokemon.Stats.weitgh
            };

            }
    public static Pokemon ToModel(this PokemonEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new Pokemon
        {
            Id = entity.Id,
            Name = entity.Name,
            Level = entity.Level,
            Stats = new Stats
            {
                Attack = entity.Attack,
                Defense = entity.Desense, 
                Speed = entity.Speed,
                weitgh = entity.weitgh
            }

        };
    } 

    public static PokemonResponseDto ToDto(this Pokemon pokemon){
    return new PokemonResponseDto{
        Id = pokemon.Id,
        Name = pokemon.Name,
        Level = pokemon.Level,
        Type = pokemon.Type,
        Stats = new StatsDto
        {
            Attack = pokemon.Stats.Attack,
            Defense = pokemon.Stats.Defense,
            Speed = pokemon.Stats.Speed,
            weitgh = pokemon.Stats.weitgh
        }
    };
    }
    public static Pokemon ToModel(this CreatePokemonDto pokemon) {
        return new Pokemon {
            Id = Guid.NewGuid(),
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = pokemon.Stats.ToModel()
        };
    }

    public static Stats ToModel(this StatsDto stats) {
        return new Stats {
            Attack = stats.Attack,
            Defense = stats.Defense,
            Speed = stats.Speed,
            weitgh = stats.weitgh
        };
    }

}