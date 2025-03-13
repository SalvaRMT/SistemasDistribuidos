using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;

namespace PokemonApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RelationalDbContext _context;

        public BookRepository(RelationalDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByIdAsync(Guid bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
