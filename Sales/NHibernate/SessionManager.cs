namespace Sales.NHibernate
{
    public class SessionManager : ISessionManager
    {
        private readonly IInternalSessionFactory _manager;

        public SessionManager(IInternalSessionFactory manager)
        {
            _manager = manager;
        }

        public SessionUsage OpenSession()
        {
            return new SessionUsage(_manager);
        }

    }
}