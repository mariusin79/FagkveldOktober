using NServiceBus;
using Sales.Contracts;
using Sales.Contracts.Commands;

namespace Sales.PlaceAnOrder
{
    public class HandleMessage : IHandleMessages<IPlaceAnOrder>
    {
        private readonly IBus _bus;
        private readonly ICustomerSalesRepository _customerSales;

        public HandleMessage(IBus bus, ICustomerSalesRepository customerSales)
        {
            _bus = bus;
            _customerSales = customerSales;
        }

        public void Handle(IPlaceAnOrder message)
        {
            // do your CRUD, DDD, JFHCI
            CustomerSales customerSales = _customerSales.Get(message.Buyer);
            customerSales.PlaceAnOrder(message.BooksIAmBuying);
            _bus.Publish<IBooksWereSold>(msg => { msg.Books = message.BooksIAmBuying; });
        }
    }
}