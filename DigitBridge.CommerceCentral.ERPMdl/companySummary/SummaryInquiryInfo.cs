using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{


    public class SummaryInquiryTableEntity
    {
        [JsonIgnore]
        public string PartitionKey => "ErpSummaryCache";

        [JsonIgnore]
        public string RowKey { get; set; }

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string SummaryInfo { get; set; }

        public SummaryInquiryInfoDetail Summary { get; set; }

        public SummaryInquiryFilter Filter { get; set; }

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
        public SummaryInquiryFilter() { }

        public SummaryInquiryFilter(IPayload payload)
        {
            MasterAccountNum = payload.MasterAccountNum;
            ProfileNum = payload.ProfileNum;
        }

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
        public int SalesOrderCount { get; set; }

        public double SalesOrderAmount { get; set; }

        public int OpenSalesOrderCount { get; set; }

        public double OpenSalesOrderAmount { get; set; }

        public int CancelSalesOrderCount { get; set; }

        public double CancelSalesOrderAmount { get; set; }

        public int ShipmentCount { get; set; }

        public double ShipmentAmount { get; set; }

        public int InvoiceCount { get; set; }

        public double InvoiceAmount { get; set; }

        public int InvoicePaymentCount { get; set; }

        public double InvoicePaymentAmount { get; set; }

        public int InvoiceReturnCount { get; set; }

        public double InvoiceReturnAmount { get; set; }

        public int PoCount { get; set; }

        public double PoAmount { get; set; }
        
        public int OpenPoCount { get; set; }

        public double OpenPoAmount { get; set; }
        
        public int PoReceiveCount { get; set; }

        public double PoReceiveAmount { get; set; }

        public int CustomerCount { get; set; }

        public int NewCustomerCount { get; set; }

        public int NonSalesCustomerCount { get; set; }

        public int ProductCount { get; set; }

        public double OrderProductAmount { get; set; }

        public int OrderProductCount { get; set; }

        public double SoldProductAmount { get; set; }

        public int SoldProductCount { get; set; }

        public int NonSalesProductCount { get; set; }
    }
}
