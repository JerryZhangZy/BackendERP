using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public enum QboSettingStatus
    {
        uninitialized = 0,
        Active = 1,
        Inactive = 100,
        Error = 255
    }
    public enum QboOAuthTokenStatus
    {
        uninitialized = 0,
        Success = 1,
        Disconnected = 100,
        Error = 255
    }
    public enum OrderImportStatus
    {
        UnSynced = 0,
        InitialImported = 1,
        TrackingImported = 2,
        SyncedWithError = 255
    }
    public enum QboSyncStatus
    {
        Null = -1,
        UnSynced = 0,
        SyncedSuccess = 1,
        Skipped = 2,
        PendingSummary = 3,
        ToBeUpdated = 4,
        PreprocessStarted = 248,
        UpdateStarted = 249,
        SyncStarted = 250,
        PreprocessedWithError= 253,
        UpdatedWithError = 254,
        SyncedWithError = 255
    }
    public enum CentralOrderStatus
    {
        Processing = 0,
        Shipped = 1,
        PartiallyShipped = 2,
        PendingShipment = 4,
        ReadyToPickup = 8,
        Canceled = 16,
        OnHold = 100
    }
    public enum SalesOrderDetailType
    {
        SalesItemLineDetail = 0,
        GroupLineDetail = 1
    }
    
    public enum OrderExportRule
    {
        Null = -1,
        Invoice = 0,
        SalesReceipt = 1,
        DailySummarySalesReceipt = 2,
        DailySummaryInvoice = 3,
        DoNotExport = 4
    }

    public enum SaleOrderQboType
    {
        Null = -1,
        Invoice = 0,
        SalesReceipt = 1,
        DailySummarySalesReceipt = 2,
        DailySummaryInvoice = 3,
        DoNotExport = 4
    }
    
    public enum CustomerCreateRule
    {
        PerMarketPlace = 0,
        PerOrder = 1
    }
    /// <summary>
    /// Item handling option while creating Invoice/Sales Receipt in Qbo when matching item not found.
    /// </summary>
    public enum QboItemCreateRule
    {
        DefaultItem = 0,
        SkipOrderWithoutMatching = 1,
        CreateNewItem = 3
    }

    public enum SalesTaxExportRule
    {
        DoNotExportSalesTaxFromDigitbridge = 0,
        ExportToDefaultSaleTaxItemAccount = 1
    }

    public enum OrderExtensionFlag
    {
        Qbo_Initial_Downloaded = 100001,
        Qbo_Tracking_Downloaded = 100002,
        Synced_Wtih_Error = 100255
    }
    //public enum QboPaymentType
    //{
    //    Expense,//--Payment is reimbursement for expense paid by cash made on behalf of the customer
    //    Check,//--Payment is reimbursement for expense paid by check made on behalf of the customer
    //    CreditCardCredit,//--Payment is reimbursement for a credit card credit made on behalf of the customer
    //    JournalEntry,//--Payment is linked to the representative journal entry
    //    CreditMemo,//--Payment is linked to the credit memo the customer has with the business
    //    Invoice,//--The invoice to which payment is applied
    //}
}
