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
        //CentralOrderToSalesOrder = 1,
        //ShipmentToInvoice ,

        public static string AddQboInvoice = "erp-qboinvoice-add-queue";
        public static string UpdateQboInvoice = "erp-qboinvoice-update-queue";
        public static string VoidQboInvoice = "erp-qboinvoice-void-queue";

        public static string AddQboInvoicePayment = "erp-qboinvoicepayment-add-queue";
        public static string DeleteQboInvoicePayment = "erp-qboinvoicepayment-delete-queue";

        public static string AddQboInvoiceReturn = "erp-qboinvoicereturn-add-queue";
        public static string DeleteQboInvoiceReturn = "erp-qboinvoicereturn-delete-queue";

    }
}
