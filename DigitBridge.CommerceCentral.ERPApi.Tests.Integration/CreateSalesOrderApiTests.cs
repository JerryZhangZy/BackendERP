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
            string connectStr = ConfigHelper.GetValueByName("DBConnectionString");
            var dbFactory = new DataBaseFactory(connectStr);
            var uuids = new List<string> {
            "9E590AE7-F15E-42CD-A575-B101C39AC536",
            "C267FD30-2B58-4A32-B856-10B71300F928",
            "19E20861-0BFA-4235-8DA9-1B5F1FF055E6"
            };
            SalesOrderManager soManager = new SalesOrderManager(dbFactory);
            bool result = true;
            foreach (var uuid in uuids)
            {
                result = await soManager.CreateSalesOrderByChannelOrderIdAsync(uuid);

                Assert.True(result);

            }

        }


    }
}
