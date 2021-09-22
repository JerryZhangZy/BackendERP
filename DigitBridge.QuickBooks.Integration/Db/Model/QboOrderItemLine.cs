using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Db.Infrastructure
{
    public class QboOrderItemLine
    {
		public long ItemLineNum { get; set; }
		public int DatabaseNum { get; set; }
		public long QboLineId { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		public int ChannelNum { get; set; }
		public string ChannelName { get; set; }
		public int ChannelAccountNum { get; set; }
		public string ChannelAccountName { get; set; }
		public long CentralProductNum { get; set; }
		public string ChannelItemId { get; set; }
		public long CentralOrderLineNum { get; set; }
		public long SalesOrderNum { get; set; }
		public string ChannelOrderId { get; set; }
		public string SecondaryChannelOrderId { get; set; }
		public string DigitbridgeOrderId { get; set; }
		//public int CentralSyncStatus { get; set; }
		public int QboSyncStatus { get; set; }
		public byte DetailType { get; set; }
		public string SyncToken { get; set; }
		public string Description { get; set; }
		public string Sku { get; set; }
		public int LineNum { get; set; }
		public decimal Amount { get; set; }
		public long ItemRef { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public DateTime ServiceDate { get; set; }
		//public DateTime EnterDate { get; set; }
		public DateTime LastUpdate { get; set; }
		public DateTime CentralCreateTime { get; set; }
		public DateTime CentralUpdatedTime { get; set; }
		public int ItemAccountRef { get; set; }
		public int MarkupInfo { get; set; }
		public int TaxCodeRef { get; set; }
		public int ClassRef { get; set; }
	}
}
