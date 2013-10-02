using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Sales.NHibernate;
using Sales.NHibernate.Repositories;

namespace Sales.Hosting
{
    public class WriteModel : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISessionManager>().ImplementedBy<SessionManager>());
            container.Register(Component.For<IInternalSessionFactory>().AsFactory());

            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(FluentConfigurationBuilder.BuildSessionFactory));

            container.Register(Component.For<ISession>().UsingFactoryMethod(kernel =>
                {
                    var factory = kernel.Resolve<ISessionFactory>();
                    var session = factory.OpenSession();
                    kernel.ReleaseComponent(factory);
                    return session;
                }).LifeStyle.Scoped());

            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<CustomerSalesRepository>().If(type => type.Name.EndsWith("Repository")).WithService.AllInterfaces());
        }
    }
}