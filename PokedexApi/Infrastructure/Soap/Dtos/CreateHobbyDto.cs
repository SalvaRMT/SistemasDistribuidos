using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Dtos;
[DataContract(Name ="CreateHobbyDto", Namespace ="http://hobby-api/hobby-service")]
public class CreateHobbyDto : HobbyCommonDto{

}