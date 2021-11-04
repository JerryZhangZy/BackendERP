using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    [Serializable]
    public class AddOrderHeaderModel
    {
        [Required]
        public string WarehouseCode { get; set; }
        public int WarehouseNum { get; set; }
        public int CentralDatabaseNum { get; set; }
        public long CentralOrderNum { get; set; }
        public int ChannelNum { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ChannelAccountNum { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ChannelAccountName { get; set; }
        [Required]
        public string ChannelOrderId { get; set; }
        public string SecondaryChannelOrderId { get; set; }
        public string SellerOrderId { get; set; }
        public string Currency { get; set; }
        public DateTime OriginalOrderDate { get; set; }
        public string SellerPublicNotes { get; set; }
        public string SellerPrivateNotes { get; set; }
        public string EndBuyerInstruction { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalShippingAmount { get; set; }
        public decimal TotalShippingTaxAmount { get; set; }
        public decimal TotalShippingDiscount { get; set; }
        public decimal TotalShippingDiscountTaxAmount { get; set; }
        public decimal TotalInsuranceAmount { get; set; }
        public decimal TotalGiftOptionAmount { get; set; }
        public decimal TotalGiftOptionTaxAmount { get; set; }
        public decimal AdditionalCostOrDiscount { get; set; }
        public decimal PromotionAmount { get; set; }
        public DateTime EstimatedShipDate { get; set; }
        public DateTime EarliestShipDate { get; set; }
        public DateTime LatestShipDate { get; set; }
        public DateTime DeliverByDate { get; set; }
        public string RequestShippingCarrier { get; set; }
        public string RequestShippingService { get; set; }
        public string MappedShippingCarrier { get; set; }
        public string MappedShippingService { get; set; }
        public string ShipToName { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToSuffix { get; set; }
        public string ShipToCompany { get; set; }
        public string ShipToCompanyJobTitle { get; set; }
        public string ShipToAttention { get; set; }
        public string ShipToDaytimePhone { get; set; }
        public string ShipToNightPhone { get; set; }
        public string ShipToAddressLine1 { get; set; }
        public string ShipToAddressLine2 { get; set; }
        public string ShipToAddressLine3 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToStateFullName { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToPostalCodeExt { get; set; }
        public string ShipToCounty { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToEmail { get; set; }
        public string BillToName { get; set; }
        public string BillToFirstName { get; set; }
        public string BillToLastName { get; set; }
        public string BillToSuffix { get; set; }
        public string BillToCompany { get; set; }
        public string BillToCompanyJobTitle { get; set; }
        public string BillToAttention { get; set; }
        public string BillToAddressLine1 { get; set; }
        public string BillToAddressLine2 { get; set; }
        public string BillToAddressLine3 { get; set; }
        public string BillToCity { get; set; }
        public string BillToState { get; set; }
        public string BillToStateFullName { get; set; }
        public string BillToPostalCode { get; set; }
        public string BillToPostalCodeExt { get; set; }
        public string BillToCounty { get; set; }
        public string BillToCountry { get; set; }
        public string BillToEmail { get; set; }
        public string BillToDaytimePhone { get; set; }
        public string BillToNightPhone { get; set; }
        public string SignatureFlag { get; set; }
        //public DateTime EnterDate { get; set; }
        public int ShipmentCount { get; set; }
        public List<OrderLineModel> OrderLineList { get; set; }
    }
}
