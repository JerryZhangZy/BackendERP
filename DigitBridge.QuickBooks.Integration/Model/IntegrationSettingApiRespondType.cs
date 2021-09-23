using DigitBridge.QuickBooks.Integration.Db.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class IntegrationSettingApiRespondType
    {
        public QboIntegrationSetting IntegrationSetting { get; set; }
        public List<QboChnlAccSetting> ChnlAccSettings { get; set; }
    }

    public class IntegrationSettingApiReqType
    {
        [JsonProperty("integrationSetting")]
        [Required(ErrorMessage = "Intergration Setting is required but not provided. ")]
        public IntergrationSettingReqType IntegrationSetting { get; set; }

        [JsonProperty("chnlAccSettings")]
        [Required, MinLength(1, ErrorMessage = "ChnlAccSettings is required but not provided. ")]
        public List<ChnlAccSettingReqType> ChnlAccSettings { get; set; }
    }

    public class IntergrationSettingReqType
    {
        [Range(10000, int.MaxValue, ErrorMessage = "Invalid MasterAccountNum. ")]
        public int MasterAccountNum { get; set; }
        [Range(10000, int.MaxValue, ErrorMessage = "Invalid ProfileNum. ")]
        public int ProfileNum { get; set; }
        /// <summary>
        /// 0: Invoice, 1: Sales Receipt, 2: Daily Summary Sales Receipt, 3: Daily Summary Invoice, 4. Do Not Export Sales Order
        /// </summary>
        [Required(ErrorMessage = "ExportOrderAs is required but not provided. ")]
        public int ExportOrderAs { get; set; }
        /// <summary>
        /// 0: By Date Exported, 1: By Payment Date 
        /// </summary>
        [Required(ErrorMessage = "ExportOrderDateType is required but not provided. ")]
        public int ExportOrderDateType { get; set; }
        [Required(ErrorMessage = "ExportOrderFromDate is required but not provided. ")]
        public DateTime ExportOrderFromDate { get; set; }

        public DateTime ExportOrderToDate { get; set; }
        /// <summary>
        /// 0: Per Marketplace, 1: Per Order
        /// </summary>
        [Required(ErrorMessage = "QboCustomerCreateRule is required but not provided. ")]
        public int QboCustomerCreateRule { get; set; }
        /// <summary>
        /// Item handling option while creating Invoice/Sales Receipt in Qbo when matching item not found
        /// 0: Default Item, 1: Skip Order if is not in QBO, 2. Create New Item if is not in QBO
        /// </summary>
        [Required(ErrorMessage = "QboItemCreateRule is required but not provided. ")]
        public int QboItemCreateRule { get; set; }
        /// <summary>
        /// 0: Export To default QboSalesTaxAcc, 1: Do not export sales tax
        /// </summary>
        [Required(ErrorMessage = "SalesTaxExportRule is required but not provided. ")]
        public int SalesTaxExportRule { get; set; }
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
        /// Type: Other Current Liabilities, Detail Type: Sales Tax Payable
        /// </summary>
        public string QboSalesTaxAccName { get; set; }
        public int QboSalesTaxAccId { get; set; }
        /// <summary>
        /// Non-Inventor Item for daily summary defualt discount item
        /// </summary>
        public string QboDiscountItemName { get; set; }
        public int QboDiscountItemId { get; set; }
        /// <summary>
        /// Non-Inventor Item for daily summary defualt shipping cost item
        /// </summary>
        public string QboShippingItemName { get; set; }
        public int QboShippingItemId { get; set; }
        [Required(ErrorMessage = "QboInvoiceImportRule is required but not provided. ")]
        public int QboInvoiceImportRule { get; set; }
        /// <summary>
        /// 0: None, 1: All
        /// </summary>
        [Required(ErrorMessage = "QboSalesOrderImportRule is required but not provided. ")]
        public int QboSalesOrderImportRule { get; set; }
        [Required(ErrorMessage = "QboImportOrderAfterUpdateDate is required but not provided. ")]
        public DateTime QboImportOrderAfterUpdateDate { get; set; }
        //public DateTime LastUpdate { get; set; }
    }

    public class ChnlAccSettingReqType
    {
        [Range(10000, int.MaxValue, ErrorMessage = "MasterAccountNum is required but not provided. ")]
        public int MasterAccountNum { get; set; }
        [Range(10000, int.MaxValue, ErrorMessage = "ProfileNum is required but not provided. ")]
        public int ProfileNum { get; set; }
        /// <summary>
        /// Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
        /// </summary>
        [Required(ErrorMessage = "ChannelAccountName is required but not provided. "),
            StringLength(150, MinimumLength = 1, ErrorMessage = "Invalid length of ChannelAccountName. ")]
        public string ChannelAccountName { get; set; }
        /// <summary>
        /// Central Channel Account Number
        /// </summary>
        [Required(ErrorMessage = "ChannelAccountNum is required but not provided. ")]
        public int ChannelAccountNum { get; set; }
        /// <summary>
        /// Central Channel name
        /// </summary>
        [Required(ErrorMessage = "ChannelName is required but not provided. "),
            StringLength(150, MinimumLength = 1, ErrorMessage = "Invalid length of ChannelName. ")]
        public string ChannelName { get; set; }
        /// <summary>
        /// Central Channel Number
        /// </summary>
        [Required(ErrorMessage = "ChannelNum is required but not provided. ")]
        public int ChannelNum { get; set; }
        /// <summary>
        /// Use if select Create Customer Records per Marketplace
        /// </summary>
        [Required(ErrorMessage = "ChannelQboCustomerName is required but not provided. ")]
        public string ChannelQboCustomerName { get; set; }
        /// <summary>
        /// Use if select Create Customer Records per Marketplace
        /// </summary>
        [Required(ErrorMessage = "ChannelQboCustomerId is required but not provided. ")]
        public int ChannelQboCustomerId { get; set; }
        /// <summary>
        /// Account for fee in this Marketplace
        /// </summary>
        public string ChannelQboFeeAcountName { get; set; }
        /// <summary>
        /// Account for fee in this Marketplace
        /// </summary>
        public int ChannelQboFeeAcountId { get; set; }
        //public DateTime LastUpdate { get; set; }
    }

    public class UserInitialDataApiResponseType
    {
        public List<CustomerPair> Customers { get; set; }
        public List<InventoryItemPair> InventoryItems { get; set; }
        public List<NonInventoryItemPair> NonInventoryItems { get; set; }
        public List<OtherCurrentLiabilitiesAccountPair> OtherCurrentLiabilitiesAccounts { get; set; }
        public UserCompanyInfo UserCompanyInfo { get; set; }
    }
    public class CustomerPair
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class InventoryItemPair
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class NonInventoryItemPair
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class OtherCurrentLiabilitiesAccountPair
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class UserCompanyInfo
    {
        public string CompanyName { get; set; }
    }

}
