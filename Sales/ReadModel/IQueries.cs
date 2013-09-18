using System.Collections.Generic;

namespace Sales.ReadModel
{
    public interface IQueries
    {
        IEnumerable<RmAvailableBook> FindBooksAvailableForSale();
    }
}