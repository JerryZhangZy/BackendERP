              
    

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
using Bogus;

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for ApInvoiceHeaderAttributes Helper.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class ApInvoiceHeaderAttributesHelperTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<ApInvoiceHeaderAttributes> FakerData => ApInvoiceHeaderAttributesTests.GetFakerData();
        public IDataBaseFactory DataBaseFactory { get; set; }

        public ApInvoiceHeaderAttributesHelperTests(TestFixture<StartupTest> fixture) 
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


        #region sync methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void QueryByJson_Test()
        {
            var data = FakerData.Generate(10);
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            data.Save();
            DataBaseFactory.Commit();

            try
            {
                var result = false;
                using (var b = new Benchmark("QueryByJson_Test"))
                {
                    using (var trs = new ScopedTransaction(DataBaseFactory))
                    {
                        var rowNum = 0;
                        var dataGet = SqlQuery.QueryJson<ApInvoiceHeaderAttributes>(
                            ApInvoiceHeaderAttributesHelper.SelectAllWhere(
                                sqlWhere: $"WHERE RowNum >= @rowNum ORDER BY RowNum OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY",
                                forJson: true
                            ),
                            CommandType.Text,
                            new SqlParameter("@rowNum", rowNum)
                        ).ToList();
                        result = dataGet.Count.Equals(10);
                    }
                }

                Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void QueryBy_Test()
        {
            try
            {
                var result = false;
                using (var b = new Benchmark("QueryByAsync_Test"))
                {
                    using (var trs = new ScopedTransaction(DataBaseFactory))
                    {
                        var rowNum = 0;
                        var dataGet = SqlQuery.Query<ApInvoiceHeaderAttributes>(
                            ApInvoiceHeaderAttributesHelper.SelectAllWhere(
                                sqlWhere: $"WHERE RowNum >= @rowNum ORDER BY RowNum OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY",
                                forJson: false
                            ),
                            CommandType.Text,
                            new SqlParameter("@rowNum", rowNum)
                        ).ToList();
                        result = dataGet.Count.Equals(10);
                    }
                }

                Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion sync methods

        #region async methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task QueryByJsonAsync_Test()
        {
            var data = FakerData.Generate(10);
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await data.SaveAsync();
            DataBaseFactory.Commit();

            try
            {
                var result = false;
                using (var b = new Benchmark("QueryByJsonAsync_Test"))
                {
                    using (var trs = new ScopedTransaction(DataBaseFactory))
                    {
                        var rowNum = 0;
                        var dataGet = (await SqlQuery.QueryJsonAsync<ApInvoiceHeaderAttributes>(
                            ApInvoiceHeaderAttributesHelper.SelectAllWhere(
                                sqlWhere: $"WHERE RowNum >= @rowNum ORDER BY RowNum OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY",
                                forJson: true
                            ),
                            CommandType.Text,
                            new SqlParameter("@rowNum", rowNum)
                        )).ToList();
                        result = dataGet.Count.Equals(10);
                    }
                }

                Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task QueryByAsync_Test()
        {
            try
            {
                var result = false;
                using (var b = new Benchmark("QueryByAsync_Test"))
                {
                    using (var trs = new ScopedTransaction(DataBaseFactory))
                    {
                        var rowNum = 0;
                        var dataGet = (await SqlQuery.QueryAsync<ApInvoiceHeaderAttributes>(
                            ApInvoiceHeaderAttributesHelper.SelectAllWhere(
                                sqlWhere: $"WHERE RowNum >= @rowNum ORDER BY RowNum OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY",
                                forJson: false
                            ),
                            CommandType.Text,
                            new SqlParameter("@rowNum", rowNum)
                        )).ToList();
                        result = dataGet.Count.Equals(10);
                    }
                }

                Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion async methods

    }
}


