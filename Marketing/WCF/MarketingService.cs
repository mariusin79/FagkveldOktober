using System.Collections.Generic;
using Marketing.Contracts;
using SharedContracts;

namespace Marketing.WCF
{
    public class MarketingService : IMarketingService
    {
        public List<SoldBook> FindBooksWhoPeopleAlsoBoughtWhenTheyBought(BookKey book)
        {
            throw new System.NotImplementedException();
        }
    }
}