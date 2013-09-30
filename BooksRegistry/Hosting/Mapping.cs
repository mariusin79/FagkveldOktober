using BooksRegistry.Automapping;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BooksRegistry.Hosting
{
    public class Mapping : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<OurMapper>().InSameNamespaceAs<OurMapper>().WithService.AllInterfaces());
        }
    }
}