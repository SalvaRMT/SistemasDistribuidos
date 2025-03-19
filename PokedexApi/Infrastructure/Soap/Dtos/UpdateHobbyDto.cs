using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Dtos;
[DataContract(Name ="UpdateHobbyDto", Namespace ="http://hobby-api/hobby-service")]
public class UpdateHobbyDto : HobbyCommonDto{
    [DataMember(Name ="Id", Order = 3)]
    public Guid Id {get; set;}
}