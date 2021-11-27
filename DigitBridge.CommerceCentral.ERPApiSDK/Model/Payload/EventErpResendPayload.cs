using DigitBridge.Base.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{

    /// <summary>
    ///  Response payload object
    /// </summary>
    [Serializable()]
    public class ErpEventResendResponsePayload : ResponsePayloadBase
    {
        #region Resend event

        /// <summary>
        /// request to resend array event uuid
        /// </summary>
        public IList<string> EventUuids { get; set; }

        /// <summary>
        /// array event uuid that Successfully sent.
        /// </summary>
        public IList<string> SentEventUuids { get; set; }

        #endregion
    }
}

