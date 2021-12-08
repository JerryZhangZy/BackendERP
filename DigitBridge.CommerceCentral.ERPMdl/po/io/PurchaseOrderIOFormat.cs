              
    

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
using DigitBridge.CommerceCentral.ERPDb;
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a PurchaseOrderIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class PurchaseOrderIOFormat : CsvFormat
    {
        public PurchaseOrderIOFormat() : base()
        {
            InitConfig();
			InitPoHeader();
			InitPoHeaderInfo();
			InitPoHeaderAttributes();
			InitPoItems();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "PurchaseOrder Deafult Format";
            KeyName = "";
			DefaultKeyName = "PoUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitPoHeader()
		{
			var obj = this.InitParentObject<PoHeaderDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("PoUuid", "", idx++, null, false),
				new CsvFormatColumn("PoNum", "", idx++, null, false),
				new CsvFormatColumn("PoType", "", idx++, null, false),
				new CsvFormatColumn("PoStatus", "", idx++, null, false),
				new CsvFormatColumn("PoDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("PoTime", "", idx++, FormatType.Time, false),
				new CsvFormatColumn("EtaShipDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EtaArrivalDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("CancelDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("Terms", "", idx++, null, false),
				new CsvFormatColumn("VendorUuid", "", idx++, null, false),
				new CsvFormatColumn("VendorCode", "", idx++, null, false),
				new CsvFormatColumn("VendorName", "", idx++, null, false),
				new CsvFormatColumn("Currency", "", idx++, null, false),
				new CsvFormatColumn("SubTotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TaxRate", "", idx++, FormatType.TaxRate, false),
				new CsvFormatColumn("TaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("TaxableAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("NonTaxableAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("DiscountRate", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("DiscountAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ShippingAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ShippingTaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("MiscAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("MiscTaxAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ChargeAndAllowanceAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("PoSourceCode", "", idx++, null, false),
				new CsvFormatColumn("UpdateDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EnterBy", "", idx++, null, false),
				new CsvFormatColumn("UpdateBy", "", idx++, null, false),
			};
		}



    
		protected virtual void InitPoHeaderInfo()
		{
			var obj = this.InitParentObject<PoHeaderInfoDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("CentralFulfillmentNum", "", idx++, null, false),
				new CsvFormatColumn("ShippingCarrier", "", idx++, null, false),
				new CsvFormatColumn("ShippingClass", "", idx++, null, false),
				new CsvFormatColumn("DistributionCenterNum", "", idx++, null, false),
				new CsvFormatColumn("CentralOrderNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelOrderID", "", idx++, null, false),
				new CsvFormatColumn("SecondaryChannelOrderID", "", idx++, null, false),
				new CsvFormatColumn("ShippingAccount", "", idx++, null, false),
				new CsvFormatColumn("RefNum", "", idx++, null, false),
				new CsvFormatColumn("CustomerPoNum", "", idx++, null, false),
				new CsvFormatColumn("CustomerUuid", "", idx++, null, false),
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
			};
		}



    
		protected virtual void InitPoHeaderAttributes()
		{
			var obj = this.InitParentObject<PoHeaderAttributesDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("JsonFields", "", idx++, null, false),
			};
		}



    
		protected virtual void InitPoItems()
		{
			var obj = this.InitParentObject<PoItemsDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("PoItemUuid", "", idx++, null, false),
				new CsvFormatColumn("Seq", "", idx++, null, false),
				new CsvFormatColumn("PoItemType", "", idx++, null, false),
				new CsvFormatColumn("PoItemStatus", "", idx++, null, false),
				new CsvFormatColumn("ProductUuid", "", idx++, null, false),
				new CsvFormatColumn("InventoryUuid", "", idx++, null, false),
				new CsvFormatColumn("SKU", "", idx++, null, false),
				new CsvFormatColumn("Description", "", idx++, null, false),
				new CsvFormatColumn("ItemTotalAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("PoQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("QtyForOther", "", idx++, null, false),
				new CsvFormatColumn("ReceivedQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("CancelledQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("PriceRule", "", idx++, null, false),
				new CsvFormatColumn("Price", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("ExtAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("DiscountPrice", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("Stockable", "", idx++, null, false),
				new CsvFormatColumn("Costable", "", idx++, null, false),
				new CsvFormatColumn("Taxable", "", idx++, null, false),
				new CsvFormatColumn("IsAp", "", idx++, null, false),
				new CsvFormatColumn("WarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("WarehouseCode", "", idx++, null, false),
			};
		}



    }
}



