


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoiceReturnService : InvoiceTransactionService, IInvoiceReturnService
    {
        public InvoiceReturnService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        #region override methods

        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceReturnServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoiceReturnServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override async Task BeforeSaveAsync()
        {
            try
            {
                await base.BeforeSaveAsync();
                if (this.Data?.InvoiceTransaction != null)
                {
                    await InvoiceService.UpdateInvoiceBalanceAsync(this.Data.InvoiceTransaction.TransUuid, true);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override void BeforeSave()
        {
            try
            {
                base.BeforeSave();
                if (this.Data?.InvoiceTransaction != null)
                {
                    InvoiceService.UpdateInvoiceBalance(this.Data.InvoiceTransaction.TransUuid, true);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override async Task AfterSaveAsync()
        {
            try
            {
                await base.AfterSaveAsync();
                if (this.Data?.InvoiceTransaction != null)
                {
                    await InvoiceService.UpdateInvoiceBalanceAsync(this.Data.InvoiceTransaction.TransUuid);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override void AfterSave()
        {
            try
            {
                base.AfterSave();
                if (this.Data?.InvoiceTransaction != null)
                {
                    InvoiceService.UpdateInvoiceBalance(this.Data.InvoiceTransaction.TransUuid);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override async Task SaveSuccessAsync()
        {
            try
            {
                await base.SaveSuccessAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override void SaveSuccess()
        {
            try
            {
                base.SaveSuccess();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Sub class should override this method to return new ActivityLog object for service
        /// </summary>
        protected override ActivityLog GetActivityLog() =>
            new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.InvoiceReturn,
                Action = (int)this.ProcessMode,
                LogSource = "InvoiceReturnService",

                MasterAccountNum = this.Data.InvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.InvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.InvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.InvoiceTransaction.TransUuid,
                ProcessNumber = $"{this.Data.InvoiceTransaction.InvoiceNumber}-{this.Data.InvoiceTransaction.TransUuid}",
                ChannelNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods


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
            await LoadInvoiceAsync(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum);
            payload.InvoiceDataDto = this.ToDto().InvoiceDataDto;
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
            if (base.SaveData())
            {
                InventoryLogService.UpdateByInvoiceReturn(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> SaveDataAsync()
        {
            if (await base.SaveDataAsync())
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
            if (await base.DeleteDataAsync())
            {
                _data.InvoiceReturnItems.Clear();
                await InventoryLogService.UpdateByInvoiceReturnAsync(_data);
                return true;
            }
            return false;
        }

        #region New return

        public async Task<bool> NewReturnAsync(InvoiceReturnPayload payload, string invoiceNumber)
        {
            NewData();

            if (!(await LoadInvoiceAsync(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum)))
            {
                this.AddError($"Invoice Number: {invoiceNumber} not found.");
                return false;
            }
            await LoadReturnedQtyAsync(this.Data.InvoiceData);
            CopyInvoiceHeaderToTrans();
            CopyInvoiceItemsToReturnItems();

            return true;
        }

        #endregion

        
        #region To qbo queue 

        private QboReturnClient _qboReturnClient;

        protected QboReturnClient qboReturnClient
        {
            get
            {
                if (_qboReturnClient is null)
                    _qboReturnClient = new QboReturnClient();
                return _qboReturnClient;
            }
        }

        /// <summary>
        /// convert erp invoice return to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<bool> AddQboRefundEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await qboReturnClient.SendAddQboReturnAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksReturn");
        }

        public async Task<bool> DeleteQboRefundEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await qboReturnClient.SendDeleteQboReturnAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksReturnDelete");
        }

        #endregion

        #region add return
        public virtual async Task<bool> AddAsync(InvoiceReturnPayload payload)
        {
            return await base.AddAsync(payload);
        }

        public virtual bool Add(InvoiceReturnPayload payload)
        {
            return base.Add(payload);
        }

        public virtual bool Add(InvoiceTransactionDataDto dto)
        {
            return base.Add(dto);
        }
        public virtual async Task<bool> AddAsync(InvoiceTransactionDataDto dto)
        {
            return await base.AddAsync(dto);
        }

        #endregion

        #region update return

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(InvoiceReturnPayload payload)
        {
            return await base.UpdateAsync(payload);
        }

        public async override Task<bool> UpdateAsync(InvoiceTransactionPayload payload)
        {
            bool result = await base.UpdateAsync(payload);

            if (Data.InvoiceTransaction.TransStatus == (int)InvoiceReturnStatus.Closed)
            {
                InvoiceTransactionDataDto trans = new InvoiceTransactionDataDto();
                this.DtoMapper.ReadDto(Data, trans);
                result &= await InventoryLogService.ReceiveInvoiceTransactionReturnbackItem(Data);
                result &= await InvoiceService.ReceivedInvoiceTransactionReturnbackItem(trans);
            }

            return result;
        }

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual bool Update(InvoiceReturnPayload payload)
        {
            return base.Update(payload);
        }

        #endregion
    }
}



