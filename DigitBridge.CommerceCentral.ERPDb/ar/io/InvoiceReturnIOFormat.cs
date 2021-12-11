              
    

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

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransactionIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class InvoiceReturnIOFormat : CsvFormat
    {
        public InvoiceReturnIOFormat() : base()
        {
            InitConfig();
			InitInvoiceTransaction();
			InitInvoiceReturnItems();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "InvoiceTransaction Deafult Format";
            KeyName = "";
			DefaultKeyName = "TransUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitInvoiceTransaction()
		{
			var obj = this.InitParentObject<InvoiceTransactionDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("TransUuid", "", idx++, null, false),
				new CsvFormatColumn("TransNum", "", idx++, null, false),
				new CsvFormatColumn("InvoiceUuid", "", idx++, null, false),
				new CsvFormatColumn("InvoiceNumber", "", idx++, null, false),
				new CsvFormatColumn("PaymentUuid", "", idx++, null, false),
				new CsvFormatColumn("PaymentNumber", "", idx++, null, false),
				new CsvFormatColumn("TransType", "", idx++, null, false),
				new CsvFormatColumn("TransStatus", "", idx++, null, false),
				new CsvFormatColumn("TransDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("TransTime", "", idx++, FormatType.Time, false),
				new CsvFormatColumn("Description", "", idx++, null, false),
				new CsvFormatColumn("Notes", "", idx++, null, false),
				new CsvFormatColumn("PaidBy", "", idx++, null, false),
				new CsvFormatColumn("BankAccountUuid", "", idx++, null, false),
				new CsvFormatColumn("BankAccountCode", "", idx++, null, false),
				new CsvFormatColumn("CheckNum", "", idx++, null, false),
				new CsvFormatColumn("AuthCode", "", idx++, null, false),
				new CsvFormatColumn("Currency", "", idx++, null, false),
				new CsvFormatColumn("ExchangeRate", "", idx++, FormatType.Rate, false),
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
				new CsvFormatColumn("CreditAccount", "", idx++, null, false),
				new CsvFormatColumn("DebitAccount", "", idx++, null, false),
				new CsvFormatColumn("TransSourceCode", "", idx++, null, false),
				new CsvFormatColumn("UpdateDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EnterBy", "", idx++, null, false),
				new CsvFormatColumn("UpdateBy", "", idx++, null, false),
			};
		}



    
		protected virtual void InitInvoiceReturnItems()
		{
			var obj = this.InitParentObject<InvoiceReturnItemsDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("ReturnItemUuid", "", idx++, null, false),
				new CsvFormatColumn("Seq", "", idx++, null, false),
				new CsvFormatColumn("InvoiceItemsUuid", "", idx++, null, false),
				new CsvFormatColumn("ReturnItemType", "", idx++, null, false),
				new CsvFormatColumn("ReturnItemStatus", "", idx++, null, false),
				new CsvFormatColumn("ReturnDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("ReturnTime", "", idx++, FormatType.Time, false),
				new CsvFormatColumn("ReceiveDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("StockDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("SKU", "", idx++, null, false),
				new CsvFormatColumn("ProductUuid", "", idx++, null, false),
				new CsvFormatColumn("InventoryUuid", "", idx++, null, false),
				new CsvFormatColumn("InvoiceWarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("InvoiceWarehouseCode", "", idx++, null, false),
				new CsvFormatColumn("WarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("WarehouseCode", "", idx++, null, false),
				new CsvFormatColumn("LotNum", "", idx++, null, false),
				new CsvFormatColumn("Reason", "", idx++, null, false),
				new CsvFormatColumn("UOM", "", idx++, null, false),
				new CsvFormatColumn("PackType", "", idx++, null, false),
				new CsvFormatColumn("PackQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("ReturnPack", "", idx++, null, false),
				new CsvFormatColumn("ReceivePack", "", idx++, null, false),
				new CsvFormatColumn("StockPack", "", idx++, null, false),
				new CsvFormatColumn("NonStockPack", "", idx++, null, false),
				new CsvFormatColumn("ReturnQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("ReceiveQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("StockQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("NonStockQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("PutBackWarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("PutBackWarehouseCode", "", idx++, null, false),
				new CsvFormatColumn("DamageWarehouseUuid", "", idx++, null, false),
				new CsvFormatColumn("DamageWarehouseCode", "", idx++, null, false),
				new CsvFormatColumn("InvoiceDiscountPrice", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("InvoiceDiscountAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("ReturnDiscountAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("Price", "", idx++, FormatType.Price, false),
				new CsvFormatColumn("ExtAmount", "", idx++, FormatType.Amount, false),
				new CsvFormatColumn("Stockable", "", idx++, null, false),
				new CsvFormatColumn("IsAr", "", idx++, null, false),
				new CsvFormatColumn("Taxable", "", idx++, null, false),
			};
		}



    }
}


