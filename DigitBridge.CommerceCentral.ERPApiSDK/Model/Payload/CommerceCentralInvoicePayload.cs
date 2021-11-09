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
    public class CommerceCentralInvoiceRequestPayload : FilterPayloadBase<InvoiceUnprocessPayloadFilter>
    {
        // Add more parameters here
    }

    [Serializable()]
    public class InvoiceUnprocessPayloadFilter
    {
        public int? ChannelNum { get; set; }
        public int? ChannelAccountNum { get; set; }

        //public int EventProcessActionStatus { get; set; }
    }

    /// <summary>
    ///  Response payload object
    /// </summary>
    [Serializable()]
    public class CommerceCentralInvoiceResponsePayload : CommerceCentralInvoiceRequestPayload, IResponsePayloadBase
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

    #region Model
    public class OutputCentralOrderInvoiceHeaderType
    {
        public string EventUuid;
        public long OrderInvoiceNum = 0;
        public int DatabaseNum = 0;
        public int MasterAccountNum = 0;
        public int ProfileNum = 0;
        public int ChannelNum = 0;
        public int ChannelAccountNum = 0;
        public string InvoiceNumber = "";
        public string InvoiceDateUtc = "";
        public long CentralOrderNum = 0;
        public string ChannelOrderID = "";
        public long OrderDCAssignmentNum = 0;
        public long OrderShipmentNum = 0;
        public string ShipmentID = "";
        public string ShipmentDateUtc = "";
        public string ShippingCarrier = "";
        public string ShippingClass = "";
        public decimal ShippingCost = 0;
        public string MainTrackingNumber = "";
        public decimal InvoiceAmount = 0;
        public decimal InvoiceTaxAmount = 0;
        public decimal InvoiceHandlingFee = 0;
        public decimal InvoiceDiscountAmount = 0;
        public decimal TotalAmount = 0;
        public string InvoiceTermsType = "";
        public string InvoiceTermsDescrption = "";
        public int InvoiceTermsDays = 0;
        public string DBChannelOrderHeaderRowID = "";
        public DateTime EnterDateUtc = DateTime.UtcNow;
    }

    public class OutputCentralOrderInvoiceItemType
    {
        public long OrderInvoiceLineNum = 0;
        public int DatabaseNum = 0;
        public int MasterAccountNum = 0;
        public int ProfileNum = 0;
        public int ChannelNum = 0;
        public int ChannelAccountNum = 0;
        public long OrderShipmentItemNum = 0;
        public long CentralOrderLineNum = 0;
        public long OrderDCAssignmentLineNum = 0;
        public string SKU = "";
        public string ChannelItemID = "";
        public decimal ShippedQty = 0;
        public decimal UnitPrice = 0;
        public decimal LineItemAmount = 0;
        public decimal LineTaxAmount = 0;
        public decimal LineHandlingFee = 0;
        public decimal LineDiscountAmount = 0;
        public decimal LineAmount = 0;
        public string DBChannelOrderLineRowID = "";
        public string ItemStatus = "";
        public DateTime EnterDateUtc { get; set; }
    }

    public class OutputCentralOrderInvoiceType
    {
        public OutputCentralOrderInvoiceHeaderType InvoiceHeader { get; set; }
        public List<OutputCentralOrderInvoiceItemType> InvoiceItems { get; set; }

    }
    #endregion
}

