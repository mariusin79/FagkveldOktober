using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using BooksRegistry.Contracts;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NServiceBus;

namespace ServiceHost
{
    public class BooksRegistryServiceStarter : IWantToRunWhenBusStartsAndStops
    {
        private ServiceHostBase _wcfEndPoint;

        public BooksRegistryServiceStarter(IWindsorContainer container)
        {
            container.Install(FromAssembly.Containing<BooksRegistry.Hosting.Wcf>());
        }

        public void Start()
        {
            _wcfEndPoint = CreateAndOpenWCFHost(typeof(IBooksRegistryService).AssemblyQualifiedName);
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