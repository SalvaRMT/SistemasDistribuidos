using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Dtos;

namespace PokedexApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "HobbyService", Namespace ="http://hobby-api/hobby-service")]

public interface IHobbyService{
    [OperationContract] 
    Task<HobbyResponseDto> GetHobbyById(Guid id, CancellationToken cancellationToken); 
    [OperationContract]
    Task<bool> DeleteHobby(Guid id, CancellationToken cancellationToken);
    [OperationContract]
    Task<List<HobbyResponseDto>> GetHobbyByName(string name, CancellationToken cancellationToken);
    [OperationContract]
    Task<HobbyResponseDto> CreateHobby(CreateHobbyDto createHobby, CancellationToken cancellationToken);
    [OperationContract]
    Task<HobbyResponseDto> UpdateHobby(UpdateHobbyDto hobby, CancellationToken cancellationToken);
}