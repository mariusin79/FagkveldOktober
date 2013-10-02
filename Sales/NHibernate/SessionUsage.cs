using System;
using NHibernate;

namespace Sales.NHibernate
{
    public sealed class SessionUsage : IDisposable
    {
        public SessionUsage(IInternalSessionFactory internalSessionManager)
        {
            InternalManager = internalSessionManager;
            Session = internalSessionManager.OpenSession();
        }

        public ISession Session { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IInternalSessionFactory InternalManager { get; set; }

        private void Dispose(bool disposing)
        {
            InternalManager.ReleaseSession(Session);

            if (disposing)
            {
                Session = null;
                InternalManager = null;
            }
        }

        ~SessionUsage()
        {
            Dispose(false);
        }
    }
}