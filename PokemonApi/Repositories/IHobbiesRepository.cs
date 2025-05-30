using PokemonApi.Models;

namespace PokemonApi.Repositories
{
    public interface IHobbiesRepository
    {
        Task<Hobbies> GetHobbyById(int id, CancellationToken cancellationToken);
        Task DeleteHobby(Hobbies hobbies, CancellationToken cancellationToken);
        Task<List<Hobbies>> GetHobbiesByName(string name, CancellationToken cancellationToken);
        Task AddHobbyAsync(Hobbies hobby, CancellationToken cancellationToken);
        Task UpdateHobbyAsync(Hobbies hobby, CancellationToken cancellationToken);
    }
}
