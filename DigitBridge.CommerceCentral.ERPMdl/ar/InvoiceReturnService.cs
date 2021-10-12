


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
    public partial class InvoiceReturnService : InvoiceTransactionService
    {
        public InvoiceReturnService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceReturnServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoiceReturnServiceValidatorDefault(this, this.dbFactory));
            return this;
        }
        //public async Task<bool> GetDataAsync(string invoiceNumber, InvoiceReturnPayload payload)
        //{
        //    var success = await base.GetDataAsync(invoiceNumber, payload.MasterAccountNum, payload.ProfileNum, true);
        //    if (success && Data.InvoiceTransaction.TransType != (int)TransTypeEnum.Return)
        //    {
        //        AddError($"{invoiceNumber} isn't a return invoice number");
        //        return false;
        //    }
        //    return success;
        //}

        /// <summary>
        /// Get invoice returns with detail by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetReturnsAsync(InvoiceReturnPayload payload, string invoiceNumber, int? transNum = null)
        {
            payload.InvoiceTransactions = await base.GetDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, TransTypeEnum.Return, transNum);
            payload.Success = true;
        }


        public async Task<bool> GetByNumberAsync(InvoiceReturnPayload payload, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, invoiceNumber, TransTypeEnum.Return, transNum);
        }

        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, invoiceNumber, TransTypeEnum.Return, transNum);
        }

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(InvoiceReturnPayload payload, string invoiceNumber, int transNum)
        {
            return await base.DeleteByNumberAsync(payload, invoiceNumber, TransTypeEnum.Return, transNum);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(InvoiceReturnPayload payload, string invoiceNumber, int transNum)
        {
            return base.DeleteByNumber(payload, invoiceNumber, TransTypeEnum.Return, transNum);
        }

        private InventoryLogService _inventoryLogService;

        protected InventoryLogService InventoryLogService
        {
            get
            {
                if (_inventoryLogService == null)
                    _inventoryLogService = new InventoryLogService(dbFactory);
                return _inventoryLogService;
            }
        }

        public override bool SaveData()
        {
            if(base.SaveData())
            {
                InventoryLogService.UpdateByInvoiceReturn(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> SaveDataAsync()
        {
            if(await base.SaveDataAsync())
            {
                await InventoryLogService.UpdateByInvoiceReturnAsync(_data);
                return true;
            }
            return false;
        }

        public override bool DeleteData()
        {
            if (base.DeleteData())
            {
                _data.InvoiceReturnItems.Clear();
                InventoryLogService.UpdateByInvoiceReturn(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> DeleteDataAsync()
        {
            if(await base.DeleteDataAsync())
            {
                _data.InvoiceReturnItems.Clear();
                await InventoryLogService.UpdateByInvoiceReturnAsync(_data);
                return true;
            }
            return false;
        }
    }
}



