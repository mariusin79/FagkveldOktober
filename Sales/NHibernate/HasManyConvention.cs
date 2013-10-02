using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Sales.NHibernate
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();
        }
    }
}