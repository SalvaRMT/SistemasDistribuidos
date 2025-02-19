using System.Runtime.Serialization;

namespace PokemonApi.Dto;

[DataContract(Name = "StatsDto", Namespace = "http://pokemonapi/pokemon-service")]
public class statsDto
{
    [DataMember(Name = "attack", Order = 1)]
    public int Attack { get; set; }

    [DataMember(Name = "defense", Order = 2)]
    public int Defense { get; set; }

    [DataMember(Name = "speed", Order = 3)]
    public int Speed { get; set; }

    [DataMember(Name = "weitgh", Order = 4)]
    public int weitgh { get; set; }
}