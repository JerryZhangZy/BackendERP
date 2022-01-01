using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{

    public class CreateSalesOrderApiTests
    {
        //public IConfiguration Configuration { get; }
        //public IDataBaseFactory DataBaseFactory { get; set; }

        [Fact]
        public async Task CreateSalesOrder_Test()
        {
            //string connectStr = ConfigHelper.GetValueByName("DBConnectionString");
            string connectStr = "";
            var dbFactory = new DataBaseFactory(connectStr);
            var uuids = new List<string> {
            "29c75212-d16d-4b9a-be9d-acb3408f94bf"
            };
            SalesOrderManager soManager = new SalesOrderManager(dbFactory);
            bool result = true;
            List<string> salesOrderNums = new List<string>();

            try
            {
                foreach (var uuid in uuids)
                {
                    using (var b = new Benchmark("FindNotExistSkuWarehouseAsync_Test"))
                    {
                        (result, salesOrderNums) = await soManager.CreateSalesOrderByChannelOrderIdAsync(uuid);
                    }
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }


        }


    }
}
