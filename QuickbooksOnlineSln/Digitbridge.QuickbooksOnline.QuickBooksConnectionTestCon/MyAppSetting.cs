using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UneedgoHelper.DotNet.Common.IO;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksTestCon
{
    public class MyAppSetting
    {
        private static IConfigurationRoot _config;
        public MyAppSetting()
        {
            _config = new ConfigurationBuilder()
                   .SetBasePath(FileUtility.ExecutingPath)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .Build();
        }

        public string DBConnectionValue
        {
            get
            {
                return _config.GetSection("DataBase")["DBConnectionValue"];
            }
        }

        public bool AzureUseManagedIdentity
        {
            get
            {
                return _config.GetSection("DataBase")["AzureUseManagedIdentity"].Equals(
                    "true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public string AzureTokenProviderConnectionString
        {
            get
            {
                return _config.GetSection("DataBase")["AzureTokenProviderConnectionString"];
            }
        }

        public string CentralOrderTableName
        {
            get
            {
                return _config.GetSection("DataBase")["CentralOrderTableName"];
            }
        }

        public string CentralItemLineTableName
        {
            get
            {
                return _config.GetSection("DataBase")["CentralItemLineTableName"];
            }
        }
        public string QuickBooksConnectionInfoTableName
        {
            get
            {
                return _config.GetSection("DataBase")["QuickBooksConnectionInfoTableName"];
            }
        }
        public string QuickBooksIntegrationSettingTableName
        {
            get
            {
                return _config.GetSection("DataBase")["QuickBooksIntegrationSettingTableName"];
            }
        }
        public string QuickBooksChannelAccSettingTableName
        {
            get
            {
                return _config.GetSection("DataBase")["QuickBooksChannelAccSettingTableName"];
            }
        }
        public string AzureTenantId
        {
            get
            {
                return _config.GetSection("DataBase")["AzureTenantId"];
            }
        }

        public string CryptKey
        {
            get
            {
                return _config.GetSection("DataBase")["CryptKey"];
            }
        }
        public string Environment
        {
            get
            {
                return _config.GetSection("QuickBooks")["Environment"];
            }
        }
        public string RedirectUrl
        {
            get
            {
                return _config.GetSection("QuickBooks")["RedirectUrl"];
            }
        }
        public string BaseUrl
        {
            get
            {
                return _config.GetSection("QuickBooks")["BaseUrl"];
            }
        }
        public string MinorVersion
        {
            get
            {
                return _config.GetSection("QuickBooks")["MinorVersion"];
            }
        }
        public string AppClientId
        {
            get
            {
                return _config.GetSection("QuickBooks")["AppClientId"];
            }
        }

        public string AppClientSecret
        {
            get
            {
                return _config.GetSection("QuickBooks")["AppClientSecret"];
            }
        }
    }
}
