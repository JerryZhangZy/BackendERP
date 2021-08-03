using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.Base.Utility
{
    public interface IMessage
    {
        IList<MessageClass> Messages { get; set; }
        IList<MessageClass> AddInfo(string message, string code = null);
        IList<MessageClass> AddWarning(string message, string code = null);
        IList<MessageClass> AddError(string message, string code = null);
        IList<MessageClass> AddFatal(string message, string code = null);
        IList<MessageClass> AddDebug(string message, string code = null);
    }
}