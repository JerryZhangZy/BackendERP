using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{

summary: {
		salesOrderCount: 1000,
		salesOrderAmount: 15000,
		openSalesOrderCount: 800,
		openSalesOrderAmount: 7500,
		cancelSalesOrderCount: 10,
		cancelSalesOrderAmount: 500,
		shipmentCount: 190,
		shipmentAmount: 7000,
		invoiceCount: 190,
		invoiceAmount: 7000,
		invoicePaymentCount: 190,
		invoicePaymentAmount: 7000,
		invoiceReturnCount: 0,
		invoiceReturnAmount: 0,
		poCount: 100,
		poAmount: 10000,
		openPoCount: 80,
		openPoAmount: 8000,
		poReceiveCount: 20,
		poReceiveAmount: 2000,
		customerCount: 1000,
		newCustomerCount: 200,
		nonSalesCustomerCount: 100,
		productCount: 1000,
		orderProductCount: 600,
		orderProductAmount: 15000,
		soldProductCount: 600,
		soldProductAmount: 7500,
		nonSalesProductCount: 400,
	},

    public class SummaryInquiryTableEntity
    {
        [JsonIgnore]
        public string PartitionKey => "ErpSummaryCache";

        [JsonIgnore]
        public string RowKey { get; set; }

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string SummaryInquiryInfo { get; set; }

        public DateTime CreateInquiryTimeUtc { get; set; }
    }
    public class SummaryInquiryInfo
    {
        public SummaryInquiryFilter Filter { get; set; }

        public SummaryInquiryInfoDetail Summary { get; set; }

        [JsonIgnore]
        public string GenerateRowKey => Filter.GenerateFilterKey;
    }

    public class SummaryInquiryFilter
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string Name { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string CustomerCode { get; set; }

        public string SalesCode { get; set; }


        [JsonIgnore]
        public string GenerateFilterKey => $"{MasterAccountNum}-{ProfileNum}-{Name}-{CustomerCode}-{SalesCode}";

        public static SummaryInquiryFilter DefaultFilter(IPayload payload)
        {
            return new SummaryInquiryFilter()
            {
                MasterAccountNum=payload.MasterAccountNum,
                ProfileNum=payload.ProfileNum,
                DateFrom = new DateTime(DateTime.Today.Year, 1, 1),
                DateTo = DateTime.Today
            };
        }
    }

    public class SummaryInquiryInfoDetail
    {
        public SummaryInquiryInfoItem SalesOrder { get; set; }

        public SummaryInquiryInfoItem OpenSalesOrder { get; set; }

        public SummaryInquiryInfoItem CancelSalesOrder { get; set; }

        public SummaryInquiryInfoItem Shipment { get; set; }

        public SummaryInquiryInfoItem Invoice { get; set; }

        public SummaryInquiryInfoItem InvoicePayment { get; set; }

        public SummaryInquiryInfoItem InvoiceReturn { get; set; }

        public SummaryInquiryInfoItem Po { get; set; }

        public SummaryInquiryInfoItem OpenPo { get; set; }

        public SummaryInquiryInfoItem PoReceive { get; set; }

        public SummaryInquiryInfoItem Customer { get; set; }

        public SummaryInquiryInfoItem NewCumstomer { get; set; }

        public SummaryInquiryInfoItem ActiveCumstomer { get; set; }

        public SummaryInquiryInfoItem NonSalesCustomer { get; set; }

        public SummaryInquiryInfoItem Product { get; set; }

        public SummaryInquiryInfoItem OrderProduct { get; set; }

        public SummaryInquiryInfoItem SoldProduct { get; set; }

        public SummaryInquiryInfoItem NonSalesProduct { get; set; }
    }

    public class SummaryInquiryInfoItem
    {
        public int Count { get; set; }

        public double Amount { get; set; }
    }
}
