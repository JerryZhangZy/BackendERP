using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPEventSDK
{
    internal static class ERPEventFunctionUrl
    {
        internal static string QuickBooksInvoice = "/addQuicksBooksInvoice";
        internal static string QuickBooksInvoiceVoid = "/addQuicksBooksInvoiceVoid";

        internal static string QuickBooksReturn = "/addQuicksBooksReturn";
        internal static string QuickBooksReturnDelete = "/addQuicksBooksReturnDelete";

        internal static string QuickBooksPayment = "/addQuicksBooksPayment";
        internal static string QuickBooksPaymentDelete = "/addQuicksBooksPaymentDelete";

        internal static string CreateSalesOrderByCentralOrder = "/addCreateSalesOrderByCentralOrder";
        internal static string CreateInvoiceByOrderShipment = "/addCreateInvoiceByOrderShipment";
    }
}
