using System;
using System.ServiceModel;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NServiceBus;
using Sales.Contracts;

namespace ServiceHost
{
    public class SalesServiceStarter : IWantToRunWhenBusStartsAndStops
    {
        private ServiceHostBase _wcfEndPoint;

        public SalesServiceStarter(IWindsorContainer container)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Install(FromAssembly.Containing<Sales.Hosting.WriteModel>());
        }

        public void Start()
        {
            _wcfEndPoint = CreateAndOpenWCFHost(typeof(ISalesService).AssemblyQualifiedName);
        }

        public void Stop()
        {
            _wcfEndPoint.Close();
        }

        private ServiceHostBase CreateAndOpenWCFHost(string constructorString)
        {
            ServiceHostBase serviceHost = new DefaultServiceHostFactory().CreateServiceHost(constructorString, new Uri[0]);
            serviceHost.Open();
            return serviceHost;
        }

    }
}