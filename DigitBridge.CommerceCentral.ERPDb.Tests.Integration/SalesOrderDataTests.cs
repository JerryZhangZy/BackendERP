


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

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    public partial class SalesOrderDataTests
    {
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetByOrderNumber_Test()
        {
            Save_Test();

            var data2 = DataBaseFactory.GetBy<SalesOrderHeader>(@"
SELECT TOP 1 ins.OrderNumber, ins.MasterAccountNum, ins.ProfileNum
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");


            var data = new SalesOrderData(DataBaseFactory);
            var rowNum = data.GetRowNum(data2.OrderNumber, data2.MasterAccountNum, data2.ProfileNum);
            data.Get(rowNum.ToLong());
            var result = data.SalesOrderHeader.OrderNumber.Equals(data2.OrderNumber);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByOrderNumberAsync_Test()
        {
            await SaveAsync_Test();

            var data2 = await DataBaseFactory.GetByAsync<SalesOrderHeader>(@"
SELECT TOP 1 ins.OrderNumber, ins.MasterAccountNum, ins.ProfileNum
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");

            var data = new SalesOrderData(DataBaseFactory);
            var rowNum = await data.GetRowNumAsync(data2.OrderNumber, data2.MasterAccountNum, data2.ProfileNum);
            await data.GetAsync(rowNum.ToLong());

            var result = data.SalesOrderHeader.OrderNumber.Equals(data2.OrderNumber);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        public static SalesOrderData GetSalesOrderFromDB(int masterAccountNum, int profileNum, IDataBaseFactory dbFactory)
        {
            var salesOrderUuid = dbFactory.GetValue<SalesOrderHeader, string>($@"
SELECT TOP 1 ins.SalesOrderUuid 
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0 and masterAccountNum={masterAccountNum} and profileNum={profileNum}
");
            var data = new SalesOrderData(dbFactory);
            var success = data.GetById(salesOrderUuid);
            Assert.True(success, "Data not found.");
            return data;
        }

        public static SalesOrderData GetFakerDataWithCountItem(int count)
        {
            var SalesOrderData = new SalesOrderData();
            SalesOrderData.SalesOrderHeader = SalesOrderHeaderTests.GetFakerData().Generate();
            SalesOrderData.SalesOrderHeaderInfo = SalesOrderHeaderInfoTests.GetFakerData().Generate();
            SalesOrderData.SalesOrderHeaderAttributes = SalesOrderHeaderAttributesTests.GetFakerData().Generate();
            SalesOrderData.SalesOrderItems = SalesOrderItemsTests.GetFakerData().Generate(count);
            foreach (var ln in SalesOrderData.SalesOrderItems)
                ln.SalesOrderItemsAttributes = SalesOrderItemsAttributesTests.GetFakerData().Generate();
            return SalesOrderData;
        }
    }
}



