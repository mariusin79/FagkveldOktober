using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sales.WCF;

namespace Sales.Hosting
{
    public class Wcf : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<SalesService>().If(type => type.GetInterfaces().Length > 0).WithService.AllInterfaces());

        }
    }
}