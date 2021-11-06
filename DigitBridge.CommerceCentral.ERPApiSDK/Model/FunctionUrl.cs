using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    internal static class FunctionUrl
    {
        internal static string QuickBooksInvoice = "/addQuicksBooksInvoice";
        internal static string QuickBooksInvoiceVoid = "/addQuicksBooksInvoiceVoid";

        internal static string QuickBooksReturn = "/addQuicksBooksReturn";
        internal static string QuickBooksReturnDelete = "/addQuicksBooksReturnDelete";

        internal static string QuickBooksPayment = "/addQuicksBooksPayment";
        internal static string QuickBooksPaymentDelete = "/addQuicksBooksPaymentDelete";

        internal static string CreateSalesOrderByCentralOrder = "/addCreateSalesOrderByCentralOrder";
        internal static string CreateInvoiceByOrderShipment = "/addCreateInvoiceByOrderShipment";

        internal const string GetSalesOrderOpenList = "salesOrders/find";

        internal const string GetPurchaseOrderList = "purchaseOrder/find";
        internal const string CreatePoReceive = "poReceives";

        internal static string InventorySync = "/InventorySyncs";

        internal const string AddShipments = "shipments";
    }
}
