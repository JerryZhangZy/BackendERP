


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

        public async Task<bool> NewPaymentAsync(InvoicePaymentPayload payload, string invoiceNumber)
        {
            NewData();

            if (!LoadInvoice(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            CopyInvoiceHeaderToTrans();

            var paidAmount = await InvoiceTransactionHelper.GetPaidAmountByInvoiceUuidAsync(dbFactory, Data.InvoiceData.InvoiceHeader.InvoiceUuid);

            //unpaid amount.
            Data.InvoiceTransaction.TotalAmount = Data.InvoiceData.InvoiceHeader.TotalAmount - paidAmount;

            return true;
        }

        /// <summary>
        /// Load returned qty for each trans return item 
        /// </summary>
        /// <returns></returns>
        protected virtual void Get()
        {
            if (this.Data.InvoiceReturnItems == null || this.Data.InvoiceReturnItems.Count == 0)
                return;

            var returnItems = InvoiceTransactionHelper.GetReturnItemsByInvoiceUuid(dbFactory, this.Data.InvoiceData.UniqueId);
            if (returnItems == null || returnItems.Count == 0)
                return;

            foreach (var item in this.Data.InvoiceReturnItems)
            {
                item.ReturnedQty = returnItems.Where(i => i.sku == item.SKU && i.rowNum != item.RowNum).Sum(j => j.returnQty).ToQty();//+ item.ReturnQty;
            }
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



