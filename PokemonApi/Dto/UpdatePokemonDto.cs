using System.Runtime.Serialization;

namespace PokemonApi.Dto;

[DataContract(Name = "UpdatePokemonDto", Namespace = "http://pokemonapi/pokemon-service")]
public class UpdatePokemonDto : PokemonCommonDto
{
    [DataMember(Name = "id", Order = 5)]
    public Guid Id { get; set; }

}
