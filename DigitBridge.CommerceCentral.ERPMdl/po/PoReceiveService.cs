using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class PoReceiveService : PoTransactionService, IPoReceiveService
    {
        public PoReceiveService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public override PoTransactionService Init()
        {
            SetDtoMapper(new PoTransactionDataDtoMapperDefault());
            SetCalculator(new PoReceiveServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new PoReceiveServiceValidatorDefault(this, this.dbFactory));
            return this;
        }
        
        public virtual async Task GetPaymentWithPoHeaderAsync(PoReceivePayload payload, string poNum, int? transNum = null)
        {
            payload.PoTransactions = await GetPoTransactionDataDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, poNum,transNum);
            payload.PoHeader = await GetPoHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, poNum);
            payload.Success = true;
            payload.Messages = this.Messages;
        }

        public async Task<bool> GetByNumberAsync(PoReceivePayload payload, string poNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, poNumber, transNum);
        }

        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string poNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, poNumber, transNum);
        }

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(PoReceivePayload payload, string poNumber, int transNum)
        {
            return await base.DeleteByNumberAsync(payload, poNumber,transNum);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(PoReceivePayload payload, string poNumber, int transNum)
        {
            return base.DeleteByNumber(payload, poNumber, transNum);
        }

        public bool Add(PoReceivePayload payload)
        {
            return base.Add(payload);
        }

        public Task<bool> AddAsync(PoReceivePayload payload)
        {
            return base.AddAsync(payload);
        }

        public bool Update(PoReceivePayload payload)
        {
            return base.Update(payload);
        }

        public Task<bool> UpdateAsync(PoReceivePayload payload)
        {
            return base.UpdateAsync(payload);
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

        private PurchaseOrderService _purchaseOrderService;

        protected PurchaseOrderService PurchaseOrderService
        {
            get
            {
                if (_purchaseOrderService == null)
                    _purchaseOrderService = new PurchaseOrderService(dbFactory);
                return _purchaseOrderService;
            }
        }

        private ApInvoiceService _apInvoiceService;

        protected ApInvoiceService ApInvoiceService
        {
            get
            {
                if (_apInvoiceService == null)
                    _apInvoiceService = new ApInvoiceService(dbFactory);
                return _apInvoiceService;
            }
        }
        private InventoryService _inventoryService;

        protected InventoryService InventoryService
        {
            get
            {
                if (_inventoryService == null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        public int GetUnUpdateTransStatus()
        {
            return dbFactory.GetValue<ApInvoiceHeader,int>("SELECT TOP 1 TransStatus FROM PoTransaction WHERE TransUuid=@0",Data.PoTransaction.TransUuid.ToSqlParameter("@0"));
        }
        
        public async Task<int> GetUnUpdateTransStatusAsync()
        {
            return dbFactory.GetValue<ApInvoiceHeader,int>("SELECT TOP 1 TransStatus FROM PoTransaction WHERE TransUuid=@0",Data.PoTransaction.TransUuid.ToSqlParameter("@0"));
        }

        public override async Task<bool> SaveDataAsync()
        {
            if (Data.PoTransaction.RowNum.IsZero())
                Data.FirstAPReceiveStatus = Data.PoTransaction.TransStatus == (int)PoTransStatus.APReceive;
            else
                Data.FirstAPReceiveStatus=await GetUnUpdateTransStatusAsync()!=(int)PoTransStatus.APReceive;
            if (await base.SaveDataAsync())
            {
                await InventoryLogService.UpdateByPoReceiveAsync(_data);
                await ApInvoiceService.CreateOrUpdateApInvoiceByPoReceiveAsync(_data);
                await PurchaseOrderService.UpdateByPoReceiveAsync(_data);
                await InventoryService.UpdatAvgCostByPoReceiveAsync(_data);
                return true;
            }
            return false;
        }

        public override bool SaveData()
        {
            if (Data.PoTransaction.RowNum.IsZero())
                Data.FirstAPReceiveStatus = Data.PoTransaction.TransStatus == (int)PoTransStatus.APReceive;
            else
                Data.FirstAPReceiveStatus=GetUnUpdateTransStatus()!=(int)PoTransStatus.APReceive;
            if (base.SaveData())
            {
                InventoryLogService.UpdateByPoReceive(_data);
                ApInvoiceService.CreateOrUpdateApInvoiceByPoReceive(_data);
                PurchaseOrderService.UpdateByPoReceive(_data);
                InventoryService.UpdatAvgCostByPoReceive(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> DeleteDataAsync()
        {
            if (await base.DeleteDataAsync())
            {
                _data.PoTransactionItems.Clear();
                await InventoryLogService.UpdateByPoReceiveAsync(_data);
                return true;
            }
            return false;
        }

        public override bool DeleteData()
        {
            if (base.DeleteData())
            {
                _data.PoTransactionItems.Clear();
                InventoryLogService.UpdateByPoReceive(_data);
                return true;
            }
            return false;
        }
    }
}



