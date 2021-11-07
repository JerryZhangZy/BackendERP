using DigitBridge.Base.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class FaultInvoiceRequest
    {
        /// <summary>
        /// InvoiceUuid
        /// </summary>
        public string InvoiceUuid { get; set; }

        /// <summary>
        /// Fault message.
        /// </summary>
        public StringBuilder Message { get; set; }
    }
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class FaultInvoiceResponse : ResponsePayloadBase
    {
        /// <summary>
        /// InvoiceUuid
        /// </summary>
        public string InvoiceUuid { get; set; }
    }
}
