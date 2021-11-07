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
    ///  Request payload object
    /// </summary>
    [Serializable()]
    public class ChannelInvoiceRequestPayload : RequestPayloadBase
    {
        // Add more parameters here
    }

    /// <summary>
    ///  Response payload object
    /// </summary>
    [Serializable()]
    public class ChannelInvoiceResponsePayload : ChannelInvoiceRequestPayload, IResponsePayloadBase
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>   
        //[JsonConverter(typeof(StringBuilderConverter))]
        public IList<OutputCentralOrderInvoiceType> InvoiceUnprocessList { get; set; }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int InvoiceUnprocessListCount { get; set; }

        #endregion list service

        #region response base

        /// <summary>
        /// Request success
        /// </summary>
        [Display(Name = "success")]
        [DataMember(Name = "success")]
        public bool Success { get; set; }

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

