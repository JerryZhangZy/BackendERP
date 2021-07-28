using DigitBridge.Base.Utility;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class ScopedTransaction : IDisposable
    {
        private readonly IPrincipal _currentPrincipal;
        private readonly IDataBaseFactory _dataBaseFactory;
        private readonly bool _withTx;

        // each sql query must specify dbFactory to excute
        //public ScopedTransaction(bool withTx=true) : this(string.Empty, withTx) {}
        public ScopedTransaction(string dsn, bool withTx = true) : this(new DbConnSetting(dsn), withTx) {}
        public ScopedTransaction(DbConnSetting config, bool withTx = true)
        {
            _dataBaseFactory = DataBaseFactory.CreateDefault(config);
            _withTx = withTx;
            if (_withTx)
                _dataBaseFactory.Begin();
            _currentPrincipal = Thread.CurrentPrincipal;
        }
        public ScopedTransaction(IDataBaseFactory dataBaseFactory, bool withTx = true)
        {
            _dataBaseFactory = DataBaseFactory.SetDefaultDataBaseFactory(dataBaseFactory);
            _withTx = withTx;
            if (_withTx)
                _dataBaseFactory.Begin();
            _currentPrincipal = Thread.CurrentPrincipal;
        }

        public void Commit()
        {
            if (_withTx)
                _dataBaseFactory.Commit();
            Thread.CurrentPrincipal = _currentPrincipal;
        }
        public void Abort()
        {
            if (_withTx)
                _dataBaseFactory.Abort();
            Thread.CurrentPrincipal = _currentPrincipal;
        }

        void IDisposable.Dispose()
        {
            if (_withTx)
                _dataBaseFactory.Abort();
            Thread.CurrentPrincipal = _currentPrincipal;
        }
    }
}