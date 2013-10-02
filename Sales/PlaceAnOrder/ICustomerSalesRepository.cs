using SharedContracts;

namespace Sales.PlaceAnOrder
{
    public interface ICustomerSalesRepository
    {
        CustomerSales Get(CustomerKey key);
    }
}