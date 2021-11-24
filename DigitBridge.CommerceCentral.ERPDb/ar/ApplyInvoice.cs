using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class ApplyInvoice
    {
        public string InvoiceUuid { get; set; }
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// This is used for update trans.
        /// </summary>
        public long? TransRowNum { get; set; }
        [JsonIgnore]
        public string TransUuid { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string QuickbookDocNum { get; set; }
        public decimal InvoiceTotalAmount { get; set; }
        public decimal InvoicePaidAmount { get; set; }
        public decimal InvoiceBalance { get; set; }
        public decimal PaidAmount { get; set; }
        [JsonIgnore]
        public bool Success { get; set; } = true;

        public InvoiceTransactionDataDto GenerateTransaction(InvoiceHeaderDto invoiceHeader, InvoiceTransactionDto templateTransaction)
        {
            InvoiceTransactionDataDto result = new InvoiceTransactionDataDto
            {
                InvoiceReturnItems = null,
                InvoiceDataDto = null,
                InvoiceTransaction = new InvoiceTransactionDto
                {
                    MasterAccountNum = templateTransaction.MasterAccountNum,
                    ProfileNum = templateTransaction.ProfileNum,
                    InvoiceUuid = invoiceHeader.InvoiceUuid,
                    InvoiceNumber = invoiceHeader.InvoiceNumber,

                    RowNum = TransRowNum,
                    TransUuid = TransRowNum.HasValue ? TransUuid : null,
                    CustomerCode = templateTransaction.CustomerCode,
                    AuthCode = templateTransaction.AuthCode,
                    CheckNum = templateTransaction.CheckNum,
                    PaidBy = templateTransaction.PaidBy,

                    TransType = (int)TransTypeEnum.Payment,
                    Currency = invoiceHeader.Currency,
                    TotalAmount = PaidAmount
                }
            };

            return result;
        }
    }
}
