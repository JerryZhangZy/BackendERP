using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    [Serializable]
    public class OrderLineModel
    {
        public string SalesOrderItemsUuid { get; set; }
        public string OriginalLineId { get; set; }
        public int CentralDatabaseNum { get; set; }
        public int CentralOrderLineNum { get; set; }
        public long CentralProductNum { get; set; }
        public string ChannelItemID { get; set; }
        public string MerchantSKU { get; set; }
        [Required]
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string ItemTitle { get; set; }
        public int? OrderQty { get; set; }
        public int ShipQty { get; set; }
        public int CancelQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LineItemTaxAmount { get; set; }
        public decimal? LineShippingAmount { get; set; }
        public decimal? LineShippingTaxAmount { get; set; }
        public decimal? LineShippingDiscount { get; set; }
        public decimal? LineShippingDiscountTaxAmount { get; set; }
        public decimal? LineRecyclingFee { get; set; }
        public string LineGiftMsg { get; set; }
        public string LineGiftNotes { get; set; }
        public decimal? LineGiftAmount { get; set; }
        public decimal? LineGiftTaxAmount { get; set; }
        public string LinePromotionCodes { get; set; }
        public decimal? LinePromotionAmount { get; set; }
        public decimal? LinePromotionTaxAmount { get; set; }
        public short BundleType { get; set; }
        public long BundleItemFulfilmentLineNum { get; set; }
        public DateTime EnterDate { get; set; }

        public string MerchantColorCode { get; set; }
        public string MerchantSizeCode { get; set; }
        public string LineUPC { get; set; }
        public string LineEAN { get; set; }
        public string ReferenceLine1 { get; set; }
        public string ReferenceLine2 { get; set; }
        public string ReferenceLine3 { get; set; }
        public decimal? Length { get; set; }
    }
}
