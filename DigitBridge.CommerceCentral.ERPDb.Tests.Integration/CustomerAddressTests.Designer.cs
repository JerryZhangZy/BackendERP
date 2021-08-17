

              
    

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
    /// Represents a Tester for CustomerAddress.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class CustomerAddressTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<CustomerAddress> GetFakerData()
        {
            #region faker data rules
            return new Faker<CustomerAddress>()
					.RuleFor(u => u.AddressUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.AddressCode, f => f.Random.Guid().ToString())
					.RuleFor(u => u.AddressType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.Description, f => f.Commerce.ProductName())
					.RuleFor(u => u.Name, f => f.Company.CompanyName())
					.RuleFor(u => u.FirstName, f => f.Company.CompanyName())
					.RuleFor(u => u.LastName, f => f.Company.CompanyName())
					.RuleFor(u => u.Suffix, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.Company, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.CompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.Attention, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.AddressLine1, f => f.Address.StreetAddress())
					.RuleFor(u => u.AddressLine2, f => f.Address.SecondaryAddress())
					.RuleFor(u => u.AddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.City, f => f.Address.City())
					.RuleFor(u => u.State, f => f.Address.State())
					.RuleFor(u => u.StateFullName, f => f.Company.CompanyName())
					.RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
					.RuleFor(u => u.PostalCodeExt, f => f.Lorem.Word())
					.RuleFor(u => u.County, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.Country, f => f.Address.Country())
					.RuleFor(u => u.Email, f => f.Internet.Email())
					.RuleFor(u => u.DaytimePhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.NightPhone, f => f.Phone.PhoneNumber())
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<CustomerAddress> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public CustomerAddressTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<CustomerAddress>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<CustomerAddress>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new CustomerAddress();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "AddressCode", "Description" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<CustomerAddress>(data.RowNum);
            var result = dataGet.AddressCode != dataOrig.AddressCode &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.AddressCode == newData.AddressCode &&
                            dataGet.Description == newData.Description;

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

            var dataUpdate = DataBaseFactory.GetById<CustomerAddress>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"AddressUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<CustomerAddress>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<CustomerAddress>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<CustomerAddress>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<CustomerAddress>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<CustomerAddress>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .Save<CustomerAddress>();

            var cnt = DataBaseFactory.Count<CustomerAddress>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .Save<CustomerAddress>();

            var NewDescription = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            listFind.ToList().ForEach(x => x.Description = NewDescription);
            listFind.Save<CustomerAddress>();

            list = DataBaseFactory.Find<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            var result = list.Where(x => x.Description == NewDescription).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<CustomerAddress>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<CustomerAddress>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<CustomerAddress>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<CustomerAddress>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<CustomerAddress>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new CustomerAddress();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "AddressCode", "Description" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<CustomerAddress>(data.RowNum);
            var result = dataGet.AddressCode != dataOrig.AddressCode &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.AddressCode == newData.AddressCode &&
                            dataGet.Description == newData.Description;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<CustomerAddress>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "AddressUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<CustomerAddress>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<CustomerAddress>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<CustomerAddress>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<CustomerAddress>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .SaveAsync<CustomerAddress>();

            var cnt = await DataBaseFactory.CountAsync<CustomerAddress>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .SaveAsync<CustomerAddress>();

            var NewDescription = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid)).ToList();
            listFind.ToList().ForEach(x => x.Description = NewDescription);
            await listFind.SaveAsync<CustomerAddress>();

            list = DataBaseFactory.Find<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            var result = list.Where(x => x.Description == NewDescription).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<CustomerAddress>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<CustomerAddress>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<CustomerAddress>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<CustomerAddress>("SELECT TOP 1 * FROM CustomerAddress")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<CustomerAddress>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<CustomerAddress>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


