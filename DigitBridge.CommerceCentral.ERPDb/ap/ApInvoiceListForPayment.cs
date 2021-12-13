using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class ApInvoiceListForPayment
    {
        public long RowNum { get; set; }
        public string ApInvoiceUuid { get; set; }
        public string ApInvoiceNumber { get; set; }
        public int ApInvoiceType { get; set; }
        public int ApInvoiceStatus { get; set; }
        public DateTime ApInvoiceDate { get; set; }
        public DateTime ApInvoiceTime { get; set; }
        public string VendorUuid { get; set; }
        public string VendorCode { get; set; }
        public string vendorName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BillDate { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Balance { get; set; }
        public long CreditAccount { get; set; }
        public long DebitAccount { get; set; }
        public decimal PayAmount { get; set; }

        
        public string TransUuid { get; set; }
        public long TransRowNum { get; set; } = 0;
        public int TransNum { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public static class ApInvoiceListForPaymentExtensions
    {

        public static ApInvoiceListForPayment FindByInvoiceUuid(this IList<ApInvoiceListForPayment> lst, string invoiceUuid)
            => (lst == null || string.IsNullOrEmpty(invoiceUuid))
                ? null
                : lst.FirstOrDefault((Func<ApInvoiceListForPayment, bool>)(item => ObjectExtensions.EqualsIgnoreSpace(item.ApInvoiceUuid, (string)invoiceUuid)));

        public static ApInvoiceListForPayment FindByTransUuid(this IList<ApInvoiceListForPayment> lst, string TransUuid)
            => (lst == null || string.IsNullOrEmpty(TransUuid))
                ? null
                : lst.FirstOrDefault(item => item.TransUuid.EqualsIgnoreSpace(TransUuid));

    }
}
