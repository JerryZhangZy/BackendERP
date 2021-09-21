using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Digitbridge.QuickbooksOnline.QuickBooksConnection.Model;
using Intuit.Ipp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.IntegrationApiMdl
{
    public class QuickbooksOnlineIntegrationSetting
    {
        private QboDbConfig _dbConfig;
        private QboIntegrationSettingDb _qboIntegrationSettingDb;
        private QboChnlAccSettingDb _qboChnlAccSettingDb;
        private QboConnectionInfoDb _qboConnectionInfoDb;
        private QboConnectionConfig _qboConnectionConfig;
        private MsSqlUniversal _msSql;
        private int _masterAccNum;
        private int _profileNum;

        private QuickbooksOnlineIntegrationSetting(MsSqlUniversal msSql, QboDbConfig dbConfig, 
            QboConnectionConfig qboConnectionConfig, QboIntegrationSettingDb qboIntegrationSettingDb, 
            QboChnlAccSettingDb qboChnlAccSettingDb, QboConnectionInfoDb qboConnectionInfoDb, int masterAccNum, int profileNum)
        {
            _msSql = msSql;
            _dbConfig = dbConfig;
            _qboConnectionConfig = qboConnectionConfig;
            _qboIntegrationSettingDb = qboIntegrationSettingDb;
            _qboChnlAccSettingDb = qboChnlAccSettingDb;
            _qboConnectionInfoDb = qboConnectionInfoDb;
            _masterAccNum = masterAccNum;
            _profileNum = profileNum;
        }

        public static async Task<QuickbooksOnlineIntegrationSetting> CreateAsync(QboDbConfig dbConfig, 
            QboConnectionConfig qboConnectionConfig, int masterAccNum, int profileNum)
        {
            try
            {
                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                        dbConfig.QuickBooksDbConnectionString
                        , dbConfig.UseAzureManagedIdentity
                        , dbConfig.TokenProviderConnectionString
                        , dbConfig.AzureTenantId
                        );

                QboIntegrationSettingDb qboIntegrationSettingDb = 
                    new QboIntegrationSettingDb(dbConfig.QuickBooksDbIntegrationSettingTableName, msSql);

                QboChnlAccSettingDb qboChnlAccSettingDb = new QboChnlAccSettingDb(
                    dbConfig.QuickBooksChannelAccSettingTableName, msSql);

                QboConnectionInfoDb qboConnectionInfoDb = new QboConnectionInfoDb(
                    dbConfig.QuickBooksDbConnectionInfoTableName, msSql, dbConfig.CryptKey);

                var quickbooksOnlineIntergrationSetting =
                    new QuickbooksOnlineIntegrationSetting(
                        msSql, dbConfig, qboConnectionConfig, qboIntegrationSettingDb, 
                        qboChnlAccSettingDb, qboConnectionInfoDb, masterAccNum, profileNum
                        );

                return quickbooksOnlineIntergrationSetting;
            }
            catch(Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        public async Task<IntegrationSettingApiRespondType> GetIntegrationSetting()
        {
            IntegrationSettingApiRespondType reponse = new IntegrationSettingApiRespondType();
            try
            {
                DataTable settingTb = await _qboIntegrationSettingDb.GetIntegrationSettingAsync(new Db.Model.Command(_masterAccNum, _profileNum));
                QboIntegrationSetting intergrationSetting = ComlexTypeConvertExtension.
                                                                DatatableToList<QboIntegrationSetting>(settingTb).FirstOrDefault();

                DataTable accSettingsDtb = await _qboChnlAccSettingDb.GetChnlAccSettingsAsync(new Db.Model.Command(_masterAccNum, _profileNum));
                List<QboChnlAccSetting> accSettings =
                       ComlexTypeConvertExtension.DatatableToList<QboChnlAccSetting>(accSettingsDtb);

                reponse.IntegrationSetting = intergrationSetting;
                reponse.ChnlAccSettings = accSettings;
                
                return reponse;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<(bool, string)> PostIntegrationSetting(string requestBody)
        {
            bool hasError = false;
            string errMsg = "";

            try
            {
                //var settings = new JsonSerializerSettings
                //{
                //    NullValueHandling = NullValueHandling.Ignore,
                //    MissingMemberHandling = MissingMemberHandling.Ignore
                //};

                IntegrationSettingApiReqType settingReqType = 
                    JsonConvert.DeserializeObject<IntegrationSettingApiReqType>(requestBody);

                (bool isReqBodyValid, string reqBodyErrMsg) = ValidateReqBody(settingReqType);

                if(!isReqBodyValid)
                {
                    return (false, reqBodyErrMsg);
                }

                await HandleQboDefaultItems(settingReqType.IntegrationSetting);

                await _qboIntegrationSettingDb.AddIntegrationSettingAsync(
                    ComlexTypeConvertExtension.ObjectToDataTable(
                        settingReqType.IntegrationSetting)
                    );

                await _qboChnlAccSettingDb.AddChnlAccSettingAsync(
                   ComlexTypeConvertExtension.ListToDataTable(
                       settingReqType.ChnlAccSettings)
                   );

                return (!hasError, errMsg);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<(bool, string)> PatchIntegrationSetting(string requestBody)
        {
            bool hasError = false;
            string errMsg = "";

            try
            {
                IntegrationSettingApiReqType settingReqType =
                    JsonConvert.DeserializeObject<IntegrationSettingApiReqType>(requestBody);

                (bool isReqBodyValid, string reqBodyErrMsg) = ValidateReqBody(settingReqType);

                if (!isReqBodyValid)
                {
                    return (false, reqBodyErrMsg);
                }

                (bool isAllExist, string existenceErrMsg) = await CheckAllPatchTargetsExist(settingReqType);

                if (!isAllExist)
                {
                    return (false, existenceErrMsg);
                }

                // [ExportOrderFromDate] is only allowed for set once for now.
                DateTime exportOrderFromDate = await _qboIntegrationSettingDb.GetExportOrderFromDate(new Command(_masterAccNum, _profileNum));

                if (settingReqType.IntegrationSetting.ExportOrderFromDate == default) 
                {
                    settingReqType.IntegrationSetting.ExportOrderFromDate = exportOrderFromDate;
                }
                else
                {
                    if(!settingReqType.IntegrationSetting.ExportOrderFromDate.Equals(exportOrderFromDate))
                    {
                        return (false, "The exportOrderFromDate is only allowed for set once, an inqury is required for updating. ");
                    }
                }
                
                await HandleQboDefaultItems(settingReqType.IntegrationSetting);

                await _qboIntegrationSettingDb.UpdateIntegrationSettingAsync(
                    ComlexTypeConvertExtension.ObjectToDataTable(settingReqType.IntegrationSetting),
                    new Command
                        (
                            settingReqType.IntegrationSetting.MasterAccountNum,
                            settingReqType.IntegrationSetting.ProfileNum
                        )
                    );

                foreach(ChnlAccSettingReqType chnlAccSettingReq in settingReqType.ChnlAccSettings)
                {
                    await _qboChnlAccSettingDb.UpdateChnlAccSettingAsync(
                       ComlexTypeConvertExtension.ObjectToDataTable(chnlAccSettingReq), 
                       chnlAccSettingReq.MasterAccountNum,
                       chnlAccSettingReq.ProfileNum,
                       chnlAccSettingReq.ChannelAccountNum
                       );
                }

                return (!hasError, errMsg);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<UserInitialDataApiResponseType> GetUserInitialData()
        {
            UserInitialDataApiResponseType response = new UserInitialDataApiResponseType();
            try
            {
                QboUniversal qboUniversal = await ConnectToQbo(_qboConnectionConfig);
                
                if(qboUniversal == null)
                {
                    return null;
                }

                List<Customer> customers = await qboUniversal.GetCustomers();

                List<CustomerPair> customerPairs = customers.Select(x => new CustomerPair()
                {
                    Name = x.DisplayName,
                    Id = x.Id.ForceToInt(),
                }).ToList();

                List<Item> items = await qboUniversal.GetItems();

                List<InventoryItemPair> inventoryItemPairs =
                    items.Where(t => t.Type == ItemTypeEnum.Inventory)
                    .Select(x => new InventoryItemPair()
                    {
                        Name = x.Name,
                        Id = x.Id.ForceToInt(),
                    }).ToList();

                List<NonInventoryItemPair> nonInventoryItemPairs =
                   items.Where(t => t.Type == ItemTypeEnum.NonInventory)
                   .Select(x => new NonInventoryItemPair()
                   {
                       Name = x.Name,
                       Id = x.Id.ForceToInt(),
                   }).ToList();

                List<Account> accounts = await qboUniversal.GetAccounts();

                List<OtherCurrentLiabilitiesAccountPair> otherCurrentLiabilitiesAccountPairs =
                    accounts.Where(t => t.AccountType == AccountTypeEnum.OtherCurrentLiability)
                    .Select(x => new OtherCurrentLiabilitiesAccountPair()
                    {
                        Name = x.Name,
                        Id = x.Id.ForceToInt(),
                    }).ToList();

                CompanyInfo companyInfo = await qboUniversal.GetCompanyInfo();

                response.Customers = customerPairs;
                response.InventoryItems = inventoryItemPairs;
                response.NonInventoryItems = nonInventoryItemPairs;
                response.OtherCurrentLiabilitiesAccounts = otherCurrentLiabilitiesAccountPairs;
                
                if(companyInfo != null)
                {
                    response.UserCompanyInfo = new UserCompanyInfo { CompanyName = companyInfo.CompanyName };
                }

                return response;
            }
            catch (Exception ex)
            {
                try
                {
                    await _qboConnectionInfoDb.UpdateQboOAuthTokenStatusAsync(
                    new Command(_masterAccNum, _profileNum),
                    QboOAuthTokenStatus.Error);
                }
                catch (Exception ex_)
                {
                    throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex_);
                }

                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Create Items if User chooses the not existed Qbo Default Items.
        /// </summary>
        /// <param name="integrationSetting"></param>
        /// <returns></returns>
        private async Task<(bool, string)> HandleQboDefaultItems(IntergrationSettingReqType integrationSetting)
        {
            bool isSuccess = true;
            string errMsg = "";
            try
            {
                QboUniversal qboUniversal = await ConnectToQbo(_qboConnectionConfig);

                if (!String.IsNullOrEmpty(integrationSetting.QboDefaultItemName) &&
                    integrationSetting.QboDefaultItemId == 0)
                {
                    Item defaultInventoryItem =
                        await qboUniversal.CreateDefaultInventoryItem(integrationSetting.QboDefaultItemName);
                    if(defaultInventoryItem != null)
                    {
                        integrationSetting.QboDefaultItemId = defaultInventoryItem.Id.ForceToInt();
                    }
                    else
                    {
                        isSuccess = false;
                        errMsg += "There was an error when creating Default Inventory Item in Quickbooks Online. ";
                    }
                }

                if (!String.IsNullOrEmpty(integrationSetting.QboDiscountItemName) &&
                    integrationSetting.QboDiscountItemId == 0)
                {
                    Item defaultDiscountItem =
                        await qboUniversal.CreateDefaultNonInventoryItem(integrationSetting.QboDiscountItemName);
                    if (defaultDiscountItem != null)
                    {
                        integrationSetting.QboDiscountItemId = defaultDiscountItem.Id.ForceToInt();
                    }
                    else
                    {
                        isSuccess = false;
                        errMsg += "There was an error when creating Default Inventory Item in Quickbooks Online. ";
                    }
                }

                if (!String.IsNullOrEmpty(integrationSetting.QboShippingItemName) &&
                    integrationSetting.QboShippingItemId == 0)
                {
                    Item defaultShipppingItem =
                       await qboUniversal.CreateDefaultNonInventoryItem(integrationSetting.QboShippingItemName);
                    if (defaultShipppingItem != null)
                    {
                        integrationSetting.QboShippingItemId = defaultShipppingItem.Id.ForceToInt();
                    }
                    else
                    {
                        isSuccess = false;
                        errMsg += "There was an error when creating Default Inventory Item in Quickbooks Online. ";
                    }
                }

                if (!String.IsNullOrEmpty(integrationSetting.QboSalesTaxItemName) &&
                    integrationSetting.QboSalesTaxItemId == 0)
                {
                    // pass account id and account name
                    Item defaultTaxItem =
                       await qboUniversal.CreateDefaultNonInventoryItem(
                           integrationSetting.QboSalesTaxItemName, 
                           integrationSetting.QboSalesTaxAccName,
                           integrationSetting.QboSalesTaxItemId);

                    if (defaultTaxItem != null)
                    {
                        integrationSetting.QboSalesTaxItemId = defaultTaxItem.Id.ForceToInt();
                        integrationSetting.QboSalesTaxAccId = defaultTaxItem.IncomeAccountRef.Value.ForceToInt();
                    }
                    else
                    {
                        isSuccess = false;
                        errMsg += "There was an error when creating Default Inventory Item in Quickbooks Online. ";
                    }
                }

                return (isSuccess, errMsg);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private async Task<QboUniversal> ConnectToQbo(QboConnectionConfig qboConnectionConfig)
        {
            try
            {
                DataTable conInfoTb = await _qboConnectionInfoDb.GetConnectionInfoByCommandAsync(
                    new Command(_masterAccNum, _profileNum));

                List<QboConnectionInfo> qboConnectionInfos =
                    ComlexTypeConvertExtension.DatatableToList<QboConnectionInfo>(conInfoTb);

                QboConnectionInfo qboConnectionInfo = qboConnectionInfos.FirstOrDefault();

                if (qboConnectionInfos.Count != 1 || String.IsNullOrEmpty(qboConnectionInfo.AuthCode))
                {
                    return null;
                }

                // Decrypt sensitive credentials
                qboConnectionInfo.ClientId = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.ClientId, _dbConfig.CryptKey);
                qboConnectionInfo.ClientSecret = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.ClientSecret, _dbConfig.CryptKey);
                qboConnectionInfo.AuthCode = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.AuthCode, _dbConfig.CryptKey);
                qboConnectionInfo.RealmId = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.RealmId, _dbConfig.CryptKey);

                // Initialize Qbo connection.
                QboUniversal qboUniversal = await QboUniversal.CreateAsync(qboConnectionInfo, qboConnectionConfig);

                // Udpate Refresh Token in Database if it got updated during Qbo connection initailizaion.
                if (qboUniversal._qboConnectionTokenStatus.RefreshTokenStatus == ConnectionTokenStatus.Updated)
                {
                    await _qboConnectionInfoDb.UpdateQboRefreshTokenAsync(qboUniversal._qboConnectionInfo.RefreshToken,
                        qboUniversal._qboConnectionInfo.LastRefreshTokUpdate,
                        new Command(_masterAccNum, _profileNum)
                        );
                }
                // Udpate Access Token in Database if it got refreshed during Qbo connection initailizaion.
                if (qboUniversal._qboConnectionTokenStatus.AccessTokenStatus == ConnectionTokenStatus.Updated)
                {
                    await _qboConnectionInfoDb.UpdateQboAccessTokenAsync(qboUniversal._qboConnectionInfo.AccessToken,
                        qboUniversal._qboConnectionInfo.LastAccessTokUpdate,
                        new Command(_masterAccNum, _profileNum)
                        );
                }
                return qboUniversal;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<bool> IsQboIntegrationSettingExist()
        {
            try
            {
                return await _qboIntegrationSettingDb.ExistsIntegrationSettingAsync(
                    new Command(_masterAccNum, _profileNum));
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private async Task<(bool, string)> CheckAllPatchTargetsExist(IntegrationSettingApiReqType intgSettingApiReqType)
        {
            bool isAllExist = true;
            string errMsg = "";

            try
            {
                bool isIntgSettingExist = await IsQboIntegrationSettingExist();

                if (!isIntgSettingExist)
                {
                    isAllExist = false;
                    errMsg+= "The targeted Integration Setting does not exist. ";
                }
                else
                {
                    foreach (ChnlAccSettingReqType chnlAccSettingReqType in intgSettingApiReqType.ChnlAccSettings)
                    {
                        bool isCurChnlAccSettingsExist =
                            await _qboChnlAccSettingDb.ExistsChnlAccSettingAsync(
                                new Command(_masterAccNum, _profileNum), chnlAccSettingReqType.ChannelAccountNum);
                        if (!isCurChnlAccSettingsExist)
                        {
                            isAllExist = false;
                            errMsg += $" The targeted Channel Account Setting : {chnlAccSettingReqType.ChannelAccountName} " +
                                $"does not exist. ";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return (isAllExist, errMsg);
        }

        private (bool, string) ValidateReqBody(IntegrationSettingApiReqType intgSettingApiReq)
        {
            bool isValid = true;
            string errMsg = "";
            try
            {

                if (!ValidationUtility.Validate(intgSettingApiReq, out string apiReqTypeErrMsg))
                {
                    isValid = false;
                    errMsg += apiReqTypeErrMsg;
                }

                if(string.IsNullOrEmpty(apiReqTypeErrMsg))
                {
                    if(intgSettingApiReq.IntegrationSetting.MasterAccountNum != _masterAccNum ||
                        intgSettingApiReq.IntegrationSetting.ProfileNum != _profileNum)
                    {
                        isValid = false;
                        errMsg += (" MasterAccountNumber and ProfileNumber in Request Body " +
                            "don't match with Header for Integration Setting.");
                    }

                    if (!ValidationUtility.Validate(intgSettingApiReq.IntegrationSetting, out string intgSettingErrMsg))
                    {
                        isValid = false;
                        errMsg += (" " + intgSettingErrMsg);
                    }

                    // Validate conditionally required attributes 
                    if(intgSettingApiReq.IntegrationSetting.QboItemCreateRule == 0)
                    {
                        if(String.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboDefaultItemName))
                        {
                            isValid = false;
                            errMsg += ("QboDefaultItemName is required but not provided. ");
                        }
                    }

                    if (intgSettingApiReq.IntegrationSetting.ExportOrderAs == 2 || 
                        intgSettingApiReq.IntegrationSetting.ExportOrderAs == 3)
                    {
                        if (String.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboDiscountItemName))
                        {
                            isValid = false;
                            errMsg += ("QboDiscountItemName is required but not provided. ");
                        }

                        if (String.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboShippingItemName))
                        {
                            isValid = false;
                            errMsg += ("QboShippingItemName is required but not provided. ");
                        }
                    }

                    if (intgSettingApiReq.IntegrationSetting.SalesTaxExportRule == 1)
                    {
                        if (String.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboSalesTaxItemName))
                        {
                            isValid = false;
                            errMsg += ("QboSalesTaxItemName is required but not provided. ");
                        }
                        else
                        {
                            // User chose to create Digitbirdge Tax Non-Inventory Item, An account is required
                            if (intgSettingApiReq.IntegrationSetting.QboSalesTaxItemId == 0 && 
                                String.IsNullOrEmpty( intgSettingApiReq.IntegrationSetting.QboSalesTaxAccName ))
                            {
                                isValid = false;
                                errMsg += ("QboSalesTaxAccName is required but not provided. ");
                            }
                        }

                    }

                    foreach (ChnlAccSettingReqType chnlAccSettingPostType in intgSettingApiReq.ChnlAccSettings)
                    {
                        if (chnlAccSettingPostType.MasterAccountNum != _masterAccNum ||
                                chnlAccSettingPostType.ProfileNum != _profileNum)
                        {
                            isValid = false;
                            errMsg += (" MasterAccountNumber and ProfileNumber in Request Body " +
                                $"don't match with Header for Channel Account Setting: {chnlAccSettingPostType.ChannelName}.");
                        }

                        if (!ValidationUtility.Validate(chnlAccSettingPostType, out string chnlAccSettingErrMsg))
                        {
                            isValid = false;
                            errMsg += (" " + chnlAccSettingErrMsg);
                        }
                    }
                }

                if(!intgSettingApiReq.IntegrationSetting.ExportOrderToDate.Equals(DateTime.MinValue))
                {
                    if(intgSettingApiReq.IntegrationSetting.ExportOrderFromDate > intgSettingApiReq.IntegrationSetting.ExportOrderToDate)
                    {
                        isValid = false;
                        errMsg += "ExportOrderToDate must be later than ExportOrderFromDate.";
                    }
                    
                }
                
                if(intgSettingApiReq.IntegrationSetting.QboImportOrderAfterUpdateDate.Equals(DateTime.MinValue))
                {
                    isValid = false;
                    errMsg += " Invalid QboImportOrderAfterUpdateDate.";
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            
            return (isValid, errMsg);
        }


    }
}
