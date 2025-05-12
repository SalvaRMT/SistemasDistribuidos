using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PokedexApi.Repositories
{
    public class HobbiesRepository : IHobbiesRepository
    {
        private readonly ILogger<HobbiesRepository> _logger;
        private readonly PokedexApi.Infrastructure.Soap.Contracts.IHobbiesService _hobbiesService;

        public HobbiesRepository(ILogger<HobbiesRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            var endpointUri = configuration.GetValue<string>("HobbiesServiceEndpoint");
            if (string.IsNullOrEmpty(endpointUri))
            {
                throw new InvalidOperationException("HobbiesServiceEndpoint is not configured properly.");
            }

            var endpoint = new EndpointAddress(endpointUri);
            var binding = new BasicHttpBinding();
            _hobbiesService = new ChannelFactory<PokedexApi.Infrastructure.Soap.Contracts.IHobbiesService>(binding, endpoint).CreateChannel();
        }

        public async Task<Hobbies?> GetHobbiesByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("Invalid GUID provided.");
                    return null;
                }

                var hobby = await _hobbiesService.GetHobbiesById(id, cancellationToken);
                return hobby?.ToModel();
            }
            catch (FaultException ex) when (ex.Message == "Hobby not found :(")
            {
                _logger.LogWarning(ex, "Failed to get hobby with id: {id}", id);
                return null;
            }
        }

        public async Task<List<Hobbies>> GetHobbiesByNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return new List<Hobbies>();
                var hobbies = await _hobbiesService.GetHobbiesByName(name, cancellationToken);
                return hobbies?.ToModelList() ?? new List<Hobbies>();
            }
            catch (FaultException ex) when (ex.Message == "Hobby not found :(")
            {
                _logger.LogWarning(ex, "Hobby not found: {name}", name);
                return new List<Hobbies>();
            }
        }

        public async Task<bool> DeleteHobbiesByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _hobbiesService.DeleteHobbies(id, cancellationToken);
            }
            catch (FaultException ex) when (ex.Message == "Hobby not found :(")
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hobby with id: {id}", id);
                throw;
            }
        }
    }
}
