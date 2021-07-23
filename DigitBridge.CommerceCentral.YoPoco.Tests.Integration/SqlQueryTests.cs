
    

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
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class SqlQueryTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public SqlQueryTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
        }
        public void Dispose()
        {
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task QueryAzureDb_Test()
        {
            var conf = new DbConnSetting()
            {
                ConnString = Configuration["DBConnectionString"],
                UseAzureManagedIdentity = Configuration["UseAzureManagedIdentity"].ToBool(),
                TenantId = Configuration["DbTenantId"],
                TokenProviderConnectionString = Configuration["AzureTokenProviderConnectionString"],
                DatabaseNum = 1
            };

            var sqlProduct = $@"SELECT c.name AS ColumnName
                FROM sys.indexes AS i 
                INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id 
                INNER JOIN sys.objects AS o ON i.object_id = o.object_id 
                LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                WHERE (i.is_primary_key = 1) AND (o.name = @tableName)";

            SqlQueryResultData result;
            try
            {
                using (var b = new Benchmark("QueryAzureDb_Test"))
                {
                    using (var trs = new ScopedTransaction(conf))
                    {
                        result = SqlQuery.QuerySqlQueryResultData(sqlProduct, System.Data.CommandType.Text, new SqlParameter("@tableName", "OrderHeader"));
                    }
                }
            }
            catch (Exception ex)
            {
                //Cannot open server 'bobotestsql' requested by the login. Client with IP address '174.81.9.150' is not allowed to access the server.
                //To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule
                //for this IP address or address range.  It may take up to five minutes for this change to take effect.
                throw;
            }


            sqlProduct = $@"SELECT TOP 100 * FROM OrderHeader";
            try
            {
                using (var b = new Benchmark("QueryAzureDb_SelectOrderHeader_Test"))
                {
                    using (var trs = new ScopedTransaction(conf))
                    {
                        result = SqlQuery.QuerySqlQueryResultData(sqlProduct, System.Data.CommandType.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task QueryAsync_Test()
		{
            var sqlProduct = $@"SELECT prd.* FROM ProductBasic prd";
            var result = new List<ProductBasic>();
            using (var b = new Benchmark("QueryAsync_Test_ProductBasic"))
            {
                result = (await SqlQuery.QueryAsync<ProductBasic>(sqlProduct, System.Data.CommandType.Text)).ToList();
            }

            var sqlInventory = $@"SELECT * FROM Inventory";
            var result2 = new List<Inventory>();
            using (var b = new Benchmark("QueryAsync_Test_Inventory"))
            {
                result2 = (await SqlQuery.QueryAsync<Inventory>(sqlInventory, System.Data.CommandType.Text)).ToList();
            }

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task QueryJsonAsync_Test()
        {
            var sql = $@"
SELECT prd.*,
(SELECT * FROM Inventory WHERE ProductUuid = prd.ProductUuid FOR JSON PATH) AS Inventory
FROM ProductBasic prd
FOR JSON PATH
";
            var sb = new StringBuilder();
            var result = new List<InventoryData>();
            using (var b = new Benchmark("QueryJsonAsync_Test"))
            {
                result = (await SqlQuery.QueryJsonAsync<InventoryData>(sql, System.Data.CommandType.Text)).ToList();
            }

            Assert.True(result != null, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task QueryJsonStringBuilderAsync_Test()
		{
            var sql = $@"
SELECT prd.*,
(SELECT * FROM Inventory WHERE ProductUuid = prd.ProductUuid FOR JSON PATH) AS Inventory
FROM ProductBasic prd
FOR JSON PATH
";
            var sb = new StringBuilder();
            var result = false;
            using (var b = new Benchmark("QueryJsonStringBuilderAsync_Test"))
            {
                result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text);
            }

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

    }
}



