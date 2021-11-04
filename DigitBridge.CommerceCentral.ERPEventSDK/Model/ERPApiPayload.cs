using DigitBridge.Base.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Model
{
 
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class ERPApiPayload
    {
        public IList<MessageClass> Messages { get; set; } = new List<MessageClass>();

        public bool Success { get; set; } = true;
 
    }
}
