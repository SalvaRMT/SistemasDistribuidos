using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Dtos;

namespace PokedexApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "HobbiesService", Namespace ="http://pokemonapi/hobbies-service")]

public interface IHobbiesService{
    [OperationContract] 
    Task<HobbiesResponseDto> GetHobbiesById(Guid id, CancellationToken cancellationToken); 
    [OperationContract]
    Task<bool> DeleteHobbies(Guid id, CancellationToken cancellationToken);
    [OperationContract]
    Task<List<HobbiesResponseDto>> GetHobbiesByName(string name, CancellationToken cancellationToken);
    [OperationContract]
    Task<HobbiesResponseDto> CreateHobbies(CreateHobbiesDto createHobby, CancellationToken cancellationToken);
    [OperationContract]
    Task<HobbiesResponseDto> UpdateHobbies(UpdateHobbiesDto hobby, CancellationToken cancellationToken);
}