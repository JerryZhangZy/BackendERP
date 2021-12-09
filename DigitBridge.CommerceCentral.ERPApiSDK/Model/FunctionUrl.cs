using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    internal static class FunctionUrl
    {
        internal const string ResendEvent = "/api/erpevents/resend";

        internal static string QuickBooksInvoice = "/api/erpevents/addQuicksBooksInvoice";
        internal static string QuickBooksInvoiceVoid = "/api/erpevents/addQuicksBooksInvoiceVoid";

        internal static string QuickBooksReturn = "/api/erpevents/addQuicksBooksReturn";
        internal static string QuickBooksReturnDelete = "/api/erpevents/addQuicksBooksReturnDelete";

        internal static string QuickBooksPayment = "/api/erpevents/addQuicksBooksPayment";
        internal static string QuickBooksPaymentDelete = "/api/erpevents/addQuicksBooksPaymentDelete";

        internal static string CreateSalesOrderByCentralOrder = "/api/erpevents/addCreateSalesOrderByCentralOrder";
        internal static string CreateInvoiceByOrderShipment = "/api/erpevents/addCreateInvoiceByOrderShipment";

        internal static string UpdateEvent = "/api/erpevents";

        internal const string GetSalesOrderOpenList = "/api/wms/salesOrders/find";
        internal const string AckReceiveSalesOrders = "/api/wms/salesOrders/AckReceive";
        internal const string AckProcessSalesOrders = "/api/wms/salesOrders/AckProcess";
        internal const string ResendWMSShipment = "/api/wms/shipments/resend";

        internal const string GetPurchaseOrderList = "/api/wms/purchaseOrders/find";
        internal const string AckReceivePurchaseOrders = "/api/wms/purchaseOrders/AckReceive";
        internal const string AckProcessPurchaseOrders = "/api/wms/purchaseOrders/AckProcess";
        internal const string PoReceive = "/api/wms/purchaseOrders/receive";

        internal static string InventorySync = "/api/wms/inventory";

        internal const string AddShipments = "/api/wms/shipments";
        internal const string WMSShipmentList = "/api/wms/shipments/find";

        internal const string UnprocessList = "/api/commercecentral/invoices/list/unprocess";
        internal const string AckReceiveInvoices = "/api/commercecentral/invoices/AckReceive";
        internal const string AckProcessInvoices = "/api/commercecentral/invoices/AckProcess";

    }
}
