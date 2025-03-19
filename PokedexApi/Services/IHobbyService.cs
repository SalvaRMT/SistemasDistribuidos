using PokedexApi.Models;
namespace PokedexApi.Services;

public interface IHobbyService
{
    Task<Hobby?> GetHobbyByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Hobby>> GetHobbyByNameAsync(string name, CancellationToken cancellationToken);
}