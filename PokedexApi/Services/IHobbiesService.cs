using PokedexApi.Models;
namespace PokedexApi.Services;

public interface IHobbiesService
{
    Task<Hobbies?> GetHobbiesByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Hobbies>> GetHobbiesByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> DeleteHobbiesByIdAsync(Guid id, CancellationToken cancellationToken);
}