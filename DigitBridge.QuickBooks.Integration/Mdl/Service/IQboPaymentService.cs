using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboPaymentService
    {
        Task<bool> ExportAsync(string invoiceNumber, int transNum);
        Task<bool> VoidQboPaymentAsync(string invoiceNumber, int transNum);
        Task<bool> DeleteQboPaymentAsync(string invoiceNumber, int transNum);
        Task<bool> GetQboPaymentAsync(string invoiceNumber, int transNum);
    }
}
