using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using DigitBridge.Base.Utility;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WmsPurchaseOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7074/api/";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        public WmsPurchaseOrderClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Fact()]
        public async Task QueryData_Test()
        {
            var client = new WmsPurchaseOrderClient(_baseUrl, _code);
            var query = new WmsQueryModel()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                UpdateDateFrom = DateTime.Now.AddDays(-10),
                UpdateDateTo = DateTime.Now
            };
            var result =await client.QueryWmsPurchaseOrderListAsync(query);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.ResultTotalCount >0, "succ");
            Assert.True(result, "succ");
        }
        
        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public IList<PoHeader> GetFakerData(int count)
        {
            var obj = new PoHeader();
            var datas = new List<PoHeader>();
            for (int i = 0; i < count; i++)
                datas.Add(GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public PoHeader GetFakerData()
        {
            var poheader= GetPoHeaderFaker().Generate();
            poheader.PoLineList = GetPoLineFaker().Generate(3);
            return poheader;
        }
        
		/// <summary>
		/// Get faker object for PoTransactionDto
		/// </summary>
		/// <param name="dto">PoTransactionDto</param>
		/// <returns>Faker object use to generate data</returns>
		public Faker<PoHeader> GetPoHeaderFaker()
		{
			#region faker data rules
			return new Faker<PoHeader>()
				.RuleFor(u => u.PoUuid, f => System.Guid.NewGuid().ToString())
				.RuleFor(u => u.VendorName, f => f.Company.CompanyName())
				.RuleFor(u => u.PoNum, f => f.Random.AlphaNumeric(50))
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoTransactionDto
		/// </summary>
		/// <param name="dto">PoTransactionDto</param>
		/// <returns>Faker object use to generate data</returns>
		public Faker<PoLine> GetPoLineFaker()
		{
			#region faker data rules
			return new Faker<PoLine>()
                    .RuleFor(u=>u.SKU,f=>f.Commerce.Product())
				.RuleFor(u => u.PoUuid, f => System.Guid.NewGuid().ToString())
				.RuleFor(u => u.PoItemUuid, f => System.Guid.NewGuid().ToString())
				.RuleFor(u => u.PoQty, f => f.Random.Int(5,2000))
				;
			#endregion faker data rules
		}

        [Fact()]
        public async Task AddData_Test()
        {
            var client = new WmsPurchaseOrderClient(_baseUrl, _code);
            var result =await client.CreatePoReceiveAsync(10001,10001,GetFakerData());
            // Assert.True(client.Data != null, "succ");
            Assert.True(result||client.Messages.Count>0, "succ");
            Assert.True(result, "succ");
        }
        [Fact()]
        public async Task AddBatchData_Test()
        {
            var client = new WmsPurchaseOrderClient(_baseUrl, _code);
            var result =await client.CreateBatchPoReceiveAsync(10001,10001,GetFakerData(10));
            Assert.True(result||client.Messages.Count>0, "succ");
            Assert.True(result, "succ");
        }
        [Fact()]
        public async Task QueryDataWithConfig_Test()
        {
            var client = new WmsPurchaseOrderClient();
            var query = new WmsQueryModel()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                UpdateDateFrom = DateTime.Now.AddDays(-10),
                UpdateDateTo = DateTime.Now
            };
            var result =await client.QueryWmsPurchaseOrderListAsync(query);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.ResultTotalCount >0, "succ");
            Assert.True(result, "succ");
        }

        public void Dispose()
        {
        }
    }
}
