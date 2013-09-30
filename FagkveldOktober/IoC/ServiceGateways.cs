using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FagkveldOktober.IoC
{
    public class ServiceGateways : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs(typeof(FagkveldOktober.ServiceGateways.ServiceGateway<>)).WithService.AllInterfaces());

            container.Register(Component.For<Func<System.Type, string>>().Instance(t => t.Name.Substring(1).Replace("Service", "")));
        }
    }
}