using NServiceBus;
using SharedContracts;

namespace Sales.Contracts.Commands
{
    public interface IPlaceAnOrder : ICommand
    {
        BookKey[] BooksIAmBuying { get; set; }

        CustomerKey Buyer { get; set; } 
    }
}