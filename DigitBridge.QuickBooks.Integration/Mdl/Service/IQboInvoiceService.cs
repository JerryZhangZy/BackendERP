using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboInvoiceService
    {
        Task<bool> ExportByNumberAsync(string invoiceNumber);
        Task<bool> VoidQboInvoiceByNumberAsync(string invoiceNumber);
        Task<bool> DeleteQboInvoiceByNumberAsync(string invoiceNumber);
        Task<bool> GetQboInvoiceByNumberAsync(string invoiceNumber);


        Task<bool> ExportByUuidAsync(string invoiceUuid);
        Task<bool> VoidQboInvoiceByUuidAsync(string invoiceUuid);
        Task<bool> DeleteQboInvoiceByUuidAsync(string invoiceUuid); 

    }
}
