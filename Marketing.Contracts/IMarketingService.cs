using System.Collections.Generic;
using System.ServiceModel;
using SharedContracts;

namespace Marketing.Contracts
{
    [ServiceContract]
    public interface IMarketingService
    {
        List<SoldBook> FindBooksWhoPeopleAlsoBoughtWhenTheyBought(BookKey book);
    }
}