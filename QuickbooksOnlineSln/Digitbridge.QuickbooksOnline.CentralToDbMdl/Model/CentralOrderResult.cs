using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl.Model
{

    public class CentralOrderResult
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; }
    }

    public class Order
    {
        [JsonProperty("orderHeader")]
        public OrderHeader OrderHeader { get; set; }

        [JsonProperty("orderItems")]
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderHeader
    {
        [JsonProperty("digitbridgeOrderId")]
        public string DigitbridgeOrderId { get; set; }

        [JsonProperty("databaseNum")]
        public int DatabaseNum { get; set; }

        [JsonProperty("centralOrderNum")]
        public int CentralOrderNum { get; set; }

        [JsonProperty("masterAccountNum")]
        public int MasterAccountNum { get; set; }

        [JsonProperty("profileNum")]
        public int ProfileNum { get; set; }

        [JsonProperty("channelNum")]
        public int ChannelNum { get; set; }

        [JsonProperty("channelName")]
        public string ChannelName { get; set; }

        [JsonProperty("channelAccountNum")]
        public int ChannelAccountNum { get; set; }

        [JsonProperty("channelAccountName")]
        public string ChannelAccountName { get; set; }

        [JsonProperty("userDataPresent")]
        public int UserDataPresent { get; set; }

        [JsonProperty("userDataRemoveDateUtc")]
        public DateTime UserDataRemoveDateUtc { get; set; }

        [JsonProperty("channelOrderID")]
        public string ChannelOrderID { get; set; }

        [JsonProperty("secondaryChannelOrderID")]
        public string SecondaryChannelOrderID { get; set; }

        [JsonProperty("sellerOrderID")]
        public string SellerOrderID { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("originalOrderDateUtc")]
        public DateTime OriginalOrderDateUtc { get; set; }

        [JsonProperty("sellerPublicNote")]
        public string SellerPublicNote { get; set; }

        [JsonProperty("sellerPrivateNote")]
        public string SellerPrivateNote { get; set; }

        [JsonProperty("endBuyerName")]
        public string EndBuyerName { get; set; }

        [JsonProperty("endBuyerInstruction")]
        public string EndBuyerInstruction { get; set; }

        [JsonProperty("endCustomerPoNum")]
        public string EndCustomerPoNum { get; set; }

        [JsonProperty("totalOrderAmount")]
        public double TotalOrderAmount { get; set; }

        [JsonProperty("totalTaxAmount")]
        public double TotalTaxAmount { get; set; }

        [JsonProperty("totalShippingAmount")]
        public double TotalShippingAmount { get; set; }

        [JsonProperty("totalShippingTaxAmount")]
        public double TotalShippingTaxAmount { get; set; }

        [JsonProperty("totalShippingDiscount")]
        public double TotalShippingDiscount { get; set; }

        [JsonProperty("totalShippingDiscountTaxAmount")]
        public double TotalShippingDiscountTaxAmount { get; set; }

        [JsonProperty("totalInsuranceAmount")]
        public double TotalInsuranceAmount { get; set; }

        [JsonProperty("totalGiftOptionAmount")]
        public double TotalGiftOptionAmount { get; set; }

        [JsonProperty("totalGiftOptionTaxAmount")]
        public double TotalGiftOptionTaxAmount { get; set; }

        [JsonProperty("additionalCostOrDiscount")]
        public double AdditionalCostOrDiscount { get; set; }

        [JsonProperty("promotionAmount")]
        public double PromotionAmount { get; set; }

        [JsonProperty("estimatedShipDateUtc")]
        public DateTime EstimatedShipDateUtc { get; set; }

        [JsonProperty("deliverByDateUtc")]
        public DateTime DeliverByDateUtc { get; set; }

        [JsonProperty("requestedShippingCarrier")]
        public string RequestedShippingCarrier { get; set; }

        [JsonProperty("requestedShippingClass")]
        public string RequestedShippingClass { get; set; }

        [JsonProperty("resellerID")]
        public string ResellerID { get; set; }

        [JsonProperty("flagNum")]
        public int FlagNum { get; set; }

        [JsonProperty("flagDesc")]
        public string FlagDesc { get; set; }

        [JsonProperty("paymentStatus")]
        public int PaymentStatus { get; set; }

        [JsonProperty("paymentStatusName")]
        public string PaymentStatusName { get; set; }

        [JsonProperty("paymentUpdateUtc")]
        public DateTime PaymentUpdateUtc { get; set; }

        [JsonProperty("shippingUpdateUtc")]
        public DateTime ShippingUpdateUtc { get; set; }

        [JsonProperty("endBuyerUserID")]
        public string EndBuyerUserID { get; set; }

        [JsonProperty("endBuyerEmail")]
        public string EndBuyerEmail { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("shipToName")]
        public string ShipToName { get; set; }

        [JsonProperty("shipToFirstName")]
        public string ShipToFirstName { get; set; }

        [JsonProperty("shipToLastName")]
        public string ShipToLastName { get; set; }

        [JsonProperty("shipToSuffix")]
        public string ShipToSuffix { get; set; }

        [JsonProperty("shipToCompany")]
        public string ShipToCompany { get; set; }

        [JsonProperty("shipToCompanyJobTitle")]
        public string ShipToCompanyJobTitle { get; set; }

        [JsonProperty("shipToAttention")]
        public string ShipToAttention { get; set; }

        [JsonProperty("shipToAddressLine1")]
        public string ShipToAddressLine1 { get; set; }

        [JsonProperty("shipToAddressLine2")]
        public string ShipToAddressLine2 { get; set; }

        [JsonProperty("shipToAddressLine3")]
        public string ShipToAddressLine3 { get; set; }

        [JsonProperty("shipToCity")]
        public string ShipToCity { get; set; }

        [JsonProperty("shipToState")]
        public string ShipToState { get; set; }

        [JsonProperty("shipToStateFullName")]
        public string ShipToStateFullName { get; set; }

        [JsonProperty("shipToPostalCode")]
        public string ShipToPostalCode { get; set; }

        [JsonProperty("shipToPostalCodeExt")]
        public string ShipToPostalCodeExt { get; set; }

        [JsonProperty("shipToCountry")]
        public string ShipToCountry { get; set; }

        [JsonProperty("shipToEmail")]
        public string ShipToEmail { get; set; }

        [JsonProperty("shipToDaytimePhone")]
        public string ShipToDaytimePhone { get; set; }

        [JsonProperty("shipToNightPhone")]
        public string ShipToNightPhone { get; set; }
        /// <summary>
        /// To be confirm
        /// </summary>
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }
        /// <summary>
        /// To be confirm
        /// </summary>
        [JsonProperty("shippedDateUtc")]
        public DateTime ShippedDateUtc { get; set; }

        [JsonProperty("billToName")]
        public string BillToName { get; set; }

        [JsonProperty("billToFirstName")]
        public string BillToFirstName { get; set; }

        [JsonProperty("billToLastName")]
        public string BillToLastName { get; set; }

        [JsonProperty("billToSuffix")]
        public string BillToSuffix { get; set; }

        [JsonProperty("billToCompany")]
        public string BillToCompany { get; set; }

        [JsonProperty("billToCompanyJobTitle")]
        public string BillToCompanyJobTitle { get; set; }

        [JsonProperty("billToAttention")]
        public string BillToAttention { get; set; }

        [JsonProperty("billToAddressLine1")]
        public string BillToAddressLine1 { get; set; }

        [JsonProperty("billToAddressLine2")]
        public string BillToAddressLine2 { get; set; }

        [JsonProperty("billToAddressLine3")]
        public string BillToAddressLine3 { get; set; }

        [JsonProperty("billToCity")]
        public string BillToCity { get; set; }

        [JsonProperty("billToState")]
        public string BillToState { get; set; }

        [JsonProperty("billToStateFullName")]
        public string BillToStateFullName { get; set; }

        [JsonProperty("billToPostalCode")]
        public string BillToPostalCode { get; set; }

        [JsonProperty("billToPostalCodeExt")]
        public string BillToPostalCodeExt { get; set; }

        [JsonProperty("billToCountry")]
        public string BillToCountry { get; set; }

        [JsonProperty("billToEmail")]
        public string BillToEmail { get; set; }

        [JsonProperty("billToDaytimePhone")]
        public string BillToDaytimePhone { get; set; }

        [JsonProperty("billToNightPhone")]
        public string BillToNightPhone { get; set; }

        [JsonProperty("signatureFlag")]
        public string SignatureFlag { get; set; }

        [JsonProperty("pickupFlag")]
        public string PickupFlag { get; set; }

        [JsonProperty("merchantDivision")]
        public string MerchantDivision { get; set; }

        [JsonProperty("merchantBatchNumber")]
        public string MerchantBatchNumber { get; set; }

        [JsonProperty("merchantDepartmentSiteID")]
        public string MerchantDepartmentSiteID { get; set; }

        [JsonProperty("reservationNumber")]
        public string ReservationNumber { get; set; }

        [JsonProperty("merchantShipToAddressType")]
        public string MerchantShipToAddressType { get; set; }

        [JsonProperty("giftIndicator")]
        public string GiftIndicator { get; set; }

        [JsonProperty("customerOrganizationType")]
        public int CustomerOrganizationType { get; set; }

        [JsonProperty("customerOrganizationTypeName")]
        public string CustomerOrganizationTypeName { get; set; }

        [JsonProperty("orderMark")]
        public int OrderMark { get; set; }

        [JsonProperty("orderMarkName")]
        public string OrderMarkName { get; set; }

        [JsonProperty("orderMark2")]
        public int OrderMark2 { get; set; }

        [JsonProperty("orderMark2Name")]
        public string OrderMark2Name { get; set; }

        [JsonProperty("orderStatus")]
        public int OrderStatus { get; set; }

        [JsonProperty("orderStatusName")]
        public string OrderStatusName { get; set; }

        [JsonProperty("orderStatusUpdateDateUtc")]
        public DateTime OrderStatusUpdateDateUtc { get; set; }

        [JsonProperty("notFulfill")]
        public int NotFulfill { get; set; }

        [JsonProperty("integratedByQuickBook")]
        public int IntegratedByQuickBook { get; set; }

        [JsonProperty("enterDateUtc")]
        public DateTime EnterDateUtc { get; set; }
    }

    public class OrderItem
    {
        [JsonProperty("digitbridgeOrderId")]
        public string DigitbridgeOrderId { get; set; }

        [JsonProperty("centralOrderLineNum")]
        public int CentralOrderLineNum { get; set; }

        [JsonProperty("centralProductNum")]
        public int CentralProductNum { get; set; }

        [JsonProperty("channelItemID")]
        public string ChannelItemID { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("itemTitle")]
        public string ItemTitle { get; set; }

        [JsonProperty("orderQty")]
        public int OrderQty { get; set; }

        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty("lineItemTaxAmount")]
        public double LineItemTaxAmount { get; set; }

        [JsonProperty("lineShippingAmount")]
        public double LineShippingAmount { get; set; }

        [JsonProperty("lineShippingTaxAmount")]
        public double LineShippingTaxAmount { get; set; }

        [JsonProperty("lineShippingDiscount")]
        public double LineShippingDiscount { get; set; }

        [JsonProperty("lineShippingDiscountTaxAmount")]
        public double LineShippingDiscountTaxAmount { get; set; }

        [JsonProperty("lineRecyclingFee")]
        public double LineRecyclingFee { get; set; }

        [JsonProperty("lineGiftMsg")]
        public string LineGiftMsg { get; set; }

        [JsonProperty("lineGiftNotes")]
        public string LineGiftNotes { get; set; }

        [JsonProperty("lineGiftAmount")]
        public double LineGiftAmount { get; set; }

        [JsonProperty("lineGiftTaxAmount")]
        public double LineGiftTaxAmount { get; set; }

        [JsonProperty("linePromotionCodes")]
        public string LinePromotionCodes { get; set; }

        [JsonProperty("linePromotionAmount")]
        public double LinePromotionAmount { get; set; }

        [JsonProperty("linePromotionTaxAmount")]
        public double LinePromotionTaxAmount { get; set; }

        [JsonProperty("itemTotalAmount")]
        public double ItemTotalAmount { get; set; }

        [JsonProperty("bundleStatus")]
        public int BundleStatus { get; set; }

        [JsonProperty("harmonizedCode")]
        public string HarmonizedCode { get; set; }

        [JsonProperty("upc")]
        public string Upc { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("enterDateUtc")]
        public DateTime EnterDateUtc { get; set; }
    }

}
