


//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Bogus;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.Extensions.Configuration;

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    public partial class InvoiceDataTests
    {
        private static int _itemCount = 10;
        public static int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }


        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public static async Task<InvoiceData> GetFakerInvoiceDataAsync(IDataBaseFactory dbFactory)
        {
            var data = GetFakerData();
            data.InvoiceHeader.MasterAccountNum = MasterAccountNum;
            data.InvoiceHeader.ProfileNum = ProfileNum;

            var inventories = GetInventories(dbFactory, data.InvoiceItems.Count);
            for (int i = 0; i < data.InvoiceItems.Count; i++)
            {
                var item = data.InvoiceItems[i];

                item.DiscountAmount = 0;
                item.ShipQty = new Random().Next(10, 100);

                var inventory = inventories[i];
                item.WarehouseCode = inventory.WarehouseCode;
                item.SKU = inventory.SKU;
                item.InventoryUuid = inventory.InventoryUuid;
                item.ProductUuid = inventory.ProductUuid;
                item.WarehouseUuid = inventory.WarehouseUuid;
            }
            data.InvoiceHeader.InvoiceNumber = NumberGenerate.Generate();
            return data;
        }

        public static async Task<InvoiceData> SaveFakerInvoiceAsync(IDataBaseFactory dbFactory, InvoiceData data = null)
        {
            var srv = new InvoiceService(dbFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            if (data == null)
                data = await GetFakerInvoiceDataAsync(dbFactory);
            var dto = mapper.WriteDto(data, null);
            var success = srv.Add(dto);

            Assert.True(success, srv.Messages.ObjectToString());

            return srv.Data;
        }

        /// <summary>
        /// get specified count Inventorys
        /// </summary>
        /// <param name="dbFactory"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Inventory[] GetInventories(IDataBaseFactory dbFactory, int count = 10)
        {
            var sql = $@" 
select  WarehouseCode,SKU,ProductUuid,InventoryUuid,WarehouseUuid 
from 
(
    SELECT  WarehouseCode,SKU,ProductUuid,InventoryUuid,WarehouseUuid
    ,ROW_NUMBER() over(partition by SKU order by rownum desc) as rid
    FROM [dbo].[Inventory]
    where MasterAccountNum = {MasterAccountNum} and ProfileNum = {ProfileNum}
) tmp 
where tmp.rid=1
";
            var inventories = dbFactory.Find<Inventory>(sql).ToArray();
            var success = inventories != null && inventories.Length >= count;
            Assert.True(success, "Inventory is not enough");

            var randomIndexs = GetRandomIndex(inventories.Length - 1, count);
            var result = new List<Inventory>();
            foreach (var index in randomIndexs)
            {
                result.Add(inventories[index]);
            }
            return result.ToArray();
        }

        private static List<int> GetRandomIndex(int max, int count = 10)
        {
            var result = new List<int>();
            while (result.Count < count)
            {
                var random = new Random().Next(0, max);
                while (!result.Contains(random))
                {
                    result.Add(random);
                }
            }
            return result;
        }
        //public async static Inventory[] GetInventories(IDataBaseFactory dbFactory)
        //{
        //    var svc = new InventoryService(dbFactory);
        //    var payload = new InventoryPayload()
        //    {
        //        MasterAccountNum = MasterAccountNum,
        //        ProfileNum = ProfileNum,
        //        Skus = new List<string>()
        //        { 
        //            //TODO read skus from db.
        //            // set skus in your db
        //            "Bacon","Bike","Car","Chair","Cheese","Fish","Hat","ProductExt-NEW-0908-200018546","Salad","Shoes","Towels",
        //        },

        //    };
        //    payload = await svc.GetInventoryBySkuArrayAsync(payload);
        //    return payload.Inventorys;
        //}
    }
}



