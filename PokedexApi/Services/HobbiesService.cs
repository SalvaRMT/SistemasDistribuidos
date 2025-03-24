using PokedexApi.Models;
using PokedexApi.Repositories;

namespace PokedexApi.Services
{
    public class HobbiesService : IHobbiesService
    {
        private readonly IHobbiesRepository _hobbiesRepository;

        public HobbiesService(IHobbiesRepository hobbiesRepository)
        {
            _hobbiesRepository = hobbiesRepository;
        }

        public async Task<Hobbies?> GetHobbiesByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _hobbiesRepository.GetHobbiesByIdAsync(id, cancellationToken);
        }

        public async Task<List<Hobbies>> GetHobbiesByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _hobbiesRepository.GetHobbiesByNameAsync(name, cancellationToken);
        }

        public async Task<bool> DeleteHobbiesByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _hobbiesRepository.DeleteHobbiesByIdAsync(id, cancellationToken);
        }
    }
}
