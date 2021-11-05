using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.ar.CommerceCentralPayload
{
    public class OutputCentralOrderInvoiceHeaderType
    {
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
}
