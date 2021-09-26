using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;
using System.Web;
using System.Data;
using System.Linq;
using DigitBridge.QuickBooks.Integration.Db.Infrastructure;
using DigitBridge.QuickBooks.Integration.Connection.Model;
using DigitBridge.QuickBooks.Integration.Infrastructure;
using DigitBridge.QuickBooks.Integration.Model;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.QuickBooks.Integration.Mdl
{
    public class UserCredentialService
    {
        protected QuickBooksConnectionInfoService quickBooksConnectionInfoService;
        protected QuickBooksSettingInfoService quickBooksSettingInfoService;
        protected IPayload payload;
        protected IDataBaseFactory dbFactory;
        //private QboConnectionConfig _qboConnectionConfig;
        //private QboConnectionInfo _qboConnectionInfo;
        //private MsSqlUniversal _msSql;
        //private int _masterAccNum;
        //private int _profileNum;

        private UserCredentialService(IPayload pl,IDataBaseFactory dataBaseFactory)
        {
            payload = pl;
            dbFactory = dataBaseFactory;
            quickBooksConnectionInfoService = new QuickBooksConnectionInfoService(dbFactory);
            quickBooksSettingInfoService = new QuickBooksSettingInfoService(dbFactory);
        }


        public static async Task<UserCredentialService> CreateAsync(IPayload pl, IDataBaseFactory dataBaseFactory)
        {
            return new UserCredentialService(pl, dataBaseFactory);
        }

        /// <summary>
        /// If get QboConnectionInfo successfully return ( true, QboAuthTokenStatus.toString() )
        /// Else return ( false, errorMsg )
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> GetTokenStatus()
        {
            try
            {
                var result = quickBooksConnectionInfoService.GetDataByPayload(payload);

                if (result.Count != 1)
                {
                    return (false, $"QboConnectionInfo not found for MasterAccountNum: {payload.MasterAccountNum} " +
                        $"and ProfileNum : {payload.ProfileNum}, it might has not been initialized yet.");
                }

                var connectionInfo = result.FirstOrDefault();

                return (true, connectionInfo.QuickBooksConnectionInfo.QboOAuthTokenStatus.ForceToTrimString());
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Change QboSetting Status to Inactive and QboOAuthTokenStatus to Disconnected
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> DisconnectUser()
        {
            try
            {
                // Get qboConnectionInfo 
                var connections = quickBooksConnectionInfoService.GetDataByPayload(payload);

                if (connections.Count < 1)
                {
                    return (false, $"QboConnectionInfo not found for MasterAccountNum: {payload.MasterAccountNum} " +
                        $"and ProfileNum : {payload.ProfileNum}, it might has not been initialized yet,");
                }
                foreach (var conn in connections)
                {
                    await quickBooksConnectionInfoService.DeleteAsync(conn.QuickBooksConnectionInfo.RowNum);
                }

                var settings = quickBooksSettingInfoService.GetDataByPayload(payload);

                foreach (var setting in settings)
                {
                    await quickBooksSettingInfoService.DeleteAsync(setting.QuickBooksIntegrationSetting.RowNum);
                }

                return (true, "");
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<(bool, string)> HandleTokens(string realmId, string authCode)
        {
            string errMsg = "";
            var connectInfo = quickBooksConnectionInfoService.GetDataByPayload(payload).FirstOrDefault();
            var accessToken = "";
            var refreshToken = "";

            // Get AccessToken and RefreshToken in QboOAuth
            (refreshToken, accessToken) =
                await QboOAuth.GetBearerTokenAsync(authCode);

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                errMsg = "Get QuickBooks Online Bearer Token Failed.";
                return (false, errMsg);
            }

            // Store Tokens
            connectInfo.QuickBooksConnectionInfo.RealmId = realmId;
            connectInfo.QuickBooksConnectionInfo.AuthCode = authCode;
            connectInfo.QuickBooksConnectionInfo.AccessToken = accessToken;
            connectInfo.QuickBooksConnectionInfo.LastAccessTokUpdate = DateTime.UtcNow;
            connectInfo.QuickBooksConnectionInfo.LastRefreshTokUpdate = DateTime.UtcNow;
            connectInfo.QuickBooksConnectionInfo.RefreshToken = refreshToken;

            // Test Connectivity
            //QboUniversal qboUniversal = await QboUniversal.CreateAsync(_qboConnectionInfo, _qboConnectionConfig);

            // Update initialStatus in QboConnectionInfo table in DP
            quickBooksConnectionInfoService.AttachData(connectInfo);
            await quickBooksConnectionInfoService.SaveDataAsync();
            return (true, errMsg);
        }

        /// <summary>
        /// If get RedirectUrl successfully return ( true, url )
        /// Else return ( false, errorMsg )
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<(bool, string)> OAuthUrl()

        {
            try
            {

                string decryptedClientId =
                    CryptoUtility.DecrypTextTripleDES(MyAppSetting.AppClientId, MyAppSetting.CryptKey);
                string decryptedClientSecret =
                    CryptoUtility.DecrypTextTripleDES(MyAppSetting.AppClientSecret, MyAppSetting.CryptKey);

                string authorizationUrl = await QboOAuth.GetAuthorizationURLAsync();

                Uri url = new Uri(authorizationUrl);
                string state = HttpUtility.ParseQueryString(url.Query).Get("state");

                if (string.IsNullOrEmpty(authorizationUrl) || string.IsNullOrEmpty(state))
                {
                    return (false, "Get redirect url to Quickbook Online login failed.");
                }
                var connectionInfos= quickBooksConnectionInfoService.GetDataByPayload(payload);

                bool isConfigExist = connectionInfos.Any();

                if (isConfigExist)
                {
                    var first = connectionInfos.First();
                    first.QuickBooksConnectionInfo.RequestState = state;
                    quickBooksConnectionInfoService.AttachData(first);
                    await quickBooksConnectionInfoService.SaveDataAsync();
                    //await _qboConnectionInfoDb.UpdateClientCredentialAsync(
                    //    new Command(_masterAccNum, _profileNum), _qboConnectionConfig.AppClientId, _qboConnectionConfig.AppClientSecret, state);
                }
                else
                {
                    // Add unique constrian
                    var connection = new QuickBooksConnectionInfoData()
                    {
                        QuickBooksConnectionInfo = new QuickBooksConnectionInfo
                        {
                            MasterAccountNum = payload.MasterAccountNum,
                            DatabaseNum = payload.DatabaseNum,
                            ProfileNum = payload.ProfileNum,
                            ClientId = MyAppSetting.AppClientId,
                            ClientSecret = MyAppSetting.AppClientSecret,
                            RequestState = state,
                            ConnectionUuid = Guid.NewGuid().ToString()
                        }
                    };
                    quickBooksConnectionInfoService.AttachData(connection);
                    await quickBooksConnectionInfoService.SaveDataAsync();
                }

                return (true, authorizationUrl);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        //private (bool, string) ValidateReqBody(UserCredentilApiReqType oAuthRedirectUrlApiReq)
        //{
        //    bool isValid = true;
        //    string errMsg = "";
        //    try
        //    {
        //        if (!ValidationUtility.Validate(oAuthRedirectUrlApiReq, out string apiReqTypeErrMsg))
        //        {
        //            isValid = false;
        //            errMsg += apiReqTypeErrMsg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
        //    }

        //    return (isValid, errMsg);
        //}

    }
}
