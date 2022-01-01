using System;
using Serilog.Core;
using Serilog.Events;

namespace DigitBridge.Log
{
    internal class Log_Internal : LogBase
    {
        private Log_Internal() : base(SeriLogInstance.GetInternalLogger())
        {
        }

        #region Instance

        private static object _lockObj = new object();
        private static Log_Internal _instance;

        public static Log_Internal Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Log_Internal();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

    }
}
