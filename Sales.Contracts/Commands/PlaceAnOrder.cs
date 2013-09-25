using SharedContracts;

namespace Sales.Contracts.Commands
{
    public class PlaceAnOrder
    {
        public BookKey[] BooksIAmBuying { get; set; }

        public CustomerKey Buyer { get; set; } 
    }
}