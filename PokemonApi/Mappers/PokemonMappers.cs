using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;
using PokemonApi.Dto;

namespace PokemonApi.Mappers;

public static class PokemonMapper
{
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
        Stats = new statsDto
        {
            Attack = pokemon.Stats.Attack,
            Defense = pokemon.Stats.Defense,
            Speed = pokemon.Stats.Speed,
            weitgh = pokemon.Stats.weitgh
        }
    };
}
}
