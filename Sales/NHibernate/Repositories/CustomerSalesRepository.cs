using System.Linq;
using NHibernate.Linq;
using Sales.PlaceAnOrder;
using SharedContracts;

namespace Sales.NHibernate.Repositories
{
    public class CustomerSalesRepository : ICustomerSalesRepository
    {
        private readonly ISessionManager _sessionManager;

        public CustomerSalesRepository(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public CustomerSales Get(CustomerKey key)
        {
            using (var sessionUsage = _sessionManager.OpenSession())
            {
                return sessionUsage.Session.Query<CustomerSales>().First(customer => customer.CustomerKey == key);
            }
        }
    }
}