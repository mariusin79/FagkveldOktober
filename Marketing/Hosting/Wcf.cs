using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Marketing.WCF;

namespace Marketing.Hosting
{
    public class Wcf : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<MarketingService>()
                .InSameNamespaceAs<MarketingService>().WithService.AllInterfaces());
        }
    }
}