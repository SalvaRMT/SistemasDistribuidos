using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;
using PokemonApi.Dtos;

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
                Defense = entity.Desense, // <- Corregido "Desense" -> "Defense"
                Speed = entity.Speed
            }
        };
    } // <- Cierre correcto del método ToModel()

    public static PokemonResponseDto ToDto(this Pokemon pokemon)
    {
        if (pokemon is null)
        {
            return null;
        }

        return new PokemonResponseDto(pokemon.Id, pokemon.Name, pokemon.Type)
        {
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,  // <- Ahora está correctamente definido
                Speed = pokemon.Stats.Speed,    // <- Ahora está correctamente definido
                Desense = pokemon.Stats.Defense // <- Corregido "Desense" -> "Defense"
            }
        };
    } // <- Cierre correcto del método ToDto()

} // <- Cierre correcto de la clase PokemonMapper
