
    

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
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class CustomerListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public CustomerListTests(TestFixture<StartupTest> fixture) 
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
                var Seq = 0;
                dataBaseFactory = DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }

        public CustomerData Save_Test(string prefix="")
        {
            var data = CustomerDataTests.GetFakerData();

            data.Customer.MasterAccountNum = 10001;
            data.Customer.ProfileNum = 10001;
            data.Customer.CustomerCode = $"{prefix}{data.Customer.CustomerCode}";
            data.Customer.CustomerName = $"{prefix}{data.Customer.CustomerName}";
            data.Customer.Contact = $"{prefix}{data.Customer.Contact}";
            data.Customer.Phone1 = $"{prefix}{data.Customer.Phone1}";
            data.Customer.Email = $"{prefix}{data.Customer.Email}";
            //data.Customer.CustomerType = $"{prefix}{data.Customer.CustomerType}";
            //data.Customer.CustomerStatus = $"{prefix}{data.Customer.CustomerCode}";
            //data.Customer.BusinessType = $"{prefix}{data.Customer.CustomerCode}";
            //data.Customer.FirstDate = $"{prefix}{data.Customer.CustomerCode}";
            data.Customer.Priority = $"{prefix}{data.Customer.Priority}";
            data.Customer.Area = $"{prefix}{data.Customer.Area}";
            data.Customer.Region = $"{prefix}{data.Customer.Region}";
            data.Customer.Districtn = $"{prefix}{data.Customer.Districtn}";
            data.Customer.Zone = $"{prefix}{data.Customer.Zone}";
            data.Customer.ClassCode = $"{prefix}{data.Customer.ClassCode}";
            data.Customer.DepartmentCode = $"{prefix}{data.Customer.DepartmentCode}";
            data.Customer.DivisionCode = $"{prefix}{data.Customer.DivisionCode}";
            data.Customer.SourceCode = $"{prefix}{data.Customer.SourceCode}";

            data.SetDataBaseFactory(dataBaseFactory);
            data.Save();
            var dataGet = new CustomerData(dataBaseFactory);
            dataGet.GetById(data.UniqueId);
            return dataGet;
        }

        private JObject GetFilters(CustomerData data)
        {
            return new JObject()
            {
                { "customerUuid",data.Customer.CustomerUuid },
                { "customerCode",data.Customer.CustomerCode },
                { "customerName",data.Customer.CustomerName },
                { "contact",data.Customer.Contact},
                { "phone1",data.Customer.Phone1 },
                { "email",data.Customer.Email},
                { "webSite",data.Customer.WebSite },
                { "customerType",data.Customer.CustomerType },
                { "customerStatus",data.Customer.CustomerStatus },
                { "businessType",data.Customer.BusinessType },
                { "firstDate",data.Customer.FirstDate.ToString("yyyy-MM-dd") },
                { "priority",data.Customer.Priority },
                { "area",data.Customer.Area},
                { "region",data.Customer.Region },
                { "districtn",data.Customer.Districtn },
                { "zone",data.Customer.Zone },
                { "classCode",data.Customer.ClassCode },
                { "departmentCode",data.Customer.DepartmentCode },
                { "divisionCode",data.Customer.DivisionCode },
                { "sourceCode",data.Customer.SourceCode },
            };
        }

        #region sync methods

        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ExcuteJsonAsync_Test()
        {
            var payload = new CustomerPayload();
            payload.LoadAll = true;
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            var customer = Save_Test("JSON_ASYNC");
            var filters = GetFilters(customer);
            var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            foreach (var obj in filters)
            {
                payload.Filter = new JObject() { { obj.Key, obj.Value } };
                await srv.GetCustomerListAsync(payload);

                System.Diagnostics.Debug.WriteLine($"filter:{obj.Key},{obj.Value.ToString()}.Success:{payload.Success},List:{payload.CustomerListCount}");
                Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
                Assert.True(payload.CustomerList!=null&&payload.CustomerList.Length>0,obj.Key);
                Assert.True(payload.CustomerListCount>0, "This is a generated tester, please report any tester bug to team leader.");

            }

            //var qry = new CustomerQuery();
            //var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            //srv.LoadRequestParameter(payload);
            //qry.SetFilterValue("OrderDateFrom", DateTime.Today.AddDays(-30));
            //qry.OrderNumberFrom.FilterValue = "j5rjyh5s54kaoji12g9hynwn5f6y3hgn7ep61zw7oy60ilwb2p";
            //qry.OrderNumberTo.FilterValue = "j5rjyh5s54kaoji12g9hynwn5f6y3hgn7ep61zw7oy60ilwb2p";
            //qry.OrderStatus.MultipleFilterValueString = "11,18,86";

            //var totalRecords = 0;
            //var result = false;
            //StringBuilder sb = new StringBuilder();
            //try
            //{
            //    using (var b = new Benchmark("ExcuteJsonAsync_Test"))
            //    {
            //        payload.CustomerListCount = await srv.CountAsync();
            //        result = await srv.ExcuteJsonAsync(sb);
            //        if (result)
            //            payload.CustomerList = sb;

            //        //using (var trs = new ScopedTransaction())
            //        //{
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Cannot open server 'bobotestsql' requested by the login. Client with IP address '174.81.9.150' is not allowed to access the server.
            //    //To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule
            //    //for this IP address or address range.  It may take up to five minutes for this change to take effect.
            //    throw;
            //}

            //var json = payload.ObjectToString();

            //Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetCustomerListAsync_Test()
        {
            var payload = new CustomerPayload();
            payload.LoadAll = true;
            var customer = Save_Test();
            var filters = GetFilters(customer);
            payload.Filter = filters;

            using (var b = new Benchmark("GetCustomerListAsync_Test"))
            {
                var qry = new CustomerQuery();
                var srv = new CustomerList(dataBaseFactory, qry);
                await srv.GetCustomerListAsync(payload);
                var json = payload.ObjectToString();
            }

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetRowNumListAsync_Test()
        {
            var payload = new CustomerPayload();
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
            };

            var qry = new CustomerQuery();
            var srv = new CustomerList(dataBaseFactory, qry);
            qry.SetSecurityParameter(10001, 10001);

            IList<long> result;
            StringBuilder sb = new StringBuilder();
            try
            {
                using (var b = new Benchmark("GetCustomerDatasAsync_Test"))
                {
                    result = await srv.GetRowNumListAsync(payload);
                }
            }
            catch (Exception ex)
            {
                //Cannot open server 'bobotestsql' requested by the login. Client with IP address '174.81.9.150' is not allowed to access the server.
                //To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule
                //for this IP address or address range.  It may take up to five minutes for this change to take effect.
                throw;
            }

            var json = result.ObjectToString();

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }


        #endregion async methods

        [Fact()]
        public void ExportSql_Test()
        {
            var payload = new CustomerPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                Skip = 0,
                Top = 100
            };
            var customerList = new CustomerList(dataBaseFactory);
            customerList.GetExportJsonList(payload);
            var jArray = JArray.Parse(payload.CustomerDataList.ToString());
            var attr = JObject.Parse(jArray[0]["CustomerAttributes"].ToString());

            Assert.True(attr != null, "succ");
        }

    }
}


