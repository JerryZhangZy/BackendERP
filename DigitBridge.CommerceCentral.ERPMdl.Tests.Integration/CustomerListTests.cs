
    

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


        #region sync methods

        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ExcuteJsonAsync_Test()
        {
            var payload = new CustomerPayload();
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
            };

            var qry = new CustomerQuery();
            var srv = new CustomerList(dataBaseFactory, qry);
            srv.LoadRequestParameter(payload);
            //qry.SetFilterValue("OrderDateFrom", DateTime.Today.AddDays(-30));
            //qry.OrderNumberFrom.FilterValue = "j5rjyh5s54kaoji12g9hynwn5f6y3hgn7ep61zw7oy60ilwb2p";
            //qry.OrderNumberTo.FilterValue = "j5rjyh5s54kaoji12g9hynwn5f6y3hgn7ep61zw7oy60ilwb2p";
            //qry.OrderStatus.MultipleFilterValueString = "11,18,86";

            var totalRecords = 0;
            var result = false;
            StringBuilder sb = new StringBuilder();
            try
            {
                using (var b = new Benchmark("ExcuteJsonAsync_Test"))
                {
                    payload.CustomerListCount = await srv.CountAsync().ConfigureAwait(false);
                    result = await srv.ExcuteJsonAsync(sb).ConfigureAwait(false);
                    if (result)
                        payload.CustomerList = sb;

                    //using (var trs = new ScopedTransaction())
                    //{
                    //}
                }
            }
            catch (Exception ex)
            {
                //Cannot open server 'bobotestsql' requested by the login. Client with IP address '174.81.9.150' is not allowed to access the server.
                //To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule
                //for this IP address or address range.  It may take up to five minutes for this change to take effect.
                throw;
            }

            var json = payload.ObjectToString();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetCustomerListAsync_Test()
        {
            var payload = new CustomerPayload();
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
            };

            using (var b = new Benchmark("GetCustomerListAsync_Test"))
            {
                var qry = new CustomerQuery();
                var srv = new CustomerList(dataBaseFactory, qry);
                payload = await srv.GetCustomerListAsync(payload);
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
                    result = await srv.GetRowNumListAsync(payload).ConfigureAwait(false);
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

    }
}


