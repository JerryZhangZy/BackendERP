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
    public class WMSShipmentResendResponsePayload : ResponsePayloadBase
    {
        #region Re send central order to erp

        public IList<string> WMSShipmentIDs { get; set; }

        public List<string> SentWMSShipmentIDs { get; set; }

        #endregion
    }
}

