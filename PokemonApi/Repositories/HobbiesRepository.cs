using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;
using PokemonApi.Infrastructure;
using PokemonApi.Mappers;

namespace PokemonApi.Repositories
{
    public class HobbiesRepository : IHobbiesRepository
    {
        private readonly RelationalDbContext _context;
        public HobbiesRepository(RelationalDbContext context)
        {
            _context = context;
        }

        public async Task<Hobbies> GetHobbyById(int id, CancellationToken cancellationToken)
        {
            var hobbyEntity = await _context.Hobbies
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            return hobbyEntity?.ToModel();
        }

        public async Task DeleteHobby(Hobbies hobbies, CancellationToken cancellationToken)
        {
            _context.Hobbies.Remove(hobbies.ToEntity());
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Hobbies>> GetHobbiesByName(string name, CancellationToken cancellationToken)
        {
            var hobbyEntities = await _context.Hobbies
                .AsNoTracking()
                .Where(h => EF.Functions.Like(h.Name, $"%{name}%"))
                .ToListAsync(cancellationToken);
            return hobbyEntities.Select(h => h.ToModel()).ToList();
        }

        public async Task AddHobbyAsync(Hobbies hobby, CancellationToken cancellationToken)
        {
            await _context.Hobbies.AddAsync(hobby.ToEntity(), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateHobbyAsync(Hobbies hobby, CancellationToken cancellationToken)
        {
            _context.Hobbies.Update(hobby.ToEntity());
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
