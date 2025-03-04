using System.Runtime.Serialization;

namespace PokemonApi.Dto;

[DataContract (Name = "CreatePokemonDto", Namespace = "http://pokemonapi/pokemon-service")]
public class CreatePokemonDto : PokemonCommonDto
{

}
