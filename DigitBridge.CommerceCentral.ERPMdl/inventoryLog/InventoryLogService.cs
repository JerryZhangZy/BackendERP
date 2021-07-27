
    

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
            var list = InventoryLogHelper.QueryInventoryLogByUuids(payload.LogUuids);
            if (list.SetDataBaseFactory(dbFactory).Delete() > 0)
                payload.InventoryLogs = _mapper.WriteInventoryLogDtoList(list, null);
            return payload;
        }

        public InventoryLogPayload GetListByUuid(InventoryLogPayload payload)
        {
            var list = InventoryLogHelper.QueryInventoryLogByUuids(payload.LogUuids);
            payload.InventoryLogs= _mapper.WriteInventoryLogDtoList(list, null);
            return payload;
        }

        //public List<InventoryLogDto> GetListByUuids(IList<string> logUuids)
        //{
        //    var list = InventoryLogHelper.QueryInventoryLogByUuids(logUuids);
        //    return _mapper.WriteInventoryLogDtoList(list, null);
        //}

        public InventoryLogPayload AddList(InventoryLogPayload payload)
        {
            if (payload.HasInventoryLogs)
                return payload;
            var svc = new InventoryData(dbFactory);
            var batchNum = GetBatchNum();
            var dtoList = payload.InventoryLogs.ToList();
            dtoList.ForEach(x =>
            {
                x.BatchNum = batchNum;
                x.RowNum = 0;
                x.InventoryLogUuid = Guid.NewGuid().ToString();
                var inventoryData = GetInventoryBySku(x.MasterAccountNum.Value, x.ProfileNum.Value, x.SKU);//svc.GetInventoryBySku(x.DatabaseNum.Value, x.MasterAccountNum.Value, x.ProfileNum.Value, x.SKU);
                if (inventoryData != null)
                {
                    var inventory = inventoryData.Inventory.First();
                    x.ProductUuid = inventory.ProductUuid;
                    x.InventoryUuid = inventory.InventoryUuid;
                    x.WarehouseCode = inventory.WarehouseCode;
                    x.BeforeInstock = inventory.Instock;
                    x.BeforeBaseCost = inventory.BaseCost;
                    x.BeforeUnitCost = inventory.UnitCost;
                    x.BeforeAvgCost = inventory.AvgCost;
                }
            });
            var mapper = new InventoryLogDataDtoMapperDefault();
            var datalist = mapper.ReadInventoryLogDtoList(null, dtoList);
            var addcount = datalist.Count();
            datalist.SetDataBaseFactory(dbFactory).Save();
            datalist = InventoryLogHelper.QueryInventoryLogByBatchNum(batchNum);
            payload.InventoryLogs = mapper.WriteInventoryLogDtoList(datalist, null);
            return payload;
            //TODO:Validator验证，Mapper,补充完整Inventory信息进去，调用Dao
        }

        public InventoryLogPayload UpdateInventoryLogList(InventoryLogPayload payload)
        {
            if (payload.HasInventoryLogs)
                return payload;
            var logUuidList = payload.InventoryLogs.Select(r => r.LogUuid).Distinct();

            var dlist = new List<InventoryLog>();
            foreach (var uuid in logUuidList)
            {
                dlist.AddRange(InventoryLogHelper.QueryInventoryLogByUuid(uuid));
            }
            var data = _mapper.ReadInventoryLogDtoList(dlist, payload.InventoryLogs.ToList()).Where(x => x.RowNum > 0).ToList();
            int result = data.Count();
            data.SetDataBaseFactory(dbFactory).Save();

            dlist = new List<InventoryLog>();
            foreach (var uuid in logUuidList)
            {
                dlist.AddRange(InventoryLogHelper.QueryInventoryLogByUuid(uuid));
            }
            var ulist = payload.InventoryLogs.Select(r => r.UniqueId).ToList();
            dlist = dlist.Where(r => ulist.Contains(r.UniqueId)).ToList();
            payload.InventoryLogs = _mapper.WriteInventoryLogDtoList(dlist, null);
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

    }
}



