using System.Runtime.Serialization;

namespace PokemonApi.Dto
{
    [DataContract(Name = "CreateHobbiesDto", Namespace = "http://pokemonapi/hobbies-service")]
    public class CreateHobbiesDto
    {
        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }

        [DataMember(Name = "Top", Order = 2)]
        public int Top { get; set; }
    }
}
