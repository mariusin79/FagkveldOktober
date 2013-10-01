using NServiceBus;
using Sales.Contracts.Commands;

namespace Sales.PlaceAnOrder
{
    public class HandleMessage : IHandleMessages<IPlaceAnOrder>
    {
        public void Handle(IPlaceAnOrder message)
        {
            throw new System.NotImplementedException();
        }
    }
}