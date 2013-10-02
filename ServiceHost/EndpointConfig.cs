using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace ServiceHost
{
    using NServiceBus;

    /*
        This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
        can be found here: http://particular.net/articles/the-nservicebus-host
    */
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.AddFacility<WcfFacility>();
            container.Kernel.Resolver.AddSubResolver(new ListResolver(container.Kernel));

            Configure.Serialization.Xml();

            Configure.With()
                     .CastleWindsorBuilder(container)
                     .UseTransport<Msmq>()
                     .RavenSubscriptionStorage()
                     .UnicastBus();
        }
    }
}