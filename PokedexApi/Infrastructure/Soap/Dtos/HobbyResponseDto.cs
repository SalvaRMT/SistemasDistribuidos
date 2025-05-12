using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Dtos;
[DataContract(Name = "HobbyResponseDto", Namespace = "http://hobby-api/hobby-service")]
public class HobbyResponseDto
{
    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Name", Order = 2)]
    public string Name { get; set; }

    [DataMember(Name = "Top", Order = 3)]
    public int Top { get; set; }
}