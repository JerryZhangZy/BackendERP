using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;
using DigitBridge.QuickBooks.Integration.Model;
using Intuit.Ipp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration.Mdl
{
    public class IntegrationSettingService
    {
        private QuickBooksConnectionInfoService quickBooksConnectionInfoService;
        private QuickBooksSettingInfoService quickBooksSettingInfoService;
        private IDataBaseFactory dbFactory;
        private IPayload payload;

        private IntegrationSettingService(IDataBaseFactory dataBaseFactory,IPayload pl)
        {
            payload = pl;
            dbFactory = dataBaseFactory;
            quickBooksConnectionInfoService = new QuickBooksConnectionInfoService(dbFactory);
            quickBooksSettingInfoService = new QuickBooksSettingInfoService(dbFactory);
        }

        public static async Task<IntegrationSettingService> CreateAsync(IDataBaseFactory dataBaseFactory, IPayload payload)
        {
                return new IntegrationSettingService(dataBaseFactory,payload);
        }

        public async Task<IntegrationSettingApiRespondType> GetIntegrationSetting()
        {
            var reponse = new IntegrationSettingApiRespondType();
            try
            {
                reponse.SettingInfo = quickBooksSettingInfoService.GetDataByPayload(payload).FirstOrDefault();

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
                var settingReqType =JsonConvert.DeserializeObject<IntegrationSettingApiReqType>(requestBody);

                (bool isReqBodyValid, string reqBodyErrMsg) = ValidateReqBody(settingReqType);

                if (!isReqBodyValid)
                {
                    return (false, reqBodyErrMsg);
                }

                await HandleQboDefaultItems(settingReqType.IntegrationSetting);

                //TODO:
//                quickBooksSettingInfoService.

//                await _qboIntegrationSettingDb.AddIntegrationSettingAsync(
//                    settingReqType.IntegrationSetting.ObjectToDataTable(
//)
//                    );

//                await _qboChnlAccSettingDb.AddChnlAccSettingAsync(
//                   settingReqType.ChnlAccSettings.ListToDataTable(
//)
//                   );

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

                //// [ExportOrderFromDate] is only allowed for set once for now.
                //DateTime exportOrderFromDate = await _qboIntegrationSettingDb.GetExportOrderFromDate(new Command(_masterAccNum, _profileNum));

                //if (settingReqType.IntegrationSetting.ExportOrderFromDate == default)
                //{
                //    settingReqType.IntegrationSetting.ExportOrderFromDate = exportOrderFromDate;
                //}
                //else
                //{
                //    if (!settingReqType.IntegrationSetting.ExportOrderFromDate.Equals(exportOrderFromDate))
                //    {
                //        return (false, "The exportOrderFromDate is only allowed for set once, an inqury is required for updating. ");
                //    }
                //}

                //await HandleQboDefaultItems(settingReqType.IntegrationSetting);

                //await _qboIntegrationSettingDb.UpdateIntegrationSettingAsync(
                //    settingReqType.IntegrationSetting.ObjectToDataTable(),
                //    new Command
                //        (
                //            settingReqType.IntegrationSetting.MasterAccountNum,
                //            settingReqType.IntegrationSetting.ProfileNum
                //        )
                //    );

                //foreach (ChnlAccSettingReqType chnlAccSettingReq in settingReqType.ChnlAccSettings)
                //{
                //    await _qboChnlAccSettingDb.UpdateChnlAccSettingAsync(
                //       chnlAccSettingReq.ObjectToDataTable(),
                //       chnlAccSettingReq.MasterAccountNum,
                //       chnlAccSettingReq.ProfileNum,
                //       chnlAccSettingReq.ChannelAccountNum
                //       );
                //}

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
                var customerService = new QboCustomerService();
                var inventoryService = new QboInventoryService();

                List<Customer> customers = await customerService.GetCustomersAsync();

                List<CustomerPair> customerPairs = customers.Select(x => new CustomerPair()
                {
                    Name = x.DisplayName,
                    Id = x.Id.ForceToInt(),
                }).ToList();

                List<Item> items = await inventoryService.GetItemsAsync();

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

                List<Account> accounts = await inventoryService.GetAccountsAsync();

                List<OtherCurrentLiabilitiesAccountPair> otherCurrentLiabilitiesAccountPairs =
                    accounts.Where(t => t.AccountType == AccountTypeEnum.OtherCurrentLiability)
                    .Select(x => new OtherCurrentLiabilitiesAccountPair()
                    {
                        Name = x.Name,
                        Id = x.Id.ForceToInt(),
                    }).ToList();

                CompanyInfo companyInfo = await customerService.GetCompanyInfoAsync();

                response.Customers = customerPairs;
                response.InventoryItems = inventoryItemPairs;
                response.NonInventoryItems = nonInventoryItemPairs;
                response.OtherCurrentLiabilitiesAccounts = otherCurrentLiabilitiesAccountPairs;

                if (companyInfo != null)
                {
                    response.UserCompanyInfo = new UserCompanyInfo { CompanyName = companyInfo.CompanyName };
                }

                return response;
            }
            catch (Exception ex)
            {
                try
                {
                    //await _qboConnectionInfoDb.UpdateQboOAuthTokenStatusAsync(
                    //new Command(_masterAccNum, _profileNum),
                    //QboOAuthTokenStatus.Error);
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
                var inventoryService = new QboInventoryService();
                if (!string.IsNullOrEmpty(integrationSetting.QboDefaultItemName) &&
                    integrationSetting.QboDefaultItemId == 0)
                {
                    Item defaultInventoryItem =
                        await inventoryService.CreateDefaultInventoryItemAsync(integrationSetting.QboDefaultItemName);
                    if (defaultInventoryItem != null)
                    {
                        integrationSetting.QboDefaultItemId = defaultInventoryItem.Id.ForceToInt();
                    }
                    else
                    {
                        isSuccess = false;
                        errMsg += "There was an error when creating Default Inventory Item in Quickbooks Online. ";
                    }
                }

                if (!string.IsNullOrEmpty(integrationSetting.QboDiscountItemName) &&
                    integrationSetting.QboDiscountItemId == 0)
                {
                    Item defaultDiscountItem =
                        await inventoryService.CreateDefaultNonInventoryItemAsync(integrationSetting.QboDiscountItemName);
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

                if (!string.IsNullOrEmpty(integrationSetting.QboShippingItemName) &&
                    integrationSetting.QboShippingItemId == 0)
                {
                    Item defaultShipppingItem =
                       await inventoryService.CreateDefaultNonInventoryItemAsync(integrationSetting.QboShippingItemName);
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

                if (!string.IsNullOrEmpty(integrationSetting.QboSalesTaxItemName) &&
                    integrationSetting.QboSalesTaxItemId == 0)
                {
                    // pass account id and account name
                    Item defaultTaxItem =
                       await inventoryService.CreateDefaultNonInventoryItemAsync(
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

        private async Task<(bool, string)> CheckAllPatchTargetsExist(IntegrationSettingApiReqType intgSettingApiReqType)
        {
            bool isAllExist = true;
            string errMsg = "";

            try
            {
                var settingInfo = quickBooksSettingInfoService.GetDataByPayload(payload).FirstOrDefault();
                bool isIntgSettingExist = settingInfo != null;

                if (!isIntgSettingExist)
                {
                    isAllExist = false;
                    errMsg += "The targeted Integration Setting does not exist. ";
                }
                else
                {
                    foreach (ChnlAccSettingReqType chnlAccSettingReqType in intgSettingApiReqType.ChnlAccSettings)
                    {
                        bool isCurChnlAccSettingsExist = settingInfo.QuickBooksChnlAccSetting.Any(r => r.ChannelAccountNum == chnlAccSettingReqType.ChannelAccountNum);
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

                if (string.IsNullOrEmpty(apiReqTypeErrMsg))
                {
                    if (intgSettingApiReq.IntegrationSetting.MasterAccountNum != payload.MasterAccountNum ||
                        intgSettingApiReq.IntegrationSetting.ProfileNum != payload.ProfileNum)
                    {
                        isValid = false;
                        errMsg += " MasterAccountNumber and ProfileNumber in Request Body " +
                            "don't match with Header for Integration Setting.";
                    }

                    if (!ValidationUtility.Validate(intgSettingApiReq.IntegrationSetting, out string intgSettingErrMsg))
                    {
                        isValid = false;
                        errMsg += " " + intgSettingErrMsg;
                    }

                    // Validate conditionally required attributes 
                    if (intgSettingApiReq.IntegrationSetting.QboItemCreateRule == 0)
                    {
                        if (string.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboDefaultItemName))
                        {
                            isValid = false;
                            errMsg += "QboDefaultItemName is required but not provided. ";
                        }
                    }

                    if (intgSettingApiReq.IntegrationSetting.ExportOrderAs == 2 ||
                        intgSettingApiReq.IntegrationSetting.ExportOrderAs == 3)
                    {
                        if (string.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboDiscountItemName))
                        {
                            isValid = false;
                            errMsg += "QboDiscountItemName is required but not provided. ";
                        }

                        if (string.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboShippingItemName))
                        {
                            isValid = false;
                            errMsg += "QboShippingItemName is required but not provided. ";
                        }
                    }

                    if (intgSettingApiReq.IntegrationSetting.SalesTaxExportRule == 1)
                    {
                        if (string.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboSalesTaxItemName))
                        {
                            isValid = false;
                            errMsg += "QboSalesTaxItemName is required but not provided. ";
                        }
                        else
                        {
                            // User chose to create Digitbirdge Tax Non-Inventory Item, An account is required
                            if (intgSettingApiReq.IntegrationSetting.QboSalesTaxItemId == 0 &&
                                string.IsNullOrEmpty(intgSettingApiReq.IntegrationSetting.QboSalesTaxAccName))
                            {
                                isValid = false;
                                errMsg += "QboSalesTaxAccName is required but not provided. ";
                            }
                        }

                    }

                    foreach (ChnlAccSettingReqType chnlAccSettingPostType in intgSettingApiReq.ChnlAccSettings)
                    {
                        if (chnlAccSettingPostType.MasterAccountNum != payload.MasterAccountNum ||
                                chnlAccSettingPostType.ProfileNum != payload.ProfileNum)
                        {
                            isValid = false;
                            errMsg += " MasterAccountNumber and ProfileNumber in Request Body " +
                                $"don't match with Header for Channel Account Setting: {chnlAccSettingPostType.ChannelName}.";
                        }

                        if (!ValidationUtility.Validate(chnlAccSettingPostType, out string chnlAccSettingErrMsg))
                        {
                            isValid = false;
                            errMsg += " " + chnlAccSettingErrMsg;
                        }
                    }
                }

                if (!intgSettingApiReq.IntegrationSetting.ExportOrderToDate.Equals(DateTime.MinValue))
                {
                    if (intgSettingApiReq.IntegrationSetting.ExportOrderFromDate > intgSettingApiReq.IntegrationSetting.ExportOrderToDate)
                    {
                        isValid = false;
                        errMsg += "ExportOrderToDate must be later than ExportOrderFromDate.";
                    }

                }

                if (intgSettingApiReq.IntegrationSetting.QboImportOrderAfterUpdateDate.Equals(DateTime.MinValue))
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
