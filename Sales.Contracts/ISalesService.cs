using System.Collections.Generic;
using System.ServiceModel;
using SharedContracts;

namespace Sales.Contracts
{
    [ServiceContract]
    public interface ISalesService
    {
        [OperationContract]
        List<AvailableBook> FindBooksAvailableForSale();

        [OperationContract]
        List<AvailableBook> GetBooksAvailableForSale(BookKey[] books);
    }
}