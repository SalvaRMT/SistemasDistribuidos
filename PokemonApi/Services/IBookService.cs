using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PokemonApi.Services
{
    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        Task<bool> DeleteBook(Guid bookId);
    }
}
