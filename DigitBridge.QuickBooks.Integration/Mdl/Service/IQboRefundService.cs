using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboRefundService
    {
        Task<bool> ExportAsync(string invoiceNumber, int transNum);
        Task<bool> VoidQboRefundAsync(string invoiceNumber, int transNum);
        Task<bool> DeleteQboRefundAsync(string invoiceNumber, int transNum);
        Task<bool> GetQboRefundAsync(string invoiceNumber, int transNum);
    }
}
