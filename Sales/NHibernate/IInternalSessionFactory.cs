using NHibernate;

namespace Sales.NHibernate
{
    public interface IInternalSessionFactory
    {
        ISession OpenSession();

        void ReleaseSession(ISession session);
    }
}