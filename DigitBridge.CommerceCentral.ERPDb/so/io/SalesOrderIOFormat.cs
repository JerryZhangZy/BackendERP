              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.YoPoco;
using CsvHelper;
using System.IO;
using DigitBridge.Base.Utility;
using System.Dynamic;
using System.Linq;
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a SalesOrderIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class SalesOrderIOFormat : CsvFormat
    {
        public SalesOrderIOFormat() : base()
        {
            InitConfig();
			InitSalesOrderHeader();
			InitSalesOrderHeaderInfo();
			InitSalesOrderHeaderAttributes();
			InitSalesOrderItems();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "SalesOrder Deafult Format";
            KeyName = "";
			DefaultKeyName = "SalesOrderUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitSalesOrderHeader()
		{
			var obj = this.InitParentObject<SalesOrderHeaderDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("SalesOrderUuid", "", idx++, null, false),
				new CsvFormatColumn("OrderNumber", "", idx++, null, false),
				new CsvFormatColumn("OrderType", "", idx++, null, false),
				new CsvFormatColumn("OrderStatus", "", idx++, null, false),
				new CsvFormatColumn("OrderDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("OrderTime", "", idx++, FormatType.Time, false),
				new CsvFormatColumn("ShipDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("DueDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("BillDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EtaArrivalDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EarliestShipDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("LatestShipDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("SignatureFlag", "", idx++, null, false),
				new CsvFormatColumn("CustomerUuid", "", idx++, null, false),
				new CsvFormatColumn("CustomerCode", "", idx++, null, false),
				new CsvFormatColumn("CustomerName", "", idx++, null, false),
				new CsvFormatColumn("Terms", "", idx++, null, false),
				new CsvFormatColumn("TermsDays", "", idx++, null, false),
				new CsvFormatColumn("Currency", "", idx++, null, false),
				new CsvFormatColumn("SubTotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("SalesAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TaxableAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("NonTaxableAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TaxRate", "", idx++, FormatType.TaxRate, false),
				new CsvFormatColumn("TaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("DiscountRate", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("DiscountAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ShippingAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ShippingTaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("MiscAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("MiscTaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ChargeAndAllowanceAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ChannelAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("PaidAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("CreditAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("Balance", "", idx++, null, false),
				new CsvFormatColumn("UnitCost", "", idx++, FormatType.Cost, false),
				new CsvFormatColumn("AvgCost", "", idx++, FormatType.Cost, false),
				new CsvFormatColumn("LotCost", "", idx++, FormatType.Cost, false),
				new CsvFormatColumn("OrderSourceCode", "", idx++, null, false),
				new CsvFormatColumn("DepositAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("MiscInvoiceUuid", "", idx++, null, false),
				new CsvFormatColumn("SalesRep", "", idx++, null, false),
				new CsvFormatColumn("SalesRep2", "", idx++, null, false),
				new CsvFormatColumn("SalesRep3", "", idx++, null, false),
				new CsvFormatColumn("SalesRep4", "", idx++, null, false),
				new CsvFormatColumn("CommissionRate", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("CommissionRate2", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("CommissionRate3", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("CommissionRate4", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("CommissionAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("CommissionAmount2", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("CommissionAmount3", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("CommissionAmount4", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("UpdateDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EnterBy", "", idx++, null, false),
				new CsvFormatColumn("UpdateBy", "", idx++, null, false),
			};
		}



    
		protected virtual void InitSalesOrderHeaderInfo()
		{
			var obj = this.InitParentObject<SalesOrderHeaderInfoDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("CentralFulfillmentNum", "", idx++, null, false),
				new CsvFormatColumn("ShippingCarrier", "", idx++, null, false),
				new CsvFormatColumn("ShippingClass", "", idx++, null, false),
				new CsvFormatColumn("DistributionCenterNum", "", idx++, null, false),
				new CsvFormatColumn("CentralOrderNum", "", idx++, null, false),
				new CsvFormatColumn("CentralOrderUuid", "", idx++, null, false),
				new CsvFormatColumn("ChannelNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelOrderID", "", idx++, null, false),
				new CsvFormatColumn("SecondaryChannelOrderID", "", idx++, null, false),
				new CsvFormatColumn("ShippingAccount", "", idx++, null, false),
				new CsvFormatColumn("RefNum", "", idx++, null, false),
				new CsvFormatColumn("CustomerPoNum", "", idx++, null, false),
				new CsvFormatColumn("EndBuyerUserID", "", idx++, null, false),
				new CsvFormatColumn("EndBuyerName", "", idx++, null, false),
				new CsvFormatColumn("EndBuyerEmail", "", idx++, null, false),
				new CsvFormatColumn("ShipToName", "", idx++, null, false),
				new CsvFormatColumn("ShipToFirstName", "", idx++, null, false),
				new CsvFormatColumn("ShipToLastName", "", idx++, null, false),
				new CsvFormatColumn("ShipToSuffix", "", idx++, null, false),
				new CsvFormatColumn("ShipToCompany", "", idx++, null, false),
				new CsvFormatColumn("ShipToCompanyJobTitle", "", idx++, null, false),
				new CsvFormatColumn("ShipToAttention", "", idx++, null, false),
				new CsvFormatColumn("ShipToAddressLine1", "", idx++, null, false),
				new CsvFormatColumn("ShipToAddressLine2", "", idx++, null, false),
				new CsvFormatColumn("ShipToAddressLine3", "", idx++, null, false),
				new CsvFormatColumn("ShipToCity", "", idx++, null, false),
				new CsvFormatColumn("ShipToState", "", idx++, null, false),
				new CsvFormatColumn("ShipToStateFullName", "", idx++, null, false),
				new CsvFormatColumn("ShipToPostalCode", "", idx++, null, false),
				new CsvFormatColumn("ShipToPostalCodeExt", "", idx++, null, false),
				new CsvFormatColumn("ShipToCounty", "", idx++, null, false),
				new CsvFormatColumn("ShipToCountry", "", idx++, null, false),
				new CsvFormatColumn("ShipToEmail", "", idx++, null, false),
				new CsvFormatColumn("ShipToDaytimePhone", "", idx++, null, false),
				new CsvFormatColumn("ShipToNightPhone", "", idx++, null, false),
				new CsvFormatColumn("BillToName", "", idx++, null, false),
				new CsvFormatColumn("BillToFirstName", "", idx++, null, false),
				new CsvFormatColumn("BillToLastName", "", idx++, null, false),
				new CsvFormatColumn("BillToSuffix", "", idx++, null, false),
				new CsvFormatColumn("BillToCompany", "", idx++, null, false),
				new CsvFormatColumn("BillToCompanyJobTitle", "", idx++, null, false),
				new CsvFormatColumn("BillToAttention", "", idx++, null, false),
				new CsvFormatColumn("BillToAddressLine1", "", idx++, null, false),
				new CsvFormatColumn("BillToAddressLine2", "", idx++, null, false),
				new CsvFormatColumn("BillToAddressLine3", "", idx++, null, false),
				new CsvFormatColumn("BillToCity", "", idx++, null, false),
				new CsvFormatColumn("BillToState", "", idx++, null, false),
				new CsvFormatColumn("BillToStateFullName", "", idx++, null, false),
				new CsvFormatColumn("BillToPostalCode", "", idx++, null, false),
				new CsvFormatColumn("BillToPostalCodeExt", "", idx++, null, false),
				new CsvFormatColumn("BillToCounty", "", idx++, null, false),
				new CsvFormatColumn("BillToCountry", "", idx++, null, false),
				new CsvFormatColumn("BillToEmail", "", idx++, null, false),
				new CsvFormatColumn("BillToDaytimePhone", "", idx++, null, false),
				new CsvFormatColumn("BillToNightPhone", "", idx++, null, false),
				new CsvFormatColumn("Notes", "", idx++, null, false),
				new CsvFormatColumn("OrderDCAssignmentNum", "", idx++, null, false),
				new CsvFormatColumn("DBChannelOrderHeaderRowID", "", idx++, null, false),
			};
		}



    
		protected virtual void InitSalesOrderHeaderAttributes()
		{
			var obj = this.InitParentObject<SalesOrderHeaderAttributesDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("JsonFields", "", idx++, null, false),
			};
		}



    
		protected virtual void InitSalesOrderItems()
		{
			var obj = this.InitParentObject<SalesOrderItemsDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("SalesOrderItemsUuid", "", idx++, null, false),
				new CsvFormatColumn("Seq", "", idx++, null, false),
				new CsvFormatColumn("OrderItemType", "", idx++, null, false),
				new CsvFormatColumn("SalesOrderItemstatus", "", idx++, null, false),
				new CsvFormatColumn("ItemDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("ItemTime", "", idx++, FormatType.Time, false),
				new CsvFormatColumn("SKU", "", idx++, null, false),
				new CsvFormatColumn("ProductUuid", "", idx++, null, false),
				new CsvFormatColumn("InventoryUuid", "", idx++, null, false),
				new CsvFormatColumn("WarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("WarehouseCode", "", idx++, null, false),
				new CsvFormatColumn("LotNum", "", idx++, null, false),
				new CsvFormatColumn("Description", "", idx++, null, false),
				new CsvFormatColumn("UOM", "", idx++, null, false),
				new CsvFormatColumn("PackType", "", idx++, null, false),
				new CsvFormatColumn("PackQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("OrderPack", "", idx++, null, false),
				new CsvFormatColumn("ShipPack", "", idx++, null, false),
				new CsvFormatColumn("CancelledPack", "", idx++, null, false),
				new CsvFormatColumn("OpenPack", "", idx++, null, false),
				new CsvFormatColumn("OrderQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("ShipQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("CancelledQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("OpenQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("PriceRule", "", idx++, null, false),
				new CsvFormatColumn("Price", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("DiscountPrice", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("ExtAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ItemTotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ShipAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("CancelledAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("OpenAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("Stockable", "", idx++, null, false),
				new CsvFormatColumn("IsAr", "", idx++, null, false),
				new CsvFormatColumn("Taxable", "", idx++, null, false),
				new CsvFormatColumn("Costable", "", idx++, null, false),
				new CsvFormatColumn("IsProfit", "", idx++, null, false),
				new CsvFormatColumn("LotInDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("LotExpDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("CentralOrderLineUuid", "", idx++, null, false),
				new CsvFormatColumn("DBChannelOrderLineRowID", "", idx++, null, false),
				new CsvFormatColumn("OrderDCAssignmentLineUuid", "", idx++, null, false),
				new CsvFormatColumn("OrderDCAssignmentLineNum", "", idx++, null, false),
				//new CsvFormatColumn("ShippingAmount","", idx++, null, false),
				//new CsvFormatColumn("Currency", "", idx++, null, false),
			};
		}



    }
}



