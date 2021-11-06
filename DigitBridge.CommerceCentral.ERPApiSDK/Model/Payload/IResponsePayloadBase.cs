using DigitBridge.Base.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Response Payload Base
    /// </summary> 
    interface IResponsePayloadBase
    {
        ///// <summary>
        ///// Request success
        ///// </summary>
        public bool Success { get; set; }

        ///// <summary>
        ///// Message list for this request
        ///// </summary>
        public IList<MessageClass> Messages { get; set; }
    }
}
