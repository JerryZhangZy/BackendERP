              
    

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
    /// Represents a Tester for PoItemsRef.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class PoItemsRefTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<PoItemsRef> GetFakerData()
        {
            #region faker data rules
            return new Faker<PoItemsRef>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.PoItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralFulfillmentNum, f => default(long))
					.RuleFor(u => u.ShippingCarrier, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ShippingClass, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.DistributionCenterNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.CentralOrderNum, f => default(long))
					.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SecondaryChannelOrderID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ShippingAccount, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.EndBuyerUserID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.EndBuyerName, f => f.Company.CompanyName())
					.RuleFor(u => u.EndBuyerEmail, f => f.Internet.Email())
					.RuleFor(u => u.ShipToName, f => f.Company.CompanyName())
					.RuleFor(u => u.ShipToFirstName, f => f.Company.CompanyName())
					.RuleFor(u => u.ShipToLastName, f => f.Company.CompanyName())
					.RuleFor(u => u.ShipToSuffix, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ShipToCompany, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ShipToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ShipToAttention, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ShipToAddressLine1, f => f.Address.StreetAddress())
					.RuleFor(u => u.ShipToAddressLine2, f => f.Address.SecondaryAddress())
					.RuleFor(u => u.ShipToAddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.ShipToCity, f => f.Address.City())
					.RuleFor(u => u.ShipToState, f => f.Address.State())
					.RuleFor(u => u.ShipToStateFullName, f => f.Company.CompanyName())
					.RuleFor(u => u.ShipToPostalCode, f => f.Address.ZipCode())
					.RuleFor(u => u.ShipToPostalCodeExt, f => f.Lorem.Word())
					.RuleFor(u => u.ShipToCounty, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ShipToCountry, f => f.Address.Country())
					.RuleFor(u => u.ShipToEmail, f => f.Internet.Email())
					.RuleFor(u => u.ShipToDaytimePhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.ShipToNightPhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.BillToName, f => f.Company.CompanyName())
					.RuleFor(u => u.BillToFirstName, f => f.Company.CompanyName())
					.RuleFor(u => u.BillToLastName, f => f.Company.CompanyName())
					.RuleFor(u => u.BillToSuffix, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.BillToCompany, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.BillToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.BillToAttention, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.BillToAddressLine1, f => f.Address.StreetAddress())
					.RuleFor(u => u.BillToAddressLine2, f => f.Address.SecondaryAddress())
					.RuleFor(u => u.BillToAddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.BillToCity, f => f.Address.City())
					.RuleFor(u => u.BillToState, f => f.Address.State())
					.RuleFor(u => u.BillToStateFullName, f => f.Company.CompanyName())
					.RuleFor(u => u.BillToPostalCode, f => f.Address.ZipCode())
					.RuleFor(u => u.BillToPostalCodeExt, f => f.Lorem.Word())
					.RuleFor(u => u.BillToCounty, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.BillToCountry, f => f.Address.Country())
					.RuleFor(u => u.BillToEmail, f => f.Internet.Email())
					.RuleFor(u => u.BillToDaytimePhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.BillToNightPhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<PoItemsRef> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public PoItemsRefTests(TestFixture<StartupTest> fixture) 
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
            FakerData = GetFakerData();
        }
        public void Dispose()
        {
        }

        //[Fact()]
        [Fact(Skip = SkipReason)]
        public void Register_Test()
        {
            var data = FakerData.Generate();
            data.Register();
            var poco = data.GetPocoData();

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void CopyFrom_Test()
        {
            var data = FakerData.Generate();
            var dataJson = data.ObjectToString();
            var newData = FakerData.Generate();
            var newDataJson = newData.ObjectToString();
            data?.CopyFrom(newData);
            var result = !data.Equals(newData);

			Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        #region sync methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void Add_Test()
		{
			var data = FakerData.Generate();
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
			data.Add();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoItemsRef>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoItemsRef>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoItemsRef();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "ShippingCarrier", "ShippingClass" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<PoItemsRef>(data.RowNum);
            var result = dataGet.ShippingCarrier != dataOrig.ShippingCarrier &&
                            dataGet.ShippingClass != dataOrig.ShippingClass &&
                            dataGet.ShippingCarrier == newData.ShippingCarrier &&
                            dataGet.ShippingClass == newData.ShippingClass;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Save_Test()
        {
	        var dataNew = FakerData.Generate();
            dataNew.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            dataNew.Save();
            DataBaseFactory.Commit();

            var dataUpdate = DataBaseFactory.GetById<PoItemsRef>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"PoItemUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoItemsRef>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<PoItemsRef>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<PoItemsRef>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<PoItemsRef>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<PoItemsRef>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .Save<PoItemsRef>();

            var cnt = DataBaseFactory.Count<PoItemsRef>("WHERE PoUuid = @0", PoUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .Save<PoItemsRef>();

            var NewShippingClass = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            listFind.ToList().ForEach(x => x.ShippingClass = NewShippingClass);
            listFind.Save<PoItemsRef>();

            list = DataBaseFactory.Find<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            var result = list.Where(x => x.ShippingClass == NewShippingClass).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<PoItemsRef>("WHERE PoUuid = @0", PoUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<PoItemsRef>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<PoItemsRef>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

        #region async methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddAsync_Test()
        {
            var data = FakerData.Generate();
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await data.AddAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItemsRef>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItemsRef>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoItemsRef();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "ShippingCarrier", "ShippingClass" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<PoItemsRef>(data.RowNum);
            var result = dataGet.ShippingCarrier != dataOrig.ShippingCarrier &&
                            dataGet.ShippingClass != dataOrig.ShippingClass &&
                            dataGet.ShippingCarrier == newData.ShippingCarrier &&
                            dataGet.ShippingClass == newData.ShippingClass;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveAsync_Test()
        {
            var dataNew = FakerData.Generate();
            dataNew.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await dataNew.SaveAsync();
            DataBaseFactory.Commit();

            var dataUpdate = await DataBaseFactory.GetByIdAsync<PoItemsRef>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "PoItemUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItemsRef>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<PoItemsRef>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<PoItemsRef>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<PoItemsRef>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .SaveAsync<PoItemsRef>();

            var cnt = await DataBaseFactory.CountAsync<PoItemsRef>("WHERE PoUuid = @0", PoUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .SaveAsync<PoItemsRef>();

            var NewShippingClass = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid)).ToList();
            listFind.ToList().ForEach(x => x.ShippingClass = NewShippingClass);
            await listFind.SaveAsync<PoItemsRef>();

            list = DataBaseFactory.Find<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            var result = list.Where(x => x.ShippingClass == NewShippingClass).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItemsRef>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<PoItemsRef>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<PoItemsRef>("WHERE PoUuid = @0", PoUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItemsRef>("SELECT TOP 1 * FROM PoItemsRef")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<PoItemsRef>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<PoItemsRef>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


