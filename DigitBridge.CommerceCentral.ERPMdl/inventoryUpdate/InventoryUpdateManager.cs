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
using DigitBridge.CommerceCentral.ERPDb.inventorySync.dto;
using DigitBridge.CommerceCentral.ERPDb.inventorySync;
using DigitBridge.Base.Utility.Model;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a InventoryUpdateService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InventoryUpdateManager :  IInventoryUpdateManager , IMessage
    {

        public InventoryUpdateManager() : base() {}

        public InventoryUpdateManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateService _inventoryUpdateService;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateService inventoryUpdateService
        {
            get
            {
                if (_inventoryUpdateService is null)
                    _inventoryUpdateService = new InventoryUpdateService(dbFactory);
                return _inventoryUpdateService;
            }
        }




        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateDataDtoCsv _inventoryUpdateDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateDataDtoCsv inventoryUpdateDataDtoCsv
        {
            get
            {
                if (_inventoryUpdateDataDtoCsv is null)
                    _inventoryUpdateDataDtoCsv = new InventoryUpdateDataDtoCsv();
                return _inventoryUpdateDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateList _inventoryUpdateList;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateList inventoryUpdateList
        {
            get
            {
                if (_inventoryUpdateList is null)
                    _inventoryUpdateList = new InventoryUpdateList(dbFactory);
                return _inventoryUpdateList;
            }
        }



        //

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


        public async Task<byte[]> ExportAsync(InventoryUpdatePayload payload)
        {
            var rowNumList =await inventoryUpdateList.GetRowNumListAsync(payload);
            var dtoList = new List<InventoryUpdateDataDto>();
           foreach(var x in rowNumList)
            {
                if (inventoryUpdateService.GetData(x))
                    dtoList.Add(inventoryUpdateService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryUpdateDataDto());
            return inventoryUpdateDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InventoryUpdatePayload payload)
        {
            var rowNumList =inventoryUpdateList.GetRowNumList(payload);
            var dtoList = new List<InventoryUpdateDataDto>();
            foreach (var x in rowNumList)
            {
                if (inventoryUpdateService.GetData(x))
                    dtoList.Add(inventoryUpdateService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryUpdateDataDto());
            return inventoryUpdateDataDtoCsv.Export(dtoList);
        }

        public void Import(InventoryUpdatePayload payload, IFormFileCollection files)
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
                var tlist = inventoryUpdateDataDtoCsv.Import(file.OpenReadStream());
                var list = new List<InventoryUpdateDataDto>();
                foreach (var item in tlist)
                {
                    if (!item.HasInventoryUpdateHeader)
                    {
                        if (item.HasInventoryUpdateItems)
                        {
                            foreach (var uitem in item.InventoryUpdateItems)
                            {
                                var data = new InventoryUpdateDataDto()
                                {
                                    InventoryUpdateHeader = new InventoryUpdateHeaderDto
                                    {
                                        WarehouseCode = uitem.WarehouseCode,
                                        WarehouseUuid = uitem.WarehouseUuid,
                                        DatabaseNum = payload.DatabaseNum,
                                        ProfileNum = payload.ProfileNum,
                                        MasterAccountNum = payload.MasterAccountNum,
                                        InventoryUpdateType = (int)payload.InventoryUpdateType,
                                    },
                                    InventoryUpdateItems = new List<InventoryUpdateItemsDto>() { uitem }
                                };
                                list.Add(data);
                            }
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.InventoryUpdate = item;
                    if (inventoryUpdateService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryUpdateService.Messages)
                            Messages.Add(msg);
                        inventoryUpdateService.Messages.Clear();
                    }
                }
                if (payload.HasInventoryUpdate)
                    payload.InventoryUpdate = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(InventoryUpdatePayload payload, IFormFileCollection files)
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
                var tlist =inventoryUpdateDataDtoCsv.Import(file.OpenReadStream());
                var list = new List<InventoryUpdateDataDto>();
                foreach(var item in tlist)
                {
                    if (!item.HasInventoryUpdateHeader)
                    {
                        if (item.HasInventoryUpdateItems)
                        {
                            foreach(var uitem in item.InventoryUpdateItems)
                            {
                                var data = new InventoryUpdateDataDto()
                                {
                                    InventoryUpdateHeader=new InventoryUpdateHeaderDto
                                    {
                                        WarehouseCode=uitem.WarehouseCode,
                                        WarehouseUuid=uitem.WarehouseUuid,
                                        DatabaseNum=payload.DatabaseNum,
                                        ProfileNum=payload.ProfileNum,
                                        MasterAccountNum=payload.MasterAccountNum,
                                        InventoryUpdateType= (int)payload.InventoryUpdateType,
                                    },
                                    InventoryUpdateItems = new List<InventoryUpdateItemsDto>() { uitem }
                                };
                                list.Add(data);
                            }
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.InventoryUpdate = item;
                    if (await inventoryUpdateService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryUpdateService.Messages)
                            Messages.Add(msg);
                        inventoryUpdateService.Messages.Clear();
                    }
                }
                if (payload.HasInventoryUpdate)
                    payload.InventoryUpdate = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }



        public async Task<bool> UpdateStockByList(InventorySyncUpdatePayload inventorySyncUpdatePayload)
        {
            List<StringArray> updateSkuArrays = new List<StringArray>();
            if (!inventorySyncUpdatePayload.HasInventorySyncData || !inventorySyncUpdatePayload.InventorySyncData.HasInventorySyncItems)
            {
                AddError("no data sync");
                return false;
            }

            var inventorySyncItems = inventorySyncUpdatePayload.InventorySyncData.InventorySyncItems;

            foreach (var item in inventorySyncItems)
                updateSkuArrays.Add(new StringArray() { Item0 = item.SKU, Item1 = item.WarehouseCode,Item2=item.Qty.ToString() });

            var inventoryList = new List<StringArray>();
            var productList = new List<StringArray>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                productList = await InventoryServiceHelper.GetProductBySkuAsync(updateSkuArrays, inventorySyncUpdatePayload.MasterAccountNum, inventorySyncUpdatePayload.ProfileNum);

            }
            using (var trx = new ScopedTransaction(dbFactory))
            {
                inventoryList = await InventoryServiceHelper.GetInventoryInfoBySkuWithWarehouseCodesAsync(updateSkuArrays, inventorySyncUpdatePayload.MasterAccountNum, inventorySyncUpdatePayload.ProfileNum);
            }

            #region check product sku is exists

            var notExistSku = (from u in updateSkuArrays
                               join p in productList
                               on u.Item0 equals p.Item0
                               into rr
                               where !rr.Any()
                               select u).ToList();

            if (notExistSku.Count != 0)
            {
                notExistSku.ToList().ForEach(
                  r =>
                     AddError($"sku { r.Item0 } not fund")
                    );
                return false;
            }

            #endregion

            #region if SKU inventory does not exist inventory will be added


            var notExistinventorySku = (from u in updateSkuArrays
                                        join i in inventoryList
                                        on u.Item0 equals i.Item0
                                        into rr
                                        where !rr.Any()
                                        select u).ToList();

            foreach (var item in notExistinventorySku)
            {

                var inventoryData = new InventoryDataDto()
                {
                    Inventory = new List<InventoryDto>() { new InventoryDto() { SKU = item.Item0, WarehouseCode = item.Item1, Instock = decimal.Parse(item.Item2),
                        ProductUuid = productList.Find(r=>r.Item0==item.Item0).Item1,
                        MasterAccountNum = inventorySyncUpdatePayload.MasterAccountNum, ProfileNum = inventorySyncUpdatePayload.ProfileNum,
 
                    
                    } }
                };
                inventoryData.ProductBasic = new ProductBasicDto() { SKU = item.Item0 };
                inventoryData.ProductExt = new ProductExtDto() { SKU=item.Item0 };
                if (!await inventoryService.AddInventoryAsync(inventoryData))
                {
                    AddError($"sync data faild");
                    return false;
                }
            };

          
 
            #endregion

 
            var inventoryUpdateDatalist = new List<InventoryUpdateDataDto>();
            
            foreach (var item in inventorySyncItems)
            {
                var existInventory = inventoryList.Find(r => r.Item0 == item.SKU && r.Item1 == item.WarehouseCode);
                if (existInventory == null) continue;

                InventoryUpdateItemsDto InventoryUpdateItems = new InventoryUpdateItemsDto()
                {  
                    WarehouseCode = item.WarehouseCode,
                    SKU = item.SKU,
                    WarehouseUuid = existInventory.Item2,
                    UpdateQty = item.Qty- decimal.Parse( existInventory.Item5),
                    InventoryUuid = existInventory.Item4,
                    ProductUuid= existInventory.Item3,
 
                };
                var data = new InventoryUpdateDataDto()
                {

                    InventoryUpdateHeader = new InventoryUpdateHeaderDto
                    {
                        WarehouseCode = item.WarehouseCode,
                        WarehouseUuid = existInventory.Item2,
                        DatabaseNum = inventorySyncUpdatePayload.DatabaseNum,
                        ProfileNum = inventorySyncUpdatePayload.ProfileNum,
                        MasterAccountNum = inventorySyncUpdatePayload.MasterAccountNum,
                        InventoryUpdateType = (int)InventoryUpdateType.Adjust,
                    },
                    InventoryUpdateItems = new List<InventoryUpdateItemsDto>() { InventoryUpdateItems }
                };
                inventoryUpdateDatalist.Add(data);
            }


            var payload = new InventoryUpdatePayload();
            payload.InventoryUpdateType = InventoryUpdateType.Adjust;
            payload.MasterAccountNum = inventorySyncUpdatePayload.MasterAccountNum;
            payload.ProfileNum = inventorySyncUpdatePayload.ProfileNum;
            foreach (var item in inventoryUpdateDatalist)
            {
       
                payload.InventoryUpdate = item;
                if (!await inventoryUpdateService.AddAsync(payload))
                {
                    AddError($"sync data faild");
                    return false;
                }
            }

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
