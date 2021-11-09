

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using System.Text;

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
            SetCalculator(new InventoryServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InventoryServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        public virtual bool GetDataBySku(string sku, int masterAccountNum, int profileNum)
        {
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = InventoryServiceHelper.GetRowNumBySku(sku, masterAccountNum, profileNum);
            }
            return GetData(rowNum);
        }

        public virtual async Task<bool> GetDataBySkuAsync(string sku, int masterAccountNum, int profileNum)
        {
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryServiceHelper.GetRowNumBySkuAsync(sku, masterAccountNum, profileNum);
            }
            return await GetDataAsync(rowNum);
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

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

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

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public virtual bool Add(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            // load data from dto
            FromDto(payload.Inventory);
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory || !payload.Inventory.HasProductExt)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Inventory)))
                return false;

            List();
            await GetDataBySkuAsync(payload.Inventory.ProductExt.SKU, payload.MasterAccountNum, payload.ProfileNum);
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);
            _ProcessMode = Base.Common.ProcessingMode.Add;

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddInventoryAsync(InventoryDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();
            
            
            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);

            Data.AddIgnoreSave(InventoryData.ProductBasicTable);
            Data.AddIgnoreSave(InventoryData.ProductExtTable);
            Data.AddIgnoreSave(InventoryData.ProductExtAttributesTable);
            Data.AddIgnoreSave(InventoryData.InventoryAttributesTable);
            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }


        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic)
                return false;

            //set edit mode before validate
            Edit();

            if (!Validate(dto))
                return false;

            // set Add mode and clear data
            if (dto.HasProductBasic)
            {
                Edit(dto.ProductBasic.RowNum.ToLong());
            }
            else
            {
                bool isValid = true;
                if (!dto.ProductExt.HasMasterAccountNum)
                {
                    AddError("Add ProductExt,MasterAccountNum must required");
                    isValid = false;
                }
                if (!dto.ProductExt.HasProfileNum)
                {
                    AddError("Add ProductExt,ProfileNum must required");
                    isValid = false;
                }
                if (!isValid)
                {
                    return false;
                }
                GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());
            }
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

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
            if (dto is null || !dto.HasProductBasic)
                return false;

            //set edit mode before validate
            Edit();

            if (!(await ValidateAsync(dto)))
                return false;

            if (dto.HasProductBasic)
            {
                await EditAsync(dto.ProductBasic.RowNum.ToLong());
            }
            else
            {
                bool isValid = true;
                if (!dto.ProductExt.HasMasterAccountNum)
                {
                    AddError("Add ProductExt,MasterAccountNum must required");
                    isValid = false;
                }
                if (!dto.ProductExt.HasProfileNum)
                {
                    AddError("Add ProductExt,ProfileNum must required");
                    isValid = false;
                }
                if (!isValid)
                {
                    return false;
                }
                await GetDataBySkuAsync(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());
            }
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);
            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory || payload.Inventory.ProductBasic.RowNum.ToLong() <= 0)
                return false;

            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            // set Add mode and clear data
            var dto = payload.Inventory;
            if (dto.HasProductBasic)
            {
                Edit(dto.ProductBasic.RowNum.ToLong());
            }
            else
            {
                GetDataBySku(dto.ProductExt.SKU, payload.MasterAccountNum, payload.ProfileNum);
            }
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory)
                return false;

            //set edit mode before validate
            Edit();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Inventory)))
                return false;

            // set Add mode and clear data
            var dto = payload.Inventory;
            if (dto.HasProductBasic)
            {
                await EditAsync(dto.ProductBasic.RowNum.ToLong());
            }
            else
            {
                await GetDataBySkuAsync(dto.ProductExt.SKU, payload.MasterAccountNum, payload.ProfileNum);
            }
            Data.AddIgnoreSave(InventoryData.ProductBasicTable);

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public async Task<bool> DeleteBySkuAsync(InventoryPayload payload, string sku)
        {
            if (string.IsNullOrEmpty(sku))
                return false;
            Delete();
            if (!(await ValidateAccountAsync(payload, sku)))
                return false;
            long rowNum = 0;
            Data.AddIgnoreDelete(InventoryData.ProductBasicTable);
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryServiceHelper.GetRowNumBySkuAsync(sku, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            return success && (await DeleteDataAsync());
        }

        public InventoryPayload GetInventoryBySkuArray(InventoryPayload payload)
        {
            if (!payload.HasSkus)
                return payload;
            var list = new List<InventoryDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = InventoryServiceHelper.GetRowNumsBySkus(payload.Skus, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rownum in rowNumList)
            {
                if (GetData(rownum))
                    list.Add(ToDto());
            }
            payload.Inventorys = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<InventoryPayload> GetInventoryBySkuArrayAsync(InventoryPayload payload)
        {
            if (!payload.HasSkus)
                return payload;
            var list = new List<InventoryDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = await InventoryServiceHelper.GetRowNumsBySkusAsync(payload.Skus, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rownum in rowNumList)
            {
                if (await GetDataAsync(rownum))
                    list.Add(ToDto());
            }
            payload.Inventorys = list;
            payload.Messages = msglist;
            return payload;
        }

        public bool GetInventoryBySku(InventoryPayload payload, string sku)
        {
            return GetDataBySku(sku, payload.MasterAccountNum, payload.ProfileNum);
        }

        public async Task<bool> GetInventoryBySkuAsync(InventoryPayload payload, string sku)
        {
            return await GetDataBySkuAsync(sku, payload.MasterAccountNum, payload.ProfileNum);
        }

        public List<Inventory> GetInventoriesBySkus(IList<string> skus,string warehouseCode)
        {
            return dbFactory.Find<Inventory>("WHERE WarehouseCode=@0 AND (EXISTS (SELECT * FROM @1 _SKU WHERE _SKU.item = COALESCE([SKU],'')))", 
                warehouseCode.ToSqlParameter("WarehouseCode"), skus.ToParameter<string>("SKU")).ToList();
        }
        public Inventory GetInventoryBySku(string sku,string warehouseCode)
        {
            return dbFactory.Find<Inventory>("WHERE WarehouseCode=@0 AND SKU=@1", 
                warehouseCode.ToSqlParameter("WarehouseCode"), sku.ToSqlParameter("SKU")).FirstOrDefault();
        }
        
        public Inventory GetInventoryBySkuWithWarehouseUuid(string sku,string warehouseUuid)
        {
            return dbFactory.Find<Inventory>("WHERE WarehouseUuid=@0 AND SKU=@1", 
                warehouseUuid.ToSqlParameter("WarehouseUuid"), sku.ToSqlParameter("SKU")).FirstOrDefault();
        }
        
        public Inventory GetInventoryByInventoryUuid(string inventoryUuid)
        {
            return dbFactory.Find<Inventory>("WHERE InventoryUuid=@0", inventoryUuid.ToSqlParameter("InventoryUuid")).FirstOrDefault();
        }

        public Inventory GetInventory(InventoryData inventoryData, dynamic sourceData, SKUType skuType = SKUType.GeneralMerchandise)
        {
            var inventory = new Inventory();
            if (inventoryData == null || inventoryData.Inventory == null || inventoryData.Inventory.Count == 0) return inventory;
            //TODO Add this logic.
            if (sourceData.InventoryUuid != null)
            {
                inventory = inventoryData.Inventory.Where(i => i.InventoryUuid == sourceData.InventoryUuid).FirstOrDefault();
            }
            else
            {
                switch (skuType)
                {
                    //TODO get inventory by sku type.
                    //case SKUType.ApparelAndShoes: inventory=  inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode &&i.ColorPatternCode==sourceData.ColorPatternCode).FirstOrDefault();break;
                    //case SKUType.FoodAndVitamin: inventory=  inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode).FirstOrDefault();break;
                    //case SKUType.ElectronicAndComputer: inventory=  inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode).FirstOrDefault();break;
                    //case SKUType.Application: inventory=  inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode).FirstOrDefault();break;
                    //case SKUType.Furniture: inventory=  inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode).FirstOrDefault();break;
                    default: inventory = inventoryData.Inventory.Where(i => i.SKU == sourceData.SKU && i.WarehouseCode == sourceData.WarehouseCode).FirstOrDefault(); break;
                }
            }
            if (inventory == null)
                inventory = new Inventory();
            return inventory;
        }

        public bool SyncInventoryAvQtyToProductDistributionCenterQuantity(InventoryPayload payload)
        {
            try
            {
                using (var trx = new ScopedTransaction(dbFactory))
                {
                    payload.SyncInventoryAvQtyCount = InventoryServiceHelper.SyncInventoryAvQtyToProductDistributionCenterQuantity(payload);
                }
                return true;
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncInventoryAvQtyToProductDistributionCenterQuantityAsync(InventoryPayload payload)
        {
            try
            {
                using (var trx = new ScopedTransaction(dbFactory))
                {
                    payload.SyncInventoryAvQtyCount = await InventoryServiceHelper.SyncInventoryAvQtyToProductDistributionCenterQuantityAsync(payload);
                }

                return true;
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatAvgCostByPoReceiveAsync(PoTransactionData data)
        {
            if (data == null || data.PoTransaction == null||!data.FirstAPReceiveStatus)
                return false;
            var header = data.PoTransaction;
            if (data.PoTransactionItems == null || data.PoTransactionItems.Count == 0)
            {
                return true;
            }
            foreach (var items in data.PoTransactionItems)
            {
                var inventory = GetInventoryByInventoryUuid(items.InventoryUuid);
                if(inventory==null)
                    continue;
                var itemCost = new ItemCostClass(inventory);
                var cost = itemCost.CalculateAvgCost(new ItemCostClass(items));
                await UpdateAvgCostAsync(cost);
            }

            return true;
        }
        
        public bool UpdatAvgCostByPoReceive(PoTransactionData data)
        {
            if (data == null || data.PoTransaction == null||!data.FirstAPReceiveStatus)
                return false;
            var header = data.PoTransaction;
            if (data.PoTransactionItems == null || data.PoTransactionItems.Count == 0)
            {
                return true;
            }
            foreach (var items in data.PoTransactionItems)
            {
                var inventory = GetInventoryByInventoryUuid(items.InventoryUuid);
                if(inventory==null)
                    continue;
                var itemCost = new ItemCostClass(inventory);
                var cost = itemCost.CalculateAvgCost(new ItemCostClass(items));
                UpdateAvgCost(cost);
            }

            return true;
        }
        
        private void UpdateAvgCost(string inventoryUuid,decimal avgCost,decimal baseCost)
        {
            dbFactory.Db.Execute("UPDATE Inventory SET AvgCost=@0 AND BaseCost=@1 WHERE InventoryUuid = @2", avgCost.ToSqlParameter("AvgCost"),baseCost.ToSqlParameter("BaseCost"),inventoryUuid.ToSqlParameter("inventoryUuid"));
        }

        private async Task UpdateAvgCostAsync(string inventoryUuid,decimal avgCost,decimal baseCost)
        {
            await dbFactory.Db.ExecuteAsync("UPDATE Inventory SET AvgCost=@0 AND BaseCost=@1 WHERE InventoryUuid = @2", avgCost.ToSqlParameter("AvgCost"),baseCost.ToSqlParameter("BaseCost"),inventoryUuid.ToSqlParameter("inventoryUuid"));
        }
        
        private void UpdateAvgCost(ItemCostClass cost)
        {
            dbFactory.Db.Execute("UPDATE Inventory SET AvgCost=@0 AND BaseCost=@1 WHERE InventoryUuid = @2", cost.AvgCost.ToSqlParameter("AvgCost"),cost.BaseCost.ToSqlParameter("BaseCost"),cost.InventoryUuid.ToSqlParameter("inventoryUuid"));
        }

        private async Task UpdateAvgCostAsync(ItemCostClass cost)
        {
            await dbFactory.Db.ExecuteAsync("UPDATE Inventory SET AvgCost=@0 AND BaseCost=@1 WHERE InventoryUuid = @2", cost.AvgCost.ToSqlParameter("AvgCost"),cost.BaseCost.ToSqlParameter("BaseCost"),cost.AvgCost.ToSqlParameter("inventoryUuid"));
        }

        public void UpdateOpenSoQtyFromSalesOrder(Dictionary<string, decimal> data)
        {
            StringBuilder cmd = new StringBuilder();
            foreach (var item in data)
            {
                cmd.AppendLine($"UPDATE Inventory SET OpenSoQty=OpenSoQty+{item.Value} WHERE InventoryUuid='{item.Key}'");
                cmd.AppendLine("GO");
            }
            dbFactory.Db.Execute(cmd.ToString());
        }
    }
}



