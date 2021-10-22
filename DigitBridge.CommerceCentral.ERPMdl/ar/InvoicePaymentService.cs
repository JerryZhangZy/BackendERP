


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using System.Text;

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
            SetCalculator(new InvoicePaymentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoicePaymentServiceValidatorDefault(this, this.dbFactory));
            return this;
        }
        /// <summary>
        /// Get invoice payment with detail and invoiceheader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithInvoiceHeaderAsync(InvoicePaymentPayload payload, string invoiceNumber, int? transNum = null)
        {
            payload.InvoiceTransactions = await GetDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
            payload.InvoiceHeader = await GetInvoiceHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            payload.Success = true;
            payload.Messages = this.Messages;
        }

        /// <summary>
        /// get payments and invoice data.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        public virtual async Task<(List<InvoiceTransaction>, InvoiceData)> GetPaymentsWithInvoice(int masterAccountNum, int profileNum, string invoiceNumber, int? transNum = null)
        {
            var tranDatas = await GetDataListAsync(masterAccountNum, profileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
            var payments = new List<InvoiceTransaction>();
            if (tranDatas != null && tranDatas.Count > 0)
            {
                payments = tranDatas.Select(i => i.InvoiceTransaction).ToList();
            }
            LoadInvoice(invoiceNumber, profileNum, masterAccountNum);
            return (payments, Data.InvoiceData);
        }


        //public virtual async Task<bool> AddAsync(InvoicePaymentPayload payload)
        //{
        //    var invoiceTransactionPayload = new InvoiceTransactionPayload
        //    {
        //        InvoiceTransaction = new InvoiceTransactionDataDto()
        //        {
        //            InvoiceTransaction = payload.InvoiceTransaction
        //        },
        //        MasterAccountNum = payload.MasterAccountNum,
        //        ProfileNum = payload.ProfileNum
        //    };
        //    return await base.AddAsync(invoiceTransactionPayload);
        //}
        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        //public virtual async Task<bool> UpdateAsync(InvoicePaymentPayload payload)
        //{
        //    var invoiceTransactionPayload = new InvoiceTransactionPayload
        //    {
        //        InvoiceTransaction = new InvoiceTransactionDataDto()
        //        {
        //            InvoiceTransaction = payload.InvoiceTransaction
        //        },
        //        MasterAccountNum = payload.MasterAccountNum,
        //        ProfileNum = payload.ProfileNum
        //    };
        //    return await base.UpdateAsync(invoiceTransactionPayload);
        //}

        public async Task<bool> GetByNumberAsync(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
        }

        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
        }

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            return await base.DeleteByNumberAsync(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            return base.DeleteByNumber(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
        }


        #region New payment

        public async Task<bool> NewPaymentByInvoiceNumberAsync(InvoiceNewPaymentPayload payload, string invoiceNumber)
        {
            NewData();

            if (!LoadInvoice(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;

            return await LoadInvoiceList(payload, Data.InvoiceData.InvoiceHeader.CustomerCode);
        }

        public async Task<bool> NewPaymentByCustomerCode(InvoiceNewPaymentPayload payload, string customerCode)
        {
            NewData();
            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;
            return await LoadInvoiceList(payload, customerCode);
        }

        private async Task<bool> LoadInvoiceList(InvoiceNewPaymentPayload payload, string customerCode)
        {
            var invoicePayload = new InvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum
            };

            var invoiceQuery = new InvoiceQuery();
            invoiceQuery.InitForNewPaymet(customerCode);

            var srv = new InvoiceList(this.dbFactory, invoiceQuery);
            await srv.GetInvoiceListAsync(invoicePayload);

            if (!invoicePayload.Success)
                this.Messages = this.Messages.Concat(srv.Messages).ToList();

            payload.InvoiceList = invoicePayload.InvoiceList;

            payload.InvoiceListCount = invoicePayload.InvoiceListCount;

            return invoicePayload.Success;
        }

        #endregion


        #region To qbo queue 
        /// <summary>
        /// convert erp invoice payment to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<bool> AddQboPaymentEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPayment");
        }

        public async Task<bool> DeleteQboRefundEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPaymentDelete");
        }
        #endregion
    }
}



