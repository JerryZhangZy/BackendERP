using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboRefundService
    {
        Task<bool> ExportByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> VoidQboRefundByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> DeleteQboRefundByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> GetQboRefundAsync(string invoiceNumber, int transNum);

        
        Task<bool> ExportByUuidAsync(string transUuid);
        Task<bool> VoidQboRefundByUuidAsync(string transUuid);
        Task<bool> DeleteQboRefundByUuidAsync(string transUuid);
    }
}
