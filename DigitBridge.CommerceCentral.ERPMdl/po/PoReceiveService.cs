using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public override async Task<bool> SaveDataAsync()
        {
            if (await base.SaveDataAsync())
            {
                await InventoryLogService.UpdateByPoReceiveAsync(_data);
                return true;
            }
            return false;
        }

        public override bool SaveData()
        {
            if (base.SaveData())
            {
                InventoryLogService.UpdateByPoReceive(_data);
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



