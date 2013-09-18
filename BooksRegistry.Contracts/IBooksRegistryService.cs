using System.Collections.Generic;
using System.ServiceModel;
using SharedContracts;

namespace BooksRegistry.Contracts
{
    [ServiceContract]
    public interface IBooksRegistryService
    {
        List<Book> GetDetailsAboutBooks(BookKey[] books);
    }
}