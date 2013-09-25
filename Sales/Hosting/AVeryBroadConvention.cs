using System.Configuration;
using Castle.Components.DictionaryAdapter;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sales.ReadModel;
using Utilities;

namespace Sales.Hosting
{
    public class AVeryBroadConvention : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IConnectionStrings>()
                         .UsingFactoryMethod<IConnectionStrings>(
                             () =>
                             new DictionaryAdapterFactory().GetAdapter<IConnectionStrings>(new ConnectionStringsWrapper(ConfigurationManager.ConnectionStrings))));

            container.Register(
                Classes.FromThisAssembly().Where(type => type.GetInterfaces().Length > 0).WithService.AllInterfaces());
        }
    }
}