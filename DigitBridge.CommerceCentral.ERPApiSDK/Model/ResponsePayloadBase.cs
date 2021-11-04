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
    [Serializable()]
    public class ResponsePayloadBase
    {
        /// <summary>
        /// Request success
        /// </summary>
        [Display(Name = "success")]
        [DataMember(Name = "success")]
        public bool Success { get; set; } = true;

        /// <summary>
        /// Message list for this request
        /// </summary>
        [Display(Name = "messages")]
        [DataMember(Name = "messages")]
        public IList<MessageClass> Messages { get; set; }

    }
}
