using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Mappers;
using PokedexApi.Models;

namespace PokedexApi.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ILogger<HobbyRepository> _logger;
        private readonly IHobbyService _hobbyService;

        public HobbyRepository(ILogger<HobbyRepository> logger, IConfiguration configuration)
        {
            _logger = logger;

            var endpointUri = configuration.GetValue<string>("HobbyServiceEndpoint");
            var endpoint = new EndpointAddress(endpointUri);
            var binding = new BasicHttpBinding();

            _hobbyService = new ChannelFactory<IHobbyService>(binding, endpoint).CreateChannel();
        }

        public async Task<Hobby?> GetHobbyByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("Invalid GUID provided.");
                    return null;
                }

                var hobby = await _hobbyService.GetHobbyById(id, cancellationToken);
                return hobby.ToModel();
            }
            catch (FaultException ex) when (ex.Message == "Hobby not found :(")
            {
                _logger.LogWarning(ex, "Failed to get hobby with id: {id}", id);
                return null;
            }
        }

        public async Task<List<Hobby>> GetHobbyByNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogWarning("Invalid hobby name provided.");
                    return new List<Hobby>();
                }

                var hobby = await _hobbyService.GetHobbyByName(name, cancellationToken);
                return hobby.ToModelList();
            }
            catch (FaultException ex) when (ex.Message == "Hobby not found :(")
            {
                _logger.LogWarning(ex, "Failed to get hobby with name: {name}", name);
                return new List<Hobby>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting hobby with name: {name}", name);
                throw; 
            }
        }
    }
}
