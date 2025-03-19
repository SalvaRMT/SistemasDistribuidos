using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Dtos;

namespace PokedexApi.Infrastructure.Soap.Contracts
{
    [ServiceContract(Name = "PokemonService", Namespace = "http://pokemonapi/pokemon-service")]
    public interface IPokemonService
    {
        [OperationContract]
        Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken);
        [OperationContract]
        Task<List<PokemonResponseDto>> GetPokemonByName(string name, CancellationToken cancellationToken);
        [OperationContract]
        Task<bool> DeletePokemonAsync(Guid id, CancellationToken cancellationToken);

        [OperationContract]
        Task<PokemonResponseDto> CreatePokemonAsync(CreatePokemonDto createPokemonDto, CancellationToken cancellationToken);

        [OperationContract]
        Task<PokemonResponseDto> UpdatePokemonAsync(UpdatePokemonDto pokemon, CancellationToken cancellationToken);
    }
}

