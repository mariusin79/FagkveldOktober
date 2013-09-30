using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Marketing.Contracts;
using Marketing.Hosting;
using NServiceBus;

namespace ServiceHost
{
    public class MarketingServiceStarter : IWantToRunWhenBusStartsAndStops
    {
        private ServiceHostBase _wcfEndPoint;

        public MarketingServiceStarter(IWindsorContainer container)
        {
            container.Install(FromAssembly.Containing<Neo4j>());
        }

        public void Start()
        {
            _wcfEndPoint = CreateAndOpenWCFHost(typeof(IMarketingService).AssemblyQualifiedName);
        }

        public void Stop()
        {
            _wcfEndPoint.Close();
        }

        private ServiceHostBase CreateAndOpenWCFHost(string constructorString)
        {
            ServiceHostBase serviceHost = new DefaultServiceHostFactory().CreateServiceHost(constructorString, new Uri[0]);
            ServiceEndpoint httpEndpoint = serviceHost.Description.Endpoints.SingleOrDefault(x => x.Binding is BasicHttpBinding);

            if (!serviceHost.Description.Behaviors.Any(x => x is ServiceMetadataBehavior) && httpEndpoint != null)
            {
                string uriString = httpEndpoint.Address.Uri.ToString();

                var metadataBehavior = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri(uriString)
                };
                serviceHost.Description.Behaviors.Add(metadataBehavior);
            }
            serviceHost.Open();
            return serviceHost;
        }

    }
}