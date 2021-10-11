using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboInvoiceService
    {
        Task<bool> ExportAsync(string invoiceNumber);
        Task<bool> VoidQboInvoiceAsync(string invoiceNumber);
        Task<bool> DeleteQboInvoiceAsync(string invoiceNumber);
        Task<bool> GetQboInvoiceAsync(string invoiceNumber);

    }
}
