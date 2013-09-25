using System.ServiceModel;
using NServiceStub.Configuration;
using Sales.Contracts;
using SharedContracts;
using NServiceStub.NServiceBus;

namespace Testing
{
    public class Class1
    {
        public void TryToSayHelloToTheSalesService()
        {
            using (var factory = new ChannelFactory<ISalesService>(new BasicHttpBinding(), "http://localhost:9101/sales"))
            {
                ISalesService service = factory.CreateChannel();

                service.FindBooksAvailableForSale();
            }
        }

        public void TellMarketingThatABookHasBeenSold()
        {
            var stub = Configure.Stub().NServiceBusSerializers().Create("servicehost");

            stub.Setup().Send<IBooksWereSold>(@event =>
                {
                    @event.Books = new[]
                        {
                            new BookKey {Value = 1}, 
                            new BookKey {Value = 2},
                            new BookKey {Value = 3}
                        };
                }, "servicehost");

            stub.Start();

            stub.Stop();
        }
    }
}
