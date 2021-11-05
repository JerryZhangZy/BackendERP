using DigitBridge.Base.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{

    /// <summary>
    ///  Response payload object
    /// </summary>
    [Serializable()]
    public class ResponsePayloadBase : IResponsePayloadBase
    {
        #region response base

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
        public IList<MessageClass> Messages { get; set; } = new List<MessageClass>();
        [JsonIgnore] public virtual bool HasMessages => Messages != null && Messages.Count > 0;
        public bool ShouldSerializeMessages() => HasMessages;

        #endregion

    }
}
