


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

            var svc = new InventoryService(dbFactory);
            var payload = new InventoryPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                Skus = new List<string>()
                { 
                    //TODO read skus from db.
                    // set skus in your db
                    "Bacon","Bike","Car","Chair","Cheese","Fish","Hat","ProductExt-NEW-0908-200018546","Salad","Shoes","Towels",
                },

            };
            payload = await svc.GetInventoryBySkuArrayAsync(payload);
            var list = payload.Inventorys;
            for (int i = 0; i < data.InvoiceItems.Count; i++)
            {
                var item = data.InvoiceItems[i];

                item.DiscountAmount = 0;
                item.ShipQty = 10;

                if (list != null && list.Count > i)
                {
                    var inventory = list[i].Inventory[0];
                    item.WarehouseCode = inventory.WarehouseCode;
                    item.SKU = inventory.SKU;
                    item.InventoryUuid = inventory.InventoryUuid;
                }
            }
            data.InvoiceHeader.InvoiceNumber = NumberGenerate.Generate();
            return data;
        }

        public static async Task<InvoiceData> SaveFakerInvoice(IDataBaseFactory dbFactory, InvoiceData data = null)
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
    }
}



