using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class ScopedTransaction : IDisposable
    {
        private readonly IPrincipal _currentPrincipal;

        public                  ScopedTransaction(bool withTx=true) : this(string.Empty, withTx) {}
        public                  ScopedTransaction(string dsn, bool withTx = true) : this(new MsSqlUniversalDBConfig(dsn), withTx) {}
        public ScopedTransaction(MsSqlUniversalDBConfig config, bool withTx = true)
        {
            DbUtility.Begin(config, withTx);
            _currentPrincipal = Thread.CurrentPrincipal;
        }

        public void Commit()
        {
            DbUtility.Commit();
            Thread.CurrentPrincipal = _currentPrincipal;
        }
        public void Abort()
        {
            DbUtility.Abort();
            Thread.CurrentPrincipal = _currentPrincipal;
        }

        void IDisposable.Dispose()
        {
            DbUtility.Abort();
            Thread.CurrentPrincipal = _currentPrincipal;
        }
    }
}