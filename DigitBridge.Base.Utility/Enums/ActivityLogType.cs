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
       
    }
}
