using System;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DigitBridge.Log
{
    internal class Log_Seri : LogBase
    {
        private Log_Seri() : base(SeriLogInstance.GetSeriLogger())
        {
        }


        #region Instance

        private static object _lockObj = new object();
        private static Log_Seri _instance;

        public static Log_Seri Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Log_Seri();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

    }
}
