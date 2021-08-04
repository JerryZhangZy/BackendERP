


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoicePaymentService : InvoiceTransactionService
    {
        public InvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
            AddValidator(new InvoicePaymentServiceValidatorDefault());
        }
        /// <summary>
        /// Get invoice payment with detail by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithInvoiceHeaderAsync(string invoiceNumber, InvoicePaymentPayload payload)
        {
            payload.InvoiceTransaction = await GetInvoicePaymentAsync(invoiceNumber, payload.MasterAccountNum, payload.ProfileNum);
            payload.InvoiceHeader = await GetInvoiceHeaderAsync(invoiceNumber, payload.MasterAccountNum, payload.ProfileNum);
        }
        /// <summary>
        /// Get InvoiceHeader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        private async Task<InvoiceHeaderDto> GetInvoiceHeaderAsync(string invoiceNumber, int masterAccountNum, int profileNum)
        {
            var invoiceHeader = await new InvoiceHeader(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new InvoiceHeaderDto();
            if (invoiceHeader != null)
                new InvoiceDataDtoMapperDefault().WriteInvoiceHeader(invoiceHeader, dto);
            return dto;
        }

        /// <summary>
        /// Get InvoiceHeader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        private async Task<InvoiceTransactionDto> GetInvoicePaymentAsync(string invoiceNumber, int masterAccountNum, int profileNum)
        {
            var invoiceTransaction = await new InvoiceTransaction(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new InvoiceTransactionDto();
            if (invoiceTransaction != null)
            {
                new InvoiceTransactionDataDtoMapperDefault().WriteInvoiceTransaction(invoiceTransaction, dto);
            }
            return dto;
        }

        public virtual async Task<bool> AddAsync(InvoicePaymentPayload payload)
        {
            var invoiceTransactionPayload = new InvoiceTransactionPayload
            {
                InvoiceTransaction = new InvoiceTransactionDataDto()
                {
                    InvoiceTransaction = payload.InvoiceTransaction
                },
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum
            };
            return await AddAsync(invoiceTransactionPayload);
        }
        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoicePaymentPayload payload)
        {
            var invoiceTransactionPayload = new InvoiceTransactionPayload
            {
                InvoiceTransaction = new InvoiceTransactionDataDto()
                {
                    InvoiceTransaction = payload.InvoiceTransaction
                },
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum
            };
            return await UpdateAsync(invoiceTransactionPayload);
        }
    }
}



