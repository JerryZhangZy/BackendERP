using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Db.Infrastructure
{
    public class QboSalesOrder
    {
		public long SalesOrderNum { get; set; }
		public int DatabaseNum { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		public int　ChannelNum { get; set; }
		public string ChannelName { get; set; }
		public int　ChannelAccountNum　{ get; set; }
		public string ChannelAccountName　{ get; set; }
		public long QboRefId { get; set; }
		public string ChannelOrderId { get; set; }
		public string SecondaryChannelOrderId { get; set; }
		public string EndCustomerPoNum { get; set; }
		public string DigitbridgeOrderId { get; set; }
		public long CentralOrderNum { get; set; }
		public byte SaleOrderQboType { get; set; }
		//public int CentralSyncStatus { get; set; }
		public int QboSyncStatus { get; set; }
		public long CustomerRef { get; set; }
		public string SyncToken { get; set; }
		public int CurrencyRef { get; set; }
		public string DocNumber { get; set; } // UNIQUE In Qbo 
		public string BillEmail { get; set; }
		public string BillEmailCc { get; set; }
		public string BillEmailBcc { get; set; }
		public DateTime TxnDate { get; set; }
		public byte FreeFormAddress { get; set; }
		public string ShipToAddrLine1 { get; set; }
		public string ShipToAddrLine2 { get; set; }
		public string ShipToAddrLine3 { get; set; }
		public string ShipToAddrLine4 { get; set; }
		public string ShipToAddrLine5 { get; set; }
		public string ShipToCity { get; set; }
		public string ShipToCountry { get; set; }
		public string ShipToState { get; set; }
		public string ShipToPostCode { get; set; }
		public string ShipToCompanyName { get; set; }
		public string ShipToName { get; set; }
		public string BillToAddrLine1 { get; set; }
		public string BillToAddrLine2 { get; set; }
		public string BillToAddrLine3 { get; set; }
		public string BillToAddrLine4 { get; set; }
		public string BillToAddrLine5 { get; set; }
		public string BillToCity { get; set; }
		public string BillToCountry { get; set; }
		public string BillToState { get; set; }
		public string BillToPostCode { get; set; }
		public string BillToCompanyName { get; set; }
		public string BillToName { get; set; }
		public DateTime ShipDate { get; set; }
		public int PaymentStatus { get; set; }
		public string TrackingNum { get; set; }
		public int ClassRef { get; set; }
		public string PrintStatus { get; set; }
		public string PaymentRefNum { get; set; }
		public string TxnSource { get; set; }
		public byte ApplyTaxAfterDiscount { get; set; }
		public byte AllowOnlineACHPayment { get; set; }
		public DateTime DueDate { get; set; }
		public string PrivateNote { get; set; }
		public string CustomerMemo { get; set; }
		public int DepositToAccountRef { get; set; }
		public string EmailStatus { get; set; }
		public decimal ExchangeRate { get; set; }
		public decimal Deposit { get; set; }
		public byte AllowOnlineCreditCardPayment { get; set; }
		public int DepartmentRef { get; set; }
		public string ShipMethodRef { get; set; }
		public decimal HomeBalance { get; set; }
		public decimal Balance { get; set; }
		public decimal HomeTotalAmt { get; set; }
		public decimal TotalAmt { get; set; }
		public decimal TotalShippingAmount { get; set; }
		public decimal TotalShippingDiscount { get; set; }
		public decimal PromotionAmount { get; set; }
		public decimal TotalTaxAmount { get; set; }
		public DateTime EmailDeliveryTime { get; set; }
		public string InvoiceLink { get; set; }
		public int TaxExemptionRef { get; set; }
		public int PaymentMethodRef { get; set; }
		public DateTime CentralCreateTime { get; set; }
		public DateTime CentralUpdatedTime { get; set; }
		//public DateTime EnterDate { get; set; }
		public DateTime LastUpdate { get; set; }
        
    }
}
