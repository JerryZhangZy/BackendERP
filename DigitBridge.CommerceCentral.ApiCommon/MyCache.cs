using DigitBridge.Base.Utility;
using DigitBridge.Orchestration.OrchestrationDb;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    /// <summary>
    /// May need to replace MemoryChche if performance is an issue.
    /// </summary>
    public static class MyCache
    {
        public static string GetCommerceCentralKey(int masterAccountNum)
        {
            return "CommerceCentral-" + masterAccountNum;
        }

        public static string GetCommerceCentralKey()
        {
            return "CommerceCentral-all";
        }

        public static string GetDatabaseSettingKey(int databaseNum)
        {
            return "Db-" + databaseNum;
        }

        public static async Task<DbConnSetting> GetCommerceCentralDbConnSetting(int masterAccountNum)
        {
            try
            {
                int databaseNum = 0;
                DbConnSetting dc = new DbConnSetting();

                if (CacheBase.TryGetValue<int>(GetCommerceCentralKey(masterAccountNum), out databaseNum))
                {//find the databaseNum in cache
                    if (CacheBase.TryGetValue<DbConnSetting>(GetDatabaseSettingKey(databaseNum), out dc))
                    {
                        return dc;
                    };
                }

                //neither CommerceCentral cache nor database cache is found, get connection by masterAccountNum
                var dbConnSetting = await GetCommerceCentralDbConnSettingByMasterAccount(masterAccountNum);

                //add CommerceCentral DatabaseNum to cache
                CacheBase.Set<int>(GetCommerceCentralKey(masterAccountNum), dbConnSetting.DatabaseNum,
                  MySingletonAppSetting.DefaultCacheExpireSlidingMins);

                //add db setting to cache
                CacheBase.Set<DbConnSetting>(GetDatabaseSettingKey(dbConnSetting.DatabaseNum), dbConnSetting,
                   MySingletonAppSetting.DefaultCacheExpireSlidingMins);

                return dbConnSetting;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static async Task<List<DbConnSetting>> GetCommerceCentralDbConnSetting()
        {
            try
            {
                List<DbConnSetting> dbConnSettings = new List<DbConnSetting>();

                var dbConnSetting = await GetCommerceCentralDbConnSettingByMasterAccount();

                dbConnSettings.Add(dbConnSetting);

                //add db setting to cache
                CacheBase.Set<List<DbConnSetting>>(GetCommerceCentralKey(), dbConnSettings,
                   MySingletonAppSetting.DefaultCacheExpireSlidingMins);

                return dbConnSettings;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static async Task<DbConnSetting> GetCommerceCentralDbConnSettingByMasterAccount(int masterAccountNum = 0)
        {
            try
            {
                if (masterAccountNum == 0)
                    masterAccountNum = 10001;

                MasterAccount ma = await MasterAccount.CreateAsync(MySingletonAppSetting.OrchestrationDbConnString,
                    MySingletonAppSetting.UseAzureManagedIdentity, MySingletonAppSetting.AzureTokenProviderConnectionString,
                    MySingletonAppSetting.OrchestrationDbTenantId);

                MasterAccountConnSettingDto maSetting = await ma.GetConnSettingByTargetName(masterAccountNum, "CommerceCentral");
                if (maSetting is null)
                {
                    throw new Exception("Connection Setting for MasterAccount is not set yet. " +
                        "MasterAccoutNum: " + masterAccountNum);
                }

                DbConnSetting dbConnSetting = new DbConnSetting()
                {
                    ConnString = maSetting.ConnectionString,
                    UseAzureManagedIdentity = maSetting.UseManagementIdentity == 1 ? true : false,
                    TenantId = maSetting.TenantId,
                    DatabaseNum = maSetting.DatabaseNum
                };

                return dbConnSetting;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }

    public static class CacheBase
    {
        public static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        public static void Remove(object key)
        {
            Cache.Remove(key);
        }

        public static TItem Set<TItem>(object key, TItem value)
        {
            return Cache.Set<TItem>(key, value);
        }

        public static TItem Set<TItem>(object key, TItem value, int slidingExpireMins)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpireMins));
            return Cache.Set<TItem>(key, value, cacheEntryOptions);
        }

        public static bool TryGetValue<TItem>(object key, out TItem value)
        {
            return Cache.TryGetValue(key, out value);
        }
    }
}