using BooksRegistry.WCF;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BooksRegistry.Hosting
{
    public class Wcf : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<BooksRegistryService>().InSameNamespaceAs<BooksRegistryService>().WithService.AllInterfaces());
        }
    }
}