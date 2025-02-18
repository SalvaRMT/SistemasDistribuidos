using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;
using PokemonApi.Mappers;
using PokemonApi.Infrastructure;

namespace PokemonApi.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly RelationalDbContext _context;

    public PokemonRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public Task GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Console.WriteLine(_context.Pokemon.GetType()); 

        var pokemon = await ((DbSet<Pokemon>)_context.Pokemon).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return pokemon;
    }
}
