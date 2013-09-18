using SharedContracts;

namespace Sales.Contracts.Commands
{
    public class BuyBook
    {
        public BookKey BookIAmBuying { get; set; }

        public CustomerKey Buyer { get; set; } 
    }
}