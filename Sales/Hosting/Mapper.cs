using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sales.Automapping;

namespace Sales.Hosting
{
    public class Mapper : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IMapper>().WithService.AllInterfaces());
        }
    }
}