using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;

namespace PokemonApi.Services
{
    public class BookService : IBookService
    {
        private readonly RelationalDbContext _context;

        public BookService(RelationalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBook(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                throw new Exception("BookNotFound");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
