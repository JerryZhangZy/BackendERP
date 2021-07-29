using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace DigitBridge.Log
{
    internal class Log_Console : LogBase
    { 
        private Log_Console():base(SeriLogInstance.GetConsoleLogger())
        {
             
        }

        #region Instance

        private static object _lockObj = new object();
        private static Log_Console _instance;

        public static Log_Console Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Log_Console();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
    }
}
