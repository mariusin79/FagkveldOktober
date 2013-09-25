using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Neo4jClient;

namespace Marketing.Hosting
{
    public class Neo4j : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IGraphClient>()
                         .UsingFactoryMethod(() =>
                             {
                                 var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
                                 client.Connect();
                                 return client;
                             }));
        }
    }
}