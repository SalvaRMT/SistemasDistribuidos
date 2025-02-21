using System.Runtime.Serialization;

namespace PokemonApi.Dto;

[DataContract (Name = "CreatePokemonDto", Namespace = "http://pokemonapi/pokemon-service")]
public class CreatePokemonDto{
    [DataMember(Name = "Name", Order = 1)]
    public string Name { get; set; }
    [DataMember(Name = "Level", Order = 2)]
    public int Level { get; set; }
    [DataMember(Name = "Type", Order = 3)]
    public string Type { get; set; }
    [DataMember(Name = "Stats", Order = 4)]
    public StatsDto Stats { get; set; }
}
