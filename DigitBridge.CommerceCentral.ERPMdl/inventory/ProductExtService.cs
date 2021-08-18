
    
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

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ProductExtService : InventoryService
    {

        public ProductExtService() : base() { }
        public ProductExtService(IDataBaseFactory dbFactory) : base(dbFactory) { }
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryService Init()
        {
            SetDtoMapper(new InventoryDataDtoMapperDefault());
            SetCalculator(new InventoryServiceCalculatorDefault());
            AddValidator(new ProductExtServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        public virtual void GetDataBySku(string sku,int masterAccountNum,int profileNum)
        {
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = InventoryServiceHelper.GetRowNumBySku(sku, masterAccountNum, profileNum);
            }
            GetData(rowNum);
        }

        public virtual async Task GetDataBySkuAsync(string sku,int masterAccountNum,int profileNum)
        {
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum =await InventoryServiceHelper.GetRowNumBySkuAsync(sku, masterAccountNum, profileNum);
            }
            await GetDataAsync(rowNum);
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductExt)
                return false;
            // set Add mode and clear data
            Edit();

            if (!Validate(dto))
                return false;

            GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

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
            if (dto is null || !dto.HasProductExt)
                return false;
            // set Add mode and clear data
            Edit();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            await GetDataBySkuAsync(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(InventoryPayload payload)
        {
            if (payload is null ||!payload.Inventory!.HasProductExt)
                return false;

            // set Add mode and clear data
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            var dto = payload.Inventory;

            GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InventoryPayload payload)
        {
            if (payload is null || !payload.Inventory!.HasProductExt)
                return false;

            // set Add mode and clear data
            Edit();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Inventory).ConfigureAwait(false)))
                return false;

            var dto = payload.Inventory;

            await GetDataBySkuAsync(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

            // load data from dto
            FromDto(payload.Inventory);

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
            if (dto is null || !dto.HasProductExt)
                return false;

            //set edit mode before validate
            Edit();

            if (!Validate(dto))
                return false;

            // set Add mode and clear data
            GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

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
            if (dto is null || !dto.HasProductExt)
                return false;

            //set edit mode before validate
            Edit();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data
            await GetDataBySkuAsync(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryPayload payload)
        {
            if (payload is null || !payload.Inventory!.HasProductExt)
                return false;

            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            // set Add mode and clear data

            var dto = payload.Inventory;

            GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

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
            if (payload is null || !payload!.Inventory.HasProductExt)
                return false;

            //set edit mode before validate
            Edit();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Inventory).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data

            var dto = payload.Inventory;

            GetDataBySku(dto.ProductExt.SKU, dto.ProductExt.MasterAccountNum.ToInt(), dto.ProductExt.ProfileNum.ToInt());

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        public override bool SaveData()
        {
            if (ProcessMode != ProcessingMode.Add && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;

            Calculate();

            #region override data.save()
            try
            {
                //dbFactory.Begin();
                if (_data.ProductExtAttributes == null)
                    _data.ProductExtAttributes = GenerateNewProductExtAttributes(_data.ProductExt);
                //Supply all inventory
                _data.Inventory = SupplyMissingInventories(_data.ProductExt, _data.Inventory);
                _data.CheckIntegrity();

                //save ProductExt
                _data.ProductExt.SetDataBaseFactory(dbFactory)?.Save();

                //save ProductExtAttributes

                _data.ProductExtAttributes.SetDataBaseFactory(dbFactory)?.Save();

                //save inventory
                 _data.Inventory.SetDataBaseFactory(dbFactory)?.Save();

            }catch(Exception)
            {
                throw;
            }
            finally
            {
                dbFactory.Abort();
            }
            #endregion
            return true;
        }

        public override async Task<bool> SaveDataAsync()
        {
            if (ProcessMode != ProcessingMode.Add && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;

            Calculate();

            #region override data.save()
            try
            {
                //dbFactory.Begin();
                if (_data.ProductExtAttributes == null)
                    _data.ProductExtAttributes = GenerateNewProductExtAttributes(_data.ProductExt);
                //Supply all inventory
                _data.Inventory = SupplyMissingInventories(_data.ProductExt, _data.Inventory);
                _data.CheckIntegrity();

                //save ProductExt
                await _data.ProductExt.SetDataBaseFactory(dbFactory).SaveAsync();

                //save ProductExtAttributes
                await _data.ProductExtAttributes.SetDataBaseFactory(dbFactory).SaveAsync();

                //save inventory
                await _data.Inventory.SetDataBaseFactory(dbFactory).SaveAsync();

                //dbFactory.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbFactory.Abort();
            }
            #endregion
            return true;
        }

        protected IList<Inventory> SupplyMissingInventories(ProductExt product, IList<Inventory> inventories)
        {
            var warehouseList = new List<DistributionCenter>();
            using (var tx = new ScopedTransaction(dbFactory))
            {
                warehouseList.AddRange(WarehouseServiceHelper.GetWarehouses(product.MasterAccountNum, product.ProfileNum));
            }
            if (inventories == null)
                inventories = new List<Inventory>();
            var twarehouseCode = inventories.Select(r => r.WarehouseCode).Distinct().ToHashSet();
            var missingWarehouse = warehouseList.Where(r => !twarehouseCode.Contains(r.DistributionCenterCode)).ToList();
            missingWarehouse.ForEach(x =>
            {
                inventories.Add(GenerateNewInventory(product, x));
            });
            return inventories;
        }

        protected ProductExtAttributes GenerateNewProductExtAttributes(ProductExt ext)
        {
            return new ProductExtAttributes()
            {
                ProductUuid = ext.ProductUuid
            };
        }

        protected Inventory GenerateNewInventory(ProductExt ext, DistributionCenter warehouse)
        {
            return new Inventory()
            {
                DatabaseNum = ext.DatabaseNum,
                MasterAccountNum = ext.MasterAccountNum,
                ProfileNum = ext.ProfileNum,
                ProductUuid = ext.ProductUuid,
                InventoryUuid = Guid.NewGuid().ToString(),
                SKU = ext.SKU,

                StyleCode = ext.StyleCode,
                ColorPatternCode = ext.ColorPatternCode,
                SizeType = ext.SizeType,
                SizeCode = ext.SizeCode,
                WidthCode = ext.WidthCode,
                LengthCode = ext.LengthCode,
                PriceRule = ext.PriceRule,
                LeadTimeDay = ext.LeadTimeDay,
                PoSize = ext.PoSize,
                MinStock = ext.MinStock,

                WarehouseCode = warehouse.DistributionCenterCode,
                WarehouseName = warehouse.DistributionCenterName,
                WarehouseUuid = warehouse.DistributionCenterUuid,

                Currency = ext.Currency,
                UOM = ext.UOM,
                QtyPerPallot = ext.QtyPerPallot,
                QtyPerCase = ext.QtyPerCase,
                QtyPerBox = ext.QtyPerBox,
                PackType = ext.PackType,
                PackQty = ext.PackQty,
                DefaultPackType = ext.DefaultPackType,

                SalesCost = ext.SalesCost
            };
        }
    }
}