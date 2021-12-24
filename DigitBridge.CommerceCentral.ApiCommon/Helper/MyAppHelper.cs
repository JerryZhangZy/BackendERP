﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static class MyAppHelper
    {
        public async static Task<IDataBaseFactory> CreateDefaultDatabaseAsync(IPayload payload)
        {
            var config = await MyCache.GetCommerceCentralDbConnSetting(payload.MasterAccountNum);
            payload.DatabaseNum = config.DatabaseNum;
            return DataBaseFactory.CreateNewDefault(config);
        }

        /// <summary>
        /// Read All MasterAccountNum And ProfileNum in payloadbase
        /// </summary>
        /// <returns></returns>
        public async static Task<IList<IPayload>> GetAllPayloadAsync()
        {
            var payloadList = new List<IPayload>(); 
            payloadList.Add(new PayloadBase()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001
            });

            //TODO Load this from db.

            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10001,
            //    ProfileNum = 10006
            //});
            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10001,
            //    ProfileNum = 10011
            //});
            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10001,
            //    ProfileNum = 10012
            //});

            payloadList.Add(new PayloadBase()
            {
                MasterAccountNum = 10002,
                ProfileNum = 10003
            });
            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10002,
            //    ProfileNum = 10004
            //});
            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10002,
            //    ProfileNum = 10005
            //});

            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10003,
            //    ProfileNum = 10007
            //});


            //payloadList.Add(new PayloadBase()
            //{
            //    MasterAccountNum = 10004,
            //    ProfileNum = 10004
            //});

            payloadList.Add(new PayloadBase()
            {
                MasterAccountNum = 10004,
                ProfileNum = 10008
            });
            return payloadList;
        }

        public async static Task<IDataBaseFactory> CreateDefaultDatabaseAsync(int masterAccountNum)
        {
            var config = await MyCache.GetCommerceCentralDbConnSetting(masterAccountNum);
            return DataBaseFactory.CreateDefault(config);
        }

        public static DataBaseFactory GetDatabase(int masterAccountNum)
        {
            var connectStr = GetConnectionString(masterAccountNum);
            ConfigurationManager.AppSettings["dsn"] = connectStr;
            return new DataBaseFactory(connectStr);
        }

        public static string GetConnectionString(int masterAccountNum)
        {
            return MySingletonAppSetting.Dsn;
        }
    }
}
