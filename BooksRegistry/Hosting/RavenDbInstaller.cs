using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Document;

namespace BooksRegistry.Hosting
{
    public class RavenDbInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod(() =>
                {
                    var connectionFactory = new DocumentStore
                        {
                            ConnectionStringName = "BooksRegistry"
                        };
                    connectionFactory.Initialize();
                    return connectionFactory;
                }));
        }
    }
}