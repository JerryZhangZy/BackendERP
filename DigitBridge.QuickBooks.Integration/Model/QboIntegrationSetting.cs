using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
	public class QboIntegrationSetting
	{
		public long IntegrationSettingNum { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		/// <summary>
		/// 0: All, 1: Shipped
		/// </summary>
		public int ExportByOrderStatus { get; set; }
		/// <summary>
		/// 0: Invoice, 1: Sales Receipt, 2: Daily Summary Sales Receipt, 3: Daily Summary Invoice, 4. Do Not Export Sales Order
		/// </summary>
		public int ExportOrderAs { get; set; }
		/// <summary>
		/// 0: By Date Exported, 1: By Payment Date 
		/// </summary>
		public int ExportOrderDateType { get; set; }
		public DateTime ExportOrderFromDate { get; set; }
		public DateTime ExportOrderToDate { get; set; }
		/// <summary>
		/// 0: By Date Exported, 1: By Shipping Date, 1: By Payment Date
		/// </summary>
		public int DailySummaryOrderDateType { get; set; }
		/// <summary>
		/// 0: Per Marketplace, 1: Per Order
		/// </summary>
		public int QboCustomerCreateRule { get; set; }
		/// <summary>
		/// Item handling option while creating Invoice/Sales Receipt in Qbo when matching item not found
		/// 0: Default Item, 1: Skip Order if is not in QBO, 2. Create New Item if is not in QBO
		/// </summary>
		public int QboItemCreateRule { get; set; }
		/// <summary>
		/// 1: Export To default QboSalesTaxAcc, 0: Do not export sales tax from Digitbridge
		/// </summary>
		public int SalesTaxExportRule { get; set; }
		/// <summary>
		/// Customized Field Name for EndCustomerPoNum in Qbo Invoice/Sells Receipt
		/// </summary>
		public string QboEndCustomerPoNumCustFieldName { get; set; }
		/// <summary>
		/// Customized Field Id for EndCustomerPoNum in Qbo Invoice/Sells Receipt
		/// </summary>
		public int QboEndCustomerPoNumCustFieldId { get; set; }
		/// <summary>
		/// Customized Field Name for Central Channel Order Id in Qbo Invoice/Sells Receipt
		/// </summary>
		public string QboChnlOrderIdCustFieldName { get; set; }
		/// <summary>
		/// Customized Field Id for Central Channel Order Id in Qbo Invoice/Sells Receipt
		/// </summary>
		public int QboChnlOrderIdCustFieldId { get; set; }
		/// <summary>
		///  Customized Field Name for Central Secondary Channel Order Name in Qbo Invoice/Sells Receipt
		/// </summary>
		public string Qbo2ndChnlOrderIdCustFieldName { get; set; }
		/// <summary>
		/// Customized Field Name for Central Secondary Channel Order Id in Qbo Invoice/Sells Receipt 
		/// </summary>
		public int Qbo2ndChnlOrderIdCustFieldId { get; set; }
		/// <summary>
		/// Used when unmatched Item found from Central orders
		/// </summary>
		public string QboDefaultItemName { get; set; }
		public int QboDefaultItemId { get; set; }
		/// <summary>
		/// Non-Inventory Item for Central calculated sales tax line
		/// </summary>
		public string QboSalesTaxItemName { get; set; }
		public int QboSalesTaxItemId { get; set; }
		/// <summary>
		/// Item for Service provided to customer, ex: item handling
		/// </summary>
		public string QboHandlingServiceItemName { get; set; }
		public int QboHandlingServiceItemId { get; set; }
		/// <summary>
		/// Item for daily summary defualt discount item
		/// </summary>
		public string QboDiscountItemName { get; set; }
		public int QboDiscountItemId { get; set; }
		/// <summary>
		/// Item for daily summary defualt shipping cost item
		/// </summary>
		public string QboShippingItemName { get; set; }
		public int QboShippingItemId { get; set; }
		 
		public string QboMiscItemName { get; set; }
		public int QboMiscItemId { get; set; }
		public string QboChargeAndAllowanceItemName { get; set; }
		public int QboChargeAndAllowanceItemId { get; set; }

		

		/// <summary>
		/// Income Account for Handling Service Item
		/// </summary>
		public string QboHandlingServiceAccName { get; set; }
		public int QboHandlingServiceAccId { get; set; }
		/// <summary>
		/// Type: Other Current Liabilities, Detail Type: Sales Tax Payable
		/// </summary>
		public string QboSalesTaxAccName { get; set; }
		public int QboSalesTaxAccId { get; set; }
		public string QboDiscountAccName { get; set; }
		public int QboDiscountAccId { get; set; }
		/// <summary>
		/// For New/Default? Item creation
		/// </summary>
		public string QboItemAssetAccName { get; set; }
		public int QboItemAssetAccId { get; set; }
		/// <summary>
		/// For New/Default? Item creation
		/// </summary>
		public string QboItemExpenseAccName { get; set; }
		public int QboItemExpenseAccId { get; set; }
		/// <summary>
		/// For New/Default? Item creation
		/// </summary>
		public string QboItemIncomeAccName { get; set; }
		public int QboItemIncomeAccId { get; set; }
		/// <summary>
		/// 0: None, 1: Seperate Bill
		/// </summary>
		public int QboPostageRule { get; set; }
		/// <summary>
		/// 0: None, 1: All, 2: Paid, 3: Unpaid
		/// </summary>
		public int QboInvoiceImportRule { get; set; }
		/// <summary>
		/// 0: None, 1: All
		/// </summary>
		public int QboSalesOrderImportRule { get; set; }
		/// <summary>
		///    Uninitialized = 0, Active = 1, Inactive = 100, Error = 255
		/// </summary>
		public int QboSettingStatus { get; set; }
		public DateTime QboImportOrderAfterUpdateDate { get; set; }
		public DateTime EnterDate { get; set; }
		public DateTime LastUpdate { get; set; }
	}
}
