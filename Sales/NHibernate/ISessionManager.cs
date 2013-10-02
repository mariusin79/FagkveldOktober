namespace Sales.NHibernate
{
    public interface ISessionManager
    {
        SessionUsage OpenSession();
    }
}