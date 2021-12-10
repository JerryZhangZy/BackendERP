using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ActivityLogType : int
    {
        Customer = 0,
        Inventory = 1,
        SalesOrder = 2,
        Shipment = 3,
        Invoice = 4,
        InvoicePayment = 5,
        InvoiceReturn = 6,
        MiscInvoice = 7,
        MiscInvoicePayment = 8,
        InventoryUpdate = 9,
        WarehouseTransfer = 10,
        PurchaseOrder = 11,
        PoReceive = 12,
        PoReceiveClose = 13,
        PoCancel = 14,
        PoReturn = 15,
        ApInvoice = 16,
        ApInvoicePayment = 17,
        Vendor = 18,
        InitNumber=19
    }

    public static class ActivityLogTypeExtensions
    {
        public static int ToInt(this ActivityLogType value) => (int)value;
        public static string ToName(this ActivityLogType value) => value switch
        {
            ActivityLogType.Customer => "Customer",
            ActivityLogType.Inventory => "Inventory",
            ActivityLogType.SalesOrder => "Sales Order",
            ActivityLogType.Shipment => "Shipment",
            ActivityLogType.Invoice => "Invoice",
            ActivityLogType.InvoicePayment => "Invoice Payment",
            ActivityLogType.InvoiceReturn => "Invoice Return",
            ActivityLogType.MiscInvoice => "Pre Payment",
            ActivityLogType.MiscInvoicePayment => "Pre Payment Apply",
            ActivityLogType.InventoryUpdate => "Inventory Update",
            ActivityLogType.WarehouseTransfer => "Warehouse Transfer",
            ActivityLogType.PurchaseOrder => "Purchase Order",
            ActivityLogType.PoReceive => "P/O Receive",
            ActivityLogType.PoReceiveClose => "P/O Receive Close",
            ActivityLogType.PoCancel => "P/O Cancel",
            ActivityLogType.PoReturn => "P/O Return",
            ActivityLogType.ApInvoice => "Bill",
            ActivityLogType.ApInvoicePayment => "Bill Payment",
            ActivityLogType.Vendor => "Vendor",
            ActivityLogType.InitNumber => "Init Number Setup",
            _ => string.Empty,
        };

    }
}
