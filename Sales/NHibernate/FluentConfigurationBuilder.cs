using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Sales.NHibernate
{
    public class FluentConfigurationBuilder
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var autoMappingConfiguration = new OurDefaultAutoMappingConfiguration();

            AutoPersistenceModel modelMapping = AutoMap.AssemblyOf<PlaceAnOrder.CustomerSales>(autoMappingConfiguration);
            //modelMapping.AddTypeSource(new TypesFromSharedContractsToAutoMap());

            modelMapping
                .Conventions.AddAssembly(Assembly.GetExecutingAssembly())
                .UseOverridesFromAssembly(Assembly.GetExecutingAssembly());

            var mc = new MappingConfiguration();
            mc.AutoMappings.Add(modelMapping);

            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("SalesDb")))
                .Mappings(m => m.AutoMappings.Add(modelMapping))
                .BuildSessionFactory();

            return sessionFactory;
        }

    }
}