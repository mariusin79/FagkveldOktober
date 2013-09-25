using System.Collections.Generic;
using System.ServiceModel;
using SharedContracts;

namespace Marketing.Contracts
{
    [ServiceContract]
    public interface IMarketingService
    {
        [OperationContract]
        List<SoldBook> FindBooksWhoPeopleAlsoBoughtWhenTheyBought(BookKey book);
    }
}