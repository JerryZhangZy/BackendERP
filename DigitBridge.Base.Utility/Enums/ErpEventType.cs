using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ErpEventActionStatus : int
    {
        Pending = -1,
        Success = 0,
        Other = 1,
    }

    public enum ErpEventType : int
    {
        Default = 0,

        [Description("CentralOrder To SalesOrder")]
        CentralOrderToSalesOrder = 1,

        [Description("Shipment To Invoice")]
        ShipmentToInvoice = 2,

        [Description("Erp invoice QuickBooks Invoice")]
        InvoiceToQboInvoice = 3,

        [Description("Void QuickBooks Invoice")]
        VoidQboInvoice = 4,

        [Description("Erp payment to QuickBooks Payment")]
        InvoicePaymentToQboPayment = 5,

        [Description("Void QuickBooks Payment")]
        DeleteQboPayment = 6,

        [Description("Erp return to QuickBooks Refund")]
        InvoiceRetrunToQboRefund = 7,

        [Description("Void QuickBooks Refund")]
        DeleteQboRefund = 8,

        [Description("Create shipment by wms")]
        ShipmentFromWMS = 9,

        [Description("Create ProductExt and Inventory from ProductBasic")]
        SyncProduct = 10,

        [Description("Create Po transaction by wms poreceive")]
        PoReceiveFromWMS = 11,

        [Description("Import customer file to erp")]
        ErpImportCustomer = 20,
        [Description("Import inventory file to erp")]
        ErpImportInventory = 21,
        [Description("Import inventoryupdate file to erp")]
        ErpImportInventoryUpdate = 22,
        [Description("Import warehousetransfer file to erp")]
        ErpImportWarehouseTransfer = 23,
        [Description("Import salesorder file to erp")]
        ErpImportSalesOrder = 24,
        [Description("Import shipment file to erp")]
        ErpImportShipment = 25,
        [Description("Import invoice file to erp")]
        ErpImportInvoice = 26,
        [Description("Import invoicepayment file to erp")]
        ErpImportInvoicePayment = 27,
        [Description("Import vendor file to erp")]
        ErpImportVendor = 28,
        [Description("Import purchaseorder file to erp")]
        ErpImportPurchaseOrder = 29,
        [Description("Import poreceive file to erp")]
        ErpImportPoReceive = 30,
    }

    public static class ErpEventQueueName
    {

        public static string GetErpEventQueueName(this ErpEventType eventType)
        {
            switch (eventType)
            {
                case ErpEventType.CentralOrderToSalesOrder:
                    return CentralOrderToSalesOrder;
                case ErpEventType.ShipmentToInvoice:
                    return ShipmentToInvoice;
                case ErpEventType.InvoiceToQboInvoice:
                    return AddQboInvoice;
                case ErpEventType.VoidQboInvoice:
                    return VoidQboInvoice;
                case ErpEventType.InvoicePaymentToQboPayment:
                    return AddQboInvoicePayment;
                case ErpEventType.DeleteQboPayment:
                    return DeleteQboInvoicePayment;
                case ErpEventType.InvoiceRetrunToQboRefund:
                    return AddQboInvoiceReturn;
                case ErpEventType.DeleteQboRefund:
                    return DeleteQboInvoiceReturn;
                case ErpEventType.ShipmentFromWMS:
                    return ERPCreateShipmentByWMS;
                case ErpEventType.SyncProduct:
                    return SyncProduct;
                case ErpEventType.PoReceiveFromWMS:
                    return ERPCreatePoReceiveByWMS;

                case ErpEventType.ErpImportCustomer:
                    return ERPQueueSetting.ErpImportCustomerQueue;
                case ErpEventType.ErpImportInventory:
                    return ERPQueueSetting.ErpImportInventoryQueue;
                case ErpEventType.ErpImportInventoryUpdate:
                    return ERPQueueSetting.ErpImportInventoryUpdateQueue;
                case ErpEventType.ErpImportWarehouseTransfer:
                    return ERPQueueSetting.ErpImportWarehouseTransferQueue;
                case ErpEventType.ErpImportSalesOrder:
                    return ERPQueueSetting.ErpImportSalesOrderQueue;
                case ErpEventType.ErpImportShipment:
                    return ERPQueueSetting.ErpImportShipmentQueue;
                case ErpEventType.ErpImportInvoice:
                    return ERPQueueSetting.ErpImportInvoiceQueue;
                case ErpEventType.ErpImportInvoicePayment:
                    return ERPQueueSetting.ErpImportInvoicePaymentQueue;
                case ErpEventType.ErpImportVendor:
                    return ERPQueueSetting.ErpImportVendorQueue;
                case ErpEventType.ErpImportPurchaseOrder:
                    return ERPQueueSetting.ErpImportPurchaseOrderQueue;
                case ErpEventType.ErpImportPoReceive:
                    return ERPQueueSetting.ErpImportPoReceiveQueue;

                default:
                    return DefaultQueue;
            }
        }





        public static string DefaultQueue = "erp-default-queue";

        public static string CentralOrderToSalesOrder = ERPQueueSetting.ERPCreateSalesOrderByCentralorderQueue;
        public static string ShipmentToInvoice = ERPQueueSetting.ERPCreateInvoiceByOrdershipmentQueue;

        public static string AddQboInvoice = ERPQueueSetting.ERPQuickBooksInvoiceQueue;
        //public static string UpdateQboInvoice = "erp-qboinvoice-update-queue";
        public static string VoidQboInvoice = ERPQueueSetting.ERPQuickBooksInvoiceVoidQueue;

        public static string AddQboInvoicePayment = ERPQueueSetting.ERPQuickBooksPaymentQueue;
        public static string DeleteQboInvoicePayment = ERPQueueSetting.ERPQuickBooksPaymentDeleteQueue;

        public static string AddQboInvoiceReturn = ERPQueueSetting.ERPQuickBooksReturnQueue;
        public static string DeleteQboInvoiceReturn = ERPQueueSetting.ERPQuickBooksReturnDeleteQueue;

        public static string SyncProduct = ERPQueueSetting.ERPSyncProductQueue;
        public static string ERPCreateShipmentByWMS = ERPQueueSetting.ERPCreateShipmentByWMSQueue;

        public static string ERPCreatePoReceiveByWMS = ERPQueueSetting.ERPCreatePoReceiveByWMSQueue;
    }
}