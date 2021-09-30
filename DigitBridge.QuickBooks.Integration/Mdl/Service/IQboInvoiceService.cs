using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using QboInvoiceApi = DigitBridge.QuickBooks.Integration.Mdl.Qbo.QboInvoiceService;
using DigitBridge.Base.Utility;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboInvoiceService  
    {
        #region write erp invoice to qbo
        Task<bool> Export(InvoicePayload payload, string invoiceNumber);

        #endregion

        #region Query invoice from qbo

        #endregion
    }
}
