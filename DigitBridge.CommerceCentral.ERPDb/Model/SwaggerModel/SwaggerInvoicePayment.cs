using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class SwaggerInvoicePayment<T> : SwaggerOne<T>
    {
        public InvoiceHeaderDto InvoiceHeader { get; set; }
    }
}
