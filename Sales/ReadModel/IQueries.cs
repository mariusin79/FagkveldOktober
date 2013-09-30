using System.Collections.Generic;
using SharedContracts;

namespace Sales.ReadModel
{
    public interface IQueries
    {
        IEnumerable<RmAvailableBook> FindBooksAvailableForSale();
        IEnumerable<RmAvailableBook> GetBooksAvailableForSale(BookKey[] books);
    }
}