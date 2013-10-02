using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Sales.NHibernate
{
    public class OurComponentDatabaseNamingConvention : IComponentConvention
    {
        public void Apply(IComponentInstance instance)
        {
            instance.Properties.First().Column(string.Empty);
        }
    }
}