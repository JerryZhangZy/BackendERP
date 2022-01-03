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
                DateFrom = new DateTime(DateTime.UtcNow.Date.Year, 1, 1),
                DateTo = DateTime.UtcNow.Date
            };
        }
    }

    public class SummaryInquiryInfoDetail
    {
        public int SalesOrderCount { get; set; }

        public decimal SalesOrderAmount { get; set; }

        public int OpenSalesOrderCount { get; set; }

        public decimal OpenSalesOrderAmount { get; set; }

        public int CancelSalesOrderCount { get; set; }

        public decimal CancelSalesOrderAmount { get; set; }

        public int ShipmentCount { get; set; }

        public decimal ShipmentAmount { get; set; }

        public int InvoiceCount { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int MiscInvoiceCount { get; set; }

        public decimal MiscInvoiceAmount { get; set; }

        public int InvoicePaymentCount { get; set; }

        public decimal InvoicePaymentAmount { get; set; }

        public int InvoiceReturnCount { get; set; }

        public decimal InvoiceReturnAmount { get; set; }

        public int PoCount { get; set; }

        public decimal PoAmount { get; set; }
        
        public int OpenPoCount { get; set; }

        public decimal OpenPoAmount { get; set; }
        
        public int PoReceiveCount { get; set; }

        public decimal PoReceiveAmount { get; set; }

        public int CustomerCount { get; set; }

        public int NewCustomerCount { get; set; }

        public int NonSalesCustomerCount { get; set; }

        public int ProductCount { get; set; }

        public decimal OrderProductAmount { get; set; }

        public int OrderProductCount { get; set; }

        public decimal SoldProductAmount { get; set; }

        public int SoldProductCount { get; set; }

        public int NonSalesProductCount { get; set; }

        public int InventoryLogCount { get; set; }
        public int TotalInQty { get; set; }
        public int TotalOutQty { get; set; }
        public int InventoryLogCountOfInvoice { get; set; }
        public int InventoryLogCountOfInvoiceReturn { get; set; }
        public int InventoryLogCountOfShipment { get; set; }
        public int InventoryLogCountOfAdjust { get; set; }
        public int InventoryLogCountOfDamage { get; set; }
        public int InventoryLogCountOfCount { get; set; }
        public int InventoryLogCountOfToWarehouse { get; set; }
        public int InventoryLogCountOfFromWarehouse { get; set; }
        public int InventoryLogCountOfAssemble { get; set; }
        public int InventoryLogCountOfDisassemble { get; set; }
        public int InventoryLogCountOfPoReceive { get; set; }
        public int InventoryLogCountOfPoReturn { get; set; }
    }
}
