using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboPaymentService
    {
        #region write erp invoice to qbo
        Task<bool> Export(string invoiceNumber);

        #endregion

        #region Query invoice from qbo

        #endregion
    }
}
