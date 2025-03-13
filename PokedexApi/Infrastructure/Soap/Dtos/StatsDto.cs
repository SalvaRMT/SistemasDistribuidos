using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Dtos;

[DataContract(Name = "StatsDto", Namespace = "http://pokemonapi/pokemon-service")]
public class StatsDto {
    [DataMember(Name = "attack", Order = 1)]
    public int Attack { get; set; }

    [DataMember(Name = "defense", Order = 2)]
    public int Defense { get; set; }

    [DataMember(Name = "speed", Order = 3)]
    public int Speed { get; set; }
}