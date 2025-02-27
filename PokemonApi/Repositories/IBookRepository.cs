using System;
using System.Threading.Tasks;
using PokemonApi.Models;

namespace PokemonApi.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid bookId);
        Task DeleteAsync(Book book);
    }
}
