


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
        }

        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault());
            AddValidator(new InvoicePaymentServiceValidatorDefault(this, this.dbFactory));
            return this;
        }
        /// <summary>
        /// Get invoice payment with detail by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithInvoiceHeaderAsync(InvoicePaymentPayload payload, string invoiceNumber, int? transNum = null)
        {
            payload.InvoiceTransactions = await GetInvoicePaymentAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            payload.InvoiceHeader = await GetInvoiceHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
        }
        /// <summary>
        /// Get InvoiceHeader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        private async Task<InvoiceHeaderDto> GetInvoiceHeaderAsync(int masterAccountNum, int profileNum, string invoiceNumber)
        {
            var invoiceHeader = await new InvoiceHeader(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new InvoiceHeaderDto();
            if (invoiceHeader != null)
                new InvoiceDataDtoMapperDefault().WriteInvoiceHeader(invoiceHeader, dto);
            return dto;
        }

        /// <summary>
        /// Get InvoiceTransactionDtos by invoiceNumber and transNum
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        private async Task<List<InvoiceTransactionDto>> GetInvoicePaymentAsync(int masterAccountNum, int profileNum, string invoiceNumber, int? transNum = null)
        {
            var invoiceTransactions = await new InvoiceTransaction(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum, TransTypeEnum.Payment, transNum);
            var dtos = new List<InvoiceTransactionDto>();
            if (invoiceTransactions != null && invoiceTransactions.Count > 0)
            {
                foreach (var item in invoiceTransactions)
                {
                    var dto = new InvoiceTransactionDto();
                    new InvoiceTransactionDataDtoMapperDefault().WriteInvoiceTransaction(item, dto);
                    dtos.Add(dto);
                }

            }
            return dtos;
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
            return await base.AddAsync(invoiceTransactionPayload);
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
            return await base.UpdateAsync(invoiceTransactionPayload);
        }
    }
}



