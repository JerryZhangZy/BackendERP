
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InventoryLogService
    {
        private InventoryLogDataDtoMapperDefault _mapper = new InventoryLogDataDtoMapperDefault();
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryLogService Init()
        {
            base.Init();
            SetDtoMapper(new InventoryLogDataDtoMapperDefault());
            SetCalculator(new InventoryLogServiceCalculatorDefault());
            AddValidator(new InventoryLogServiceValidatorDefault());
            return this;
        }
        public bool DeleteByLogUuid(string logUuid)
        {
            InventoryLogHelper.DeleteByLogUuid(logUuid);
            return true;
        }

        public List<InventoryLogDto> GetListByUuid(string logUuid)
        {
            var list = InventoryLogHelper.QueryInventoryLogByUuid(logUuid);
            return _mapper.WriteInventoryLogDtoList(list, null);
        }

        private Inventory QueryInventoryBySku(int masterAccountNum,int profileNum,string sku)
        {
            return InventoryHelper.QueryInventoryBySku(masterAccountNum,profileNum, sku);
        }

        public bool AddList(List<InventoryLogDto> dtoList)
        {
            if (dtoList == null || dtoList.Count()==0)
                return false;
            if (dtoList.Exists(r => string.IsNullOrEmpty(r.SKU)))
                return false;
            if (dtoList.Exists(r => string.IsNullOrEmpty(r.LogUuid)))
                return false;
            var svc = new InventoryData(dbFactory);
            var batchNum = InventoryLogHelper.GetBatchNum();
            
            dtoList.ForEach(x =>
            {
                x.BatchNum = batchNum;
                var inventory = InventoryHelper.QueryInventoryBySku(x.MasterAccountNum.Value,x.ProfileNum.Value, x.SKU);//svc.GetInventoryBySku(x.DatabaseNum.Value, x.MasterAccountNum.Value, x.ProfileNum.Value, x.SKU);
                if (inventory != null)
                {
                    x.ProductUuid = inventory.ProductUuid;
                    x.InventoryUuid = inventory.InventoryUuid;
                    x.WarehouseUuid = inventory.WarehouseUuid;
                    x.BeforeInstock = inventory.Instock;
                    x.BeforeBaseCost = inventory.BaseCost;
                    x.BeforeUnitCost = inventory.UnitCost;
                    x.BeforeAvgCost = inventory.AvgCost;
                }
            });
            var mapper = new InventoryLogDataDtoMapperDefault();
            var datalist = mapper.ReadInventoryLogDtoList(null,dtoList);

            return datalist.SetDataBaseFactory<InventoryLog>(dbFactory).Save();
            //TODO:Validator验证，Mapper,补充完整Inventory信息进去，调用Dao
        }

        public bool UpdateInventoryLogList(List<InventoryLogDto> list)
        {
            if (list == null || list.Count() == 0)
                return false;
            var logUuidList = list.Select(r => r.LogUuid).Distinct();

            var dlist = new List<InventoryLog>();
            foreach (var uuid in logUuidList)
            {
                dlist.AddRange(InventoryLogHelper.QueryInventoryLogByUuid(uuid));
            }
            var data = _mapper.ReadInventoryLogDtoList(dlist, list);

            return dlist.SetDataBaseFactory<InventoryLog>(dbFactory).Save();
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



