using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ErpEventType : int
    {
        [Description("CentralOrder To SalesOrder")]
        CentralOrderToSalesOrder = 1,
        [Description("Shipment To Invoice")]
        ShipmentToInvoice ,

        [Description("Add QuickBooks Invoice")]
        AddQboInvoice ,
        [Description("Update QuickBooks Invoice")]
        UpdateQboInvoice ,
        [Description("Void QuickBooks Invoice")]
        VoidQboInvoice ,

        [Description("Add QuickBooks InvoicePayment")]
        AddQboInvoicePayment,
        [Description("Delete QuickBooks InvoicePayment")]
        DeleteQboInvoicePayment,

        [Description("Add QuickBooks InvoiceReturn")]
        AddQboInvoiceReturn,
        [Description("Delete QuickBooks InvoiceReturn")]
        DeleteQboInvoiceReturn,

    }
    public static class ErpEventQueueName
    {
        public static string CentralOrderToSalesOrder = DefaultQueue;
        public static string ShipmentToInvoice = DefaultQueue;
        public static string DefaultQueue = "erp-default-queue";

        public static string GetErpEventQueueName(this ErpEventType eventType)
        {
            switch (eventType)
            {
                case ErpEventType.CentralOrderToSalesOrder:
                    return CentralOrderToSalesOrder;
                case ErpEventType.ShipmentToInvoice:
                    return ShipmentToInvoice;
                case ErpEventType.AddQboInvoice:
                    return AddQboInvoice;
                case ErpEventType.UpdateQboInvoice:
                    return UpdateQboInvoice;
                case ErpEventType.VoidQboInvoice:
                    return VoidQboInvoice;
                case ErpEventType.AddQboInvoicePayment:
                    return AddQboInvoicePayment;
                case ErpEventType.DeleteQboInvoicePayment:
                    return DeleteQboInvoicePayment;
                case ErpEventType.AddQboInvoiceReturn:
                    return AddQboInvoiceReturn;
                case ErpEventType.DeleteQboInvoiceReturn:
                    return DeleteQboInvoiceReturn;
                default:
                    return DefaultQueue;
            }
        }

        public static string AddQboInvoice = "erp-qboinvoice-add-queue";
        public static string UpdateQboInvoice = "erp-qboinvoice-update-queue";
        public static string VoidQboInvoice = "erp-qboinvoice-void-queue";

        public static string AddQboInvoicePayment = "erp-qboinvoicepayment-add-queue";
        public static string DeleteQboInvoicePayment = "erp-qboinvoicepayment-delete-queue";

        public static string AddQboInvoiceReturn = "erp-qboinvoicereturn-add-queue";
        public static string DeleteQboInvoiceReturn = "erp-qboinvoicereturn-delete-queue";

    }
}
