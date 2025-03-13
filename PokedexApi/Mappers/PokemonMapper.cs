using PokedexApi.Infrastructure.Soap.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Mappers
{
    public static class PokemonMapper
    {
        public static Pokemon ToModel(this PokemonResponseDto dto)
        {
            if (dto == null) return null;
            return new Pokemon
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                Level = dto.Level,
                Attack = dto.Stats.Attack,
                Defense = dto.Stats.Defense,
                Speed = dto.Stats.Speed
            };
        }
    }
}
