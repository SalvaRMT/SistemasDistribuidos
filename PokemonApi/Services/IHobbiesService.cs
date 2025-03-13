using System.ServiceModel;
using PokemonApi.Dto;

namespace PokemonApi.Services
{
    [ServiceContract(Name = "AlanSalvadorHobbiesService", Namespace = "http://pokemonapi/hobbies-service")]
    public interface IHobbiesService
    {
         [OperationContract]
         Task<HobbiesResponseDto> GetHobbiesById(int id, CancellationToken cancellationToken);

         [OperationContract]
         Task<bool> DeleteHobbies(int id, CancellationToken cancellationToken);

         [OperationContract]
         Task<List<HobbiesResponseDto>> GetHobbieByName(string name, CancellationToken cancellationToken);

         [OperationContract]
         Task<HobbiesResponseDto> CreateHobbies(CreateHobbiesDto createHobbiesDto, CancellationToken cancellationToken);

         [OperationContract]
         Task<HobbiesResponseDto> UpdateHobbies(UpdateHobbiesDto updateHobbiesDto, CancellationToken cancellationToken);
    }
}
