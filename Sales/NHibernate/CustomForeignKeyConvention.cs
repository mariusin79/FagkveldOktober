using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace Sales.NHibernate
{
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return string.Format("{0}Id", type.Name);

            return string.Format("{0}Id", property.Name);
        }
    }
}