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