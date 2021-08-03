


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
    public partial class InvoicePaymentService //: InvoiceTransactionService
    {
        IDataBaseFactory _dbFactory;
        public InvoicePaymentService(IDataBaseFactory dbFactory)//: base(dbFactory) { }
        {
            _dbFactory = dbFactory;
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

        public virtual async Task<bool> UpdateAsync(InvoicePaymentPayload payload)
        {
            var dto = payload.InvoiceTransaction;
            if (dto is null || !dto.RowNum.HasValue || dto.RowNum.Value < 0)
                return false;
            // validate data 
            if (dto.MasterAccountNum != payload.MasterAccountNum || dto.ProfileNum != payload.ProfileNum)
                throw new InvalidParameterException("Invalid request.");
            var invoiceTransaction = await new InvoiceTransaction(_dbFactory).GetByRowNumAsync(dto.RowNum.Value, payload.MasterAccountNum, payload.ProfileNum);
            if (invoiceTransaction == null)
                throw new NoContentException("No transaction to update.");
            //Make sure the TransUuid won't be changed.
            dto.TransUuid = null;
            // load data from dto
            new InvoiceTransactionDataDtoMapperDefault().ReadInvoiceTransaction(invoiceTransaction, dto);

            return await SavePaymentAsync(invoiceTransaction);
        }

        public async Task<bool> SavePaymentAsync(InvoiceTransaction invoiceTransaction)
        {
            //todo save other infos.
            _dbFactory.Begin();
            invoiceTransaction.SetDataBaseFactory(_dbFactory);
            if (!(await invoiceTransaction.SaveAsync().ConfigureAwait(false))) return false;
            _dbFactory.Commit();
            return true;
        }

    }
}



