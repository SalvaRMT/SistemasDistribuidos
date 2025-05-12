using PokedexApi.Models;

namespace PokedexApi.Repositories;

public interface IHobbiesRepository
{
    Task<Hobbies?> GetHobbiesByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Hobbies>> GetHobbiesByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> DeleteHobbiesByIdAsync(Guid id, CancellationToken cancellationToken);
}