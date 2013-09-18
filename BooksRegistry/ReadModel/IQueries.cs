using System.Collections.Generic;
using BooksRegistry.Contracts;
using SharedContracts;

namespace BooksRegistry.ReadModel
{
    public interface IQueries
    {
        IEnumerable<Book> GetDetailsAboutBooks(BookKey[] books);
         
    }
}