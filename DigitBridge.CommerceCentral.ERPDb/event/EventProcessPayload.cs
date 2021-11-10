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
    public class EventProcessRequestPayload
    {
        /// <summary>
        /// EventUuid
        /// </summary>
        public string EventUuid { get; set; }

        /// <summary>
        /// Handle result,Default is false.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Fault message.
        /// </summary>
        public StringBuilder Message { get; set; }

    }
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class EventProcessResponsePayload : ResponsePayloadBase
    {
        //put more response here.
    }
}
