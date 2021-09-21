using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Common.IO;

namespace Digitbridge.QuickbooksOnline.CentralToDbTestCon
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
                return _config["DBConnectionValue"];
            }
        }

        public bool AzureUseManagedIdentity
        {
            get
            {
                return _config["AzureUseManagedIdentity"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public string AzureTokenProviderConnectionString
        {
            get
            {
                return _config["AzureTokenProviderConnectionString"];
            }
        }

        public string AzureTenantId
        {
            get
            {
                return _config["AzureTenantId"];
            }
        }

        public string CentralOrderTableName
        {
            get
            {
                return _config["CentralOrderTableName"];
            }
        }

        public string CentralItemLineTableName
        {
            get
            {
                return _config["CentralItemLineTableName"];
            }
        }
        public string QuickBooksIntegrationSettingTableName
        {
            get
            {
                return _config["QuickBooksIntegrationSettingTableName"];
            }
        }
        public int CentralGetOrderApiPageSize
        {
            get
            {
                return _config["CentralGetOrderApiPageSize"].ForceToInt();
            }
        }
        public int RollingImportDayRange
        {
            get
            {
                return _config["RollingImportDayRange"].ForceToInt();
            }
        }
        public string CentralInApiEndPoint
        {
            get
            {
                return _config["CentralInApiEndPoint"];
            }
        }
        public string CentralInOrdersApiName
        {
            get
            {
                return _config["CentralInOrdersApiName"];
            }
        }
        public string CentralInOrderExtensionFlagsApiName
        {
            get
            {
                return _config["CentralInOrderExtensionFlagsApiName"];
            }
        }
        public string CentralIntegrationApiCode
        {
            get
            {
                return _config["CentralIntegrationApiCode"];
            }
        }
        public string AzureWebJobsStorage
        {
            get
            {
                return _config["AzureWebJobsStorage"];
            }
        }
        public string AzureWebJobsStorageAccountName
        {
            get
            {
                return _config["AzureWebJobsStorageAccountName"];
            }
        }
        public string AzureWebJobsStorageEndpointSuffix
        {
            get
            {
                return _config["AzureWebJobsStorageEndpointSuffix"];
            }
        }
        public string AzureWebJobsStorageAccountKey
        {
            get
            {
                return _config["AzureWebJobsStorageAccountKey"];
            }
        }
        public bool AzureWebJobsStorageUseHttps
        {
            get
            {
                return _config["AzureWebJobsStorageUseHttps"].Equals("true", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            }
        }
        public bool AzureWebJobsStorageUseManagedIdentity
        {
            get
            {
                return _config["AzureWebJobsStorageUseManagedIdentity"].Equals("true", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            }
        }
        public string AzureWebJobsStorageTokenProviderConnectionString
        {
            get
            {
                return _config["AzureWebJobsStorageTokenProviderConnectionString"];
            }
        }
        public string AzureWebJobsStorageTenantId
        {
            get
            {
                return _config["AzureWebJobsStorageTenantId"];
            }
        }
    }
}
