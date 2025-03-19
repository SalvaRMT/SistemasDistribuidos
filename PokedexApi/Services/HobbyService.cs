using PokedexApi.Models;
using PokedexApi.Repositories;

namespace PokedexApi.Services;

public class HobbyService : IHobbyService
{
    private readonly IHobbyRepository _hobbyRepository;
    public HobbyService(IHobbyRepository hobbyRepository)
    {
        _hobbyRepository = hobbyRepository;
    }
    public async Task<Hobby?> GetHobbyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _hobbyRepository.GetHobbyByIdAsync(id, cancellationToken);
    }

    public async Task<List<Hobby>> GetHobbyByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _hobbyRepository.GetHobbyByNameAsync(name, cancellationToken);
    }
}