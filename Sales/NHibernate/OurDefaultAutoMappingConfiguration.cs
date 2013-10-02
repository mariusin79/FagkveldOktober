using System;
using System.Runtime.CompilerServices;
using FluentNHibernate.Automapping;
using Sales.PlaceAnOrder;
using SharedContracts;

namespace Sales.NHibernate
{
    public class OurDefaultAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(CustomerSales).Namespace && IsEntityTypeInDomainNamespace(type)
                && !type.IsDefined(typeof(CompilerGeneratedAttribute), false) && !type.IsEnum;
        }

        private static bool IsEntityTypeInDomainNamespace(Type type)
        {
            return type != typeof(HandleMessage);
        }

        public override bool IsComponent(Type type)
        {
            return type == typeof(BookKey) || type == typeof(Money) || type == typeof(CustomerKey);
        }

        public override bool ShouldMap(FluentNHibernate.Member member)
        {
            if (member.IsProperty && !member.CanWrite)
            {
                return false;
            }

            return base.ShouldMap(member);
        }

    }
}