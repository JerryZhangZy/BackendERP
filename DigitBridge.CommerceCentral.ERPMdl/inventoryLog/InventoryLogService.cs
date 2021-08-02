
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InventoryLogService
    {
        private Inventory inventory;
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryLogService Init()
        {
            base.Init();
            inventory = new Inventory(dbFactory);
            SetDtoMapper(new InventoryLogDataDtoMapperDefault());
            SetCalculator(new InventoryLogServiceCalculatorDefault());
            AddValidator(new InventoryLogServiceValidatorDefault());
            return this;
        }

        private InventoryLogDataDtoMapperDefault _mapper = new InventoryLogDataDtoMapperDefault();

        private InventoryData GetInventoryById(string productUuid)
        {
            var data = new InventoryData(dbFactory);
            if (data.GetById(productUuid))
                return data;
            return null;
        }

        public long GetBatchNum()
        {
            return dbFactory.Db.ExecuteScalar<long>("select max(BatchNum) as maxBatchNum from InventoryLog;") + 1;
        }

        public IList<long> GetRowNumsByUuids(string logUuid)
        {
            return dbFactory.Db.Fetch<long>($"select RowNum from InventoryLog Where LogUuid='{logUuid}'");
        }

        public string FindProductUuidBySku(int masterAccountNum, int profileNum, string sku)
        {
            return dbFactory.Db.FirstOrDefault<string>($"select ProductUuid from Inventory where Sku='{sku}' and MasterAccountNum = {masterAccountNum} and ProfileNum={profileNum}");
        }

        public InventoryData GetInventoryBySku(int masterAccountNum, int profileNum, string sku)
        {
            var productUuid = FindProductUuidBySku(masterAccountNum, profileNum, sku);
            return GetInventoryById(productUuid);
        }
        public InventoryLogPayload DeleteByLogUuid(InventoryLogPayload payload)
        {
            if (!payload.HasInventoryLogUuids)
                return payload;

            var rowNumList = new List<long>();
            foreach (var logUuid in payload.InventoryLogUuids)
            {
                rowNumList.AddRange(GetRowNumsByUuids(logUuid));
            }

            foreach (var rowNum in rowNumList)
            {

                if (GetData(rowNum) && DeleteData())
                    payload.InventoryLogs.Add(ToDto());

            }

            return payload;
        }

        public InventoryLogPayload GetListByUuid(InventoryLogPayload payload)
        {
            if (!payload.HasInventoryLogUuids)
                return payload;

            var rowNumList = new List<long>();
            foreach(var logUuid in payload.InventoryLogUuids)
            {
                rowNumList.AddRange(GetRowNumsByUuids(logUuid));
            }

            foreach(var rowNum in rowNumList)
            {

                if (GetData(rowNum)&& ValidatePayload(payload))
                    payload.InventoryLogs.Add(ToDto());

            }

            return payload;
        }

        public InventoryLogPayload AddList(InventoryLogPayload payload)
        {
            if (!payload.HasInventoryLogs)
                return payload;
            payload.BatchNum = GetBatchNum();
            var succList = new List<InventoryLogDataDto>();
            foreach(var log in payload.InventoryLogs)
            {
                payload.InventoryLog = log;
                if (Add(payload))
                    succList.Add(ToDto());
            }
            payload.InventoryLog = null;
            payload.InventoryLogs = succList;

            return payload;
            //TODO:Validator验证，Mapper,补充完整Inventory信息进去，调用Dao
        }

        public InventoryLogPayload UpdateInventoryLogList(InventoryLogPayload payload)
        {
            if (!payload.HasInventoryLogs)
                return payload;

            var succList = new List<InventoryLogDataDto>();
            foreach (var log in payload.InventoryLogs)
            {
                payload.InventoryLog = log;
                if (Update(payload))
                    succList.Add(ToDto());
            }
            payload.InventoryLog = null;
            payload.InventoryLogs = succList;
            return payload;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryLogDataDto dto)
        {
            if (dto is null) 
                return false;
            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InventoryLogDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;
            return await SaveDataAsync().ConfigureAwait(false);
        }

        private void SupplyData(long batchNum)
        {
            Data.InventoryLog.BatchNum = batchNum;
            Data.InventoryLog.InventoryLogUuid = Guid.NewGuid().ToString();
            var inventoryData = GetInventoryBySku(Data.InventoryLog.MasterAccountNum, Data.InventoryLog.ProfileNum, Data.InventoryLog.SKU);//svc.GetInventoryBySku(x.DatabaseNum.Value, x.MasterAccountNum.Value, x.ProfileNum.Value, x.SKU);
            if (inventoryData != null)
            {
                var inventory = inventoryData.Inventory.First();
                Data.InventoryLog.ProductUuid = inventory.ProductUuid;
                Data.InventoryLog.InventoryUuid = inventory.InventoryUuid;
                Data.InventoryLog.WarehouseCode = inventory.WarehouseCode;
                Data.InventoryLog.BeforeInstock = inventory.Instock;
                Data.InventoryLog.BeforeBaseCost = inventory.BaseCost;
                Data.InventoryLog.BeforeUnitCost = inventory.UnitCost;
                Data.InventoryLog.BeforeAvgCost = inventory.AvgCost;
            }
        }



        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryLogPayload payload)
        {
            if (payload is null || !payload.HasInventoryLog)
                return false;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.InventoryLog);

            if (!ValidatePayload(payload))
                return false;

            // validate data for Add processing
            if (!Validate())
                return false;
            SupplyData(payload.BatchNum);
            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InventoryLogPayload payload)
        {
            if (payload is null || !payload.HasInventoryLog)
                return false;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.InventoryLog);

            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            SupplyData(payload.BatchNum);
            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryLogDataDto dto)
        {
            if (dto is null || !dto.HasInventoryLog || dto.InventoryLog.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.InventoryLog.RowNum.ToLong());
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryLogDataDto dto)
        {
            if (dto is null || !dto.HasInventoryLog || dto.InventoryLog.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.InventoryLog.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryLogPayload payload)
        {
            if (payload is null || !payload.HasInventoryLog || payload.InventoryLog.InventoryLog.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(payload.InventoryLog.InventoryLog.RowNum.ToLong());

            if (!ValidatePayload(payload))
                return false;

            // load data from dto
            FromDto(payload.InventoryLog);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryLogPayload payload)
        {
            if (payload is null || !payload.HasInventoryLog || payload.InventoryLog.InventoryLog.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(payload.InventoryLog.InventoryLog.RowNum.ToLong());

            if (!ValidatePayload(payload))
                return false;

            // load data from dto
            FromDto(payload.InventoryLog);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

    }
}



