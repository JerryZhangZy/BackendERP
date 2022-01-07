using System.Threading.Tasks;
namespace DigitBridge.QuickBooks.Integration
{
    interface IQboPaymentService
    {
        Task<bool> ExportByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> VoidQboPaymentByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> DeleteQboPaymentByNumberAsync(string invoiceNumber, int transNum);
        Task<bool> GetQboPaymentByNumberAsync(string invoiceNumber, int transNum);

        Task<bool> ExportByUuidAsync(string transUuid);
        Task<bool> VoidQboPaymentByUuidAsync(string transUuid);
        Task<bool> DeleteQboPaymentByUuidAsync(string transUuid);
    }
}
