using DigitBridge.Base.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class FaultInvoiceRequestPayload
    {
        /// <summary>
        /// EventUuid
        /// </summary>
        public string EventUuid { get; set; }

        /// <summary>
        /// Fault message.
        /// </summary>
        public StringBuilder Message { get; set; }
    }
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class FaultInvoiceResponsePayload : ResponsePayloadBase
    {
        /// <summary>
        /// EventUuid
        /// </summary>
        public string EventUuid { get; set; }
    }
}
