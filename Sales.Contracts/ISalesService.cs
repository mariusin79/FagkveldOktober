using System.Collections.Generic;
using System.ServiceModel;

namespace Sales.Contracts
{
    [ServiceContract]
    public interface ISalesService
    {
        [OperationContract]
        List<AvailableBook> FindBooksAvailableForSale();
    }
}