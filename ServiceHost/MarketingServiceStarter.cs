using Castle.Windsor;
using Castle.Windsor.Installer;
using Marketing.Hosting;
using NServiceBus;

namespace ServiceHost
{
    public class MarketingServiceStarter : IWantToRunWhenBusStartsAndStops
    {
        private readonly IWindsorContainer _container;

        public MarketingServiceStarter(IWindsorContainer container)
        {
            _container = container;
        }

        public void Start()
        {
            _container.Install(FromAssembly.Containing<Neo4j>());
        }

        public void Stop()
        {
        }
    }
}