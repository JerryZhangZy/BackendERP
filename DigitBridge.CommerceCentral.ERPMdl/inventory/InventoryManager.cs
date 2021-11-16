//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
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
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a InventoryService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InventoryManager :  IInventoryManager , IMessage
    {

        public InventoryManager() : base() {}

        public InventoryManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected InventoryService _inventoryService;
        [XmlIgnore, JsonIgnore]
        public InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryDataDtoCsv _inventoryDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public InventoryDataDtoCsv inventoryDataDtoCsv
        {
            get
            {
                if (_inventoryDataDtoCsv is null)
                    _inventoryDataDtoCsv = new InventoryDataDtoCsv();
                return _inventoryDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryList _inventoryList;
        [XmlIgnore, JsonIgnore]
        public InventoryList inventoryList
        {
            get
            {
                if (_inventoryList is null)
                    _inventoryList = new InventoryList(dbFactory);
                return _inventoryList;
            }
        }

        public async Task<byte[]> ExportAsync(InventoryPayload payload)
        {
            var rowNumList =await inventoryList.GetRowNumListAsync(payload);
            var dtoList = new List<InventoryDataDto>();
           foreach(var x in rowNumList)
            {
                if (inventoryService.GetData(x))
                    dtoList.Add(inventoryService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryDataDto());
            return inventoryDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InventoryPayload payload)
        {
            var rowNumList =inventoryList.GetRowNumList(payload);
            var dtoList = new List<InventoryDataDto>();
            foreach (var x in rowNumList)
            {
                if (inventoryService.GetData(x))
                    dtoList.Add(inventoryService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryDataDto());
            return inventoryDataDtoCsv.Export(dtoList);
        }

        public void Import(InventoryPayload payload, IFormFileCollection files)
        {
            if(files==null||files.Count==0)
            {
                AddError("no files upload");
                return;
            }
            foreach(var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = inventoryDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.Inventory = item;
                    if (inventoryService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryService.Messages)
                            Messages.Add(msg);
                        inventoryService.Messages.Clear();
                    }
                }
                if (payload.HasInventory)
                    payload.Inventory = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(InventoryPayload payload, IFormFileCollection files)
        {
            if(files==null||files.Count==0)
            {
                AddError("no files upload");
                return;
            }
            foreach(var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list =inventoryDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.Inventory = item;
                    if (await inventoryService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryService.Messages)
                            Messages.Add(msg);
                        inventoryService.Messages.Clear();
                    }
                }
                if (payload.HasInventory)
                    payload.Inventory = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }


        public async Task<bool> NewWarehouse(InventoryNewWarehousePayload payload)
        {

            string sql = $@"SELECT * FROM DistributionCenter WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND  DistributionCenterCode=@2";
           var distributionCenters= dbFactory.Db.Query<DistributionCenter>(sql,
                payload.MasterAccountNum.ToSqlParameter("@0"),
                payload.ProfileNum.ToSqlParameter("@1"),
                payload.DistributionCenterCode.ToSqlParameter("@2")
                ).ToList();
            if (distributionCenters == null || distributionCenters.Count() == 0) return false;


            #region  Add new warehouse to All SKU. sql
            sql = $@"INSERT INTO [dbo].[Inventory]
           (
		   [DatabaseNum]
           ,[MasterAccountNum]
           ,[ProfileNum]
           ,[ProductUuid]
           ,[InventoryUuid]
           ,[StyleCode]
           ,[ColorPatternCode]
           ,[SizeType]
           ,[SizeCode]
           ,[WidthCode]
           ,[LengthCode]
           ,[PriceRule]
           ,[LeadTimeDay]
           ,[PoSize]
           ,[MinStock]
           ,[SKU]
           ,[WarehouseUuid]
           ,[WarehouseCode]
           ,[WarehouseName]
           ,[LotNum]
           ,[LotInDate]
           ,[LotExpDate]
           ,[LotDescription]
           ,[LpnNum]
           ,[LpnDescription]
           ,[Notes]
           ,[Currency]
           ,[UOM]
           ,[QtyPerPallot]
           ,[QtyPerCase]
           ,[QtyPerBox]
           ,[PackType]
           ,[PackQty]
           ,[DefaultPackType]
           ,[Instock]
           ,[OnHand]
           ,[OpenSoQty]
           ,[OpenFulfillmentQty]
           ,[AvaQty]
           ,[OpenPoQty]
           ,[OpenInTransitQty]
           ,[OpenWipQty]
           ,[ProjectedQty]
           ,[BaseCost]
           ,[TaxRate]
           ,[TaxAmount]
           ,[ShippingAmount]
           ,[MiscAmount]
           ,[ChargeAndAllowanceAmount]
           ,[UnitCost]
           ,[AvgCost]
           ,[SalesCost]
           ,[UpdateDateUtc]
           ,[EnterBy]
           ,[UpdateBy]

		   )
SELECT
           prd.DatabaseNum
           ,prd.MasterAccountNum
           ,prd.ProfileNum
           ,prd.ProductUuid
           ,CAST(newid() AS NVARCHAR(50))
           ,prd.SKU
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,0
           ,100
           ,1000
           ,prd.SKU
           ,COALESCE(@0, '')  
           ,COALESCE(@1, '')
           ,COALESCE(@2, '') --<WarehouseName, nvarchar(200),>
           ,''
           ,null
           ,null
           ,''
           ,''
           ,''
           ,''
           ,''
           ,'EA'
           ,1
           ,1
           ,1
           ,1
           ,1
           ,0
           ,COALESCE(0,0)	--<Instock, decimal(24,6),>
           ,0
           ,0
           ,0
           ,COALESCE(0,0)
           ,0
           ,0
           ,0
           ,COALESCE(0,0)
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,GETDATE()
           ,'SYSTEM'
           ,'SYSTEM'
FROM ProductBasic prd
where prd.ProductUuid != '' AND prd.MasterAccountNum=@3 AND prd.ProfileNum=@4
AND NOT EXISTS (SELECT * FROM Inventory i WHERE i.ProductUuid = prd.ProductUuid)";
            #endregion

            if (await dbFactory.Db.ExecuteAsync(sql,
                   distributionCenters[0].DistributionCenterUuid.ToSqlParameter("DistributionCenterUuid"),
                     distributionCenters[0].DistributionCenterCode.ToSqlParameter("DistributionCenterCode"),
                     distributionCenters[0].DistributionCenterName.ToSqlParameter("DistributionCenterName"),
                     payload.MasterAccountNum.ToSqlParameter("MasterAccountNum"),
                     payload.ProfileNum.ToSqlParameter("ProfileNum")
                   ) == 0)
                return false;

            return true;
        }

        public async Task<bool> ExistSKU(ExistSKUPayload payload,string SKU)
        {
            string sql = $@"SELECT COUNT(*) FROM ProductExt WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND SKU=@2";
            if (await dbFactory.Db.ExecuteScalarAsync<int>(sql,
                   payload.MasterAccountNum.ToSqlParameter("MasterAccountNum"),
                   payload.ProfileNum.ToSqlParameter("ProfileNum"),
                   SKU.ToSqlParameter("SKU")
                   ) == 0)
                payload.IsExistSKU = false;
               else
                payload.IsExistSKU = true;

            return true;
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages
    }
}
