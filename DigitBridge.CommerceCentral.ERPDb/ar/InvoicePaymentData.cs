using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InvoicePaymentData
    {
        /// <summary>
        /// Get invoiceTransaction by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns> 
        public virtual async Task<long?> GetRowNumAsync(string invoiceNumber)
        {
            return await dbFactory.GetValueAsync<InvoiceTransaction, long?>($"SELECT TOP 1 RowNum FROM InvoiceTransaction where InvoiceNumber='{invoiceNumber}'");
        }
    }
}



