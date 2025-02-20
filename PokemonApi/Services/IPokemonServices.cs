using System.ServiceModel;
using PokemonApi.Dto;

namespace PokemonApi.Services;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemonapi/pokemon-service")]
public interface IPokemonService
{
    [OperationContract]
    Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken);
}