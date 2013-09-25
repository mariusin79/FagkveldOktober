using NServiceBus;
using SharedContracts;

namespace Sales.Contracts
{
    public interface IBooksWereSold : IEvent
    {
        BookKey[] Books { get; set; } 
    }
}