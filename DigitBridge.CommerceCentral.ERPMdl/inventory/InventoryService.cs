


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
    public partial class InventoryService
    {
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryService Init()
        {
            base.Init();
            SetDtoMapper(new InventoryDataDtoMapperDefault());
            SetCalculator(new InventoryServiceCalculatorDefault());
            AddValidator(new InventoryServiceValidatorDefault());
            return this;
        }

        public int DeleteInventoryLog(string logUuid)
        {
            var svc = new InventoryData(dbFactory);
            return svc.DeleteInventoryLogByLogUuid(logUuid);
        }

        public List<InventoryLogDto> GetInventoryLogByUuid(string logUuid)
        {
            var svc = new InventoryData(dbFactory);
            var list = svc.GetInventoryLogByLogUuid(logUuid);
            var data = new InventoryData() { InventoryLog = list };
            var datadto = ToDto(data);
            return datadto.InventoryLog.ToList() ;
        }

        public bool AddInventoryLogList(List<InventoryLogDto> list)
        {
            if (list == null || list.Count() == 0)
                return false;
            if (list.Exists(r => string.IsNullOrEmpty(r.SKU)))
                return false;
            if (list.Exists(r => string.IsNullOrEmpty(r.LogUuid)))
                return false;
            var svc = new InventoryData(dbFactory);
            var batchNum = svc.GetInventoryLogBatchNum();

            list.ForEach(x =>
            {
                x.BatchNum = batchNum;
                var inventory = svc.GetInventoryBySku(x.DatabaseNum.Value, x.MasterAccountNum.Value, x.ProfileNum.Value, x.SKU);
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
            var newdto = new InventoryDataDto() { InventoryLog = list };

            var data= FromDto(newdto);
            return svc.SaveInventoryLog(data.InventoryLog);
            //TODO:Validator验证，Mapper,补充完整Inventory信息进去，调用Dao


        }

        public bool UpdateInventoryLogList(List<InventoryLogDto> list)
        {
            if (list == null || list.Count() == 0)
                return false;
            var newdto = new InventoryDataDto() { InventoryLog = list };
            var logUuidList = list.Select(r => r.LogUuid).Distinct();

            var svc = new InventoryData(dbFactory);
            var dlist = new List<InventoryLog>();
            foreach (var uuid in logUuidList)
            {
                dlist.AddRange(svc.GetInventoryLogByLogUuid(uuid));
            }
            var oridata = new InventoryData() { InventoryLog = dlist };
            var data = DtoMapper.ReadDto(oridata,newdto);
            return svc.SaveInventoryLog(data.InventoryLog);
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryDataDto dto)
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
        public virtual async Task<bool> AddAsync(InventoryDataDto dto)
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
        public virtual bool Update(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic || dto.ProductBasic.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.ProductBasic.RowNum.ToLong());
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
        public virtual async Task<bool> UpdateAsync(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic || dto.ProductBasic.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

    }
}



