using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboInvoiceService
    {
        #region write erp invoice to qbo
        Task<bool> ExportAsync(string invoiceNumber);

        #endregion

        #region Query invoice from qbo

        #endregion
    }
}
