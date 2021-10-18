using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl.Model
{
    public class SummaryInquiryTableEntity:SummaryInquiryInfo
    {
        [JsonIgnore]
    public string PartitionKey => "ErpSummaryCache";

    public string RowKey =>string.Empty;
}
    public class SummaryInquiryInfo
    {
        public SummaryInquiryFIlter Filter { get; set; }

        public SummaryInquiryInfoDetail summary { get; set; }
    }

    public class SummaryInquiryFIlter
    {
        public string Name { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string CustomerCode { get; set; }

        public string SalesCode { get; set; }

        public static SummaryInquiryFIlter DefaultFilter()
        {
            return new SummaryInquiryFIlter()
            {
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
