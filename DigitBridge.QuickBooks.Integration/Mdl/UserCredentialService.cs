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

namespace DigitBridge.QuickBooks.Integration.Mdl
{
    public class UserCredentialService
    {
        private QboDbConfig _dbConfig;
        private QboConnectionConfig _qboConnectionConfig;
        private QboConnectionInfoDb _qboConnectionInfoDb;
        private QboConnectionInfo _qboConnectionInfo;
        private MsSqlUniversal _msSql;
        private int _masterAccNum;
        private int _profileNum;

        private UserCredentialService(MsSqlUniversal msSql, QboDbConfig dbConfig,
            QboConnectionConfig qboConnectionConfig, QboConnectionInfoDb qboConnectionInfoDb,
            int masterAccNum, int profileNum)
        {
            _msSql = msSql;
            _dbConfig = dbConfig;
            _qboConnectionConfig = qboConnectionConfig;
            _qboConnectionInfoDb = qboConnectionInfoDb;
            _masterAccNum = masterAccNum;
            _profileNum = profileNum;
        }

        private UserCredentialService(MsSqlUniversal msSql, QboDbConfig dbConfig,
            QboConnectionConfig qboConnectionConfig, QboConnectionInfoDb qboConnectionInfoDb,
            QboConnectionInfo qboConnectionInfo)
        {
            _msSql = msSql;
            _dbConfig = dbConfig;
            _qboConnectionConfig = qboConnectionConfig;
            _qboConnectionInfoDb = qboConnectionInfoDb;
            _qboConnectionInfo = qboConnectionInfo;
        }

        public static async Task<UserCredentialService> CreateAsync(QboDbConfig dbConfig,
            QboConnectionConfig qboConnectionConfig, string requstState)
        {
            try
            {
                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                        dbConfig.QuickBooksDbConnectionString
                        , dbConfig.UseAzureManagedIdentity
                        , dbConfig.TokenProviderConnectionString
                        , dbConfig.AzureTenantId
                        );

                QboConnectionInfoDb qboConnectionInfoDb = new QboConnectionInfoDb(
                    dbConfig.QuickBooksDbConnectionInfoTableName, msSql, dbConfig.CryptKey);

                // get qboConnectionInfo by requestStates
                DataTable conInfoTb = await qboConnectionInfoDb.GetConnectionInfoByRequestStateAsync(requstState);

                List<QboConnectionInfo> qboConnectionInfos =
                    conInfoTb.DatatableToList<QboConnectionInfo>();

                if (qboConnectionInfos.Count != 1)
                {
                    return null;
                }

                QboConnectionInfo qboConnectionInfo = qboConnectionInfos.FirstOrDefault();

                var quickBooksOnlineUserCredential =
                    new UserCredentialService(
                        msSql, dbConfig, qboConnectionConfig,
                        qboConnectionInfoDb, qboConnectionInfo);

                return quickBooksOnlineUserCredential;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static async Task<UserCredentialService> CreateAsync(QboDbConfig dbConfig,
            QboConnectionConfig qboConnectionConfig, string masterAccNum, string profileNum)
        {
            try
            {
                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                        dbConfig.QuickBooksDbConnectionString
                        , dbConfig.UseAzureManagedIdentity
                        , dbConfig.TokenProviderConnectionString
                        , dbConfig.AzureTenantId
                        );

                QboConnectionInfoDb qboConnectionInfoDb = new QboConnectionInfoDb(
                    dbConfig.QuickBooksDbConnectionInfoTableName, msSql, dbConfig.CryptKey);

                var quickBooksOnlineUserCredential =
                    new UserCredentialService(
                        msSql, dbConfig, qboConnectionConfig,
                        qboConnectionInfoDb, masterAccNum.ForceToInt(), profileNum.ForceToInt()
                        );

                return quickBooksOnlineUserCredential;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
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
                // get qboConnectionInfo by requestStates
                DataTable conInfoTb = await _qboConnectionInfoDb.GetConnectionInfoByCommandAsync(
                    new Command(_masterAccNum, _profileNum));

                List<QboConnectionInfo> qboConnectionInfos =
                    conInfoTb.DatatableToList<QboConnectionInfo>();

                if (qboConnectionInfos.Count != 1)
                {
                    return (false, $"QboConnectionInfo not found for MasterAccountNum: {_masterAccNum} " +
                        $"and ProfileNum : {_profileNum}, it might has not been initialized yet.");
                }

                QboConnectionInfo qboConnectionInfo = qboConnectionInfos.FirstOrDefault();

                return (true, qboConnectionInfo.QboOAuthTokenStatus.ForceToTrimString());
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
                DataTable conInfoTb = await _qboConnectionInfoDb.GetConnectionInfoByCommandAsync(
                    new Command(_masterAccNum, _profileNum));

                List<QboConnectionInfo> qboConnectionInfos =
                    conInfoTb.DatatableToList<QboConnectionInfo>();

                if (qboConnectionInfos.Count < 1)
                {
                    return (false, $"QboConnectionInfo not found for MasterAccountNum: {_masterAccNum} " +
                        $"and ProfileNum : {_profileNum}, it might has not been initialized yet,");
                }

                await _qboConnectionInfoDb.DeleteConnectionInfoByCommandAsync(
                     new Command(_masterAccNum, _profileNum));

                // Get qboIntegrationSetting
                QboIntegrationSettingDb qboIntegrationSettingDb =
                    new QboIntegrationSettingDb(_dbConfig.QuickBooksDbIntegrationSettingTableName, _msSql);

                DataTable settingTb = await qboIntegrationSettingDb.GetIntegrationSettingAsync(new Command(_masterAccNum, _profileNum));

                List<QboIntegrationSetting> qboIntergrationSettings =
                    settingTb.DatatableToList<QboIntegrationSetting>();

                if (qboIntergrationSettings.Count > 0)
                {
                    await qboIntegrationSettingDb.DeleteIntegrationSettingAsync(
                        new Command(_masterAccNum, _profileNum));
                }

                // Get qboChnlAccSetting
                QboChnlAccSettingDb qboChnlAccSettingDb =
                    new QboChnlAccSettingDb(_dbConfig.QuickBooksChannelAccSettingTableName, _msSql);

                DataTable chnlSettingTb = await qboChnlAccSettingDb.GetChnlAccSettingsAsync(new Command(_masterAccNum, _profileNum));

                List<QboChnlAccSetting> qboChnlAccSettings =
                    chnlSettingTb.DatatableToList<QboChnlAccSetting>();

                int chnlAccSettingCount = qboChnlAccSettings.Count;

                if (chnlAccSettingCount > 0)
                {
                    await qboChnlAccSettingDb.DeleteChnlAccSettingsAsync(
                        new Command(_masterAccNum, _profileNum), chnlAccSettingCount);
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

            try
            {
                string clientId = CryptoUtility.DecrypTextTripleDES(_qboConnectionInfo.ClientId, _dbConfig.CryptKey);
                string clientSecret = CryptoUtility.DecrypTextTripleDES(_qboConnectionInfo.ClientSecret, _dbConfig.CryptKey);
                string accessToken = "";
                string refreshToken = "";

                // Get AccessToken and RefreshToken in QboOAuth
                (refreshToken, accessToken) =
                    await QboOAuth.GetBearerToken(_qboConnectionConfig, clientId, clientSecret, authCode);

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                {
                    errMsg = "Get QuickBooks Online Bearer Token Failed.";
                    return (false, errMsg);
                }

                // Store Tokens
                await _qboConnectionInfoDb.InitiateToken(
                    accessToken, refreshToken,
                    CryptoUtility.EncrypTextTripleDES(realmId, _dbConfig.CryptKey),
                    CryptoUtility.EncrypTextTripleDES(authCode, _dbConfig.CryptKey),
                    new Command(_qboConnectionInfo.MasterAccountNum, _qboConnectionInfo.ProfileNum)
                    );

                _qboConnectionInfo.RealmId = realmId;
                _qboConnectionInfo.AuthCode = authCode;
                _qboConnectionInfo.AccessToken = accessToken;
                _qboConnectionInfo.LastAccessTokUpdate = DateTime.UtcNow;
                _qboConnectionInfo.LastRefreshTokUpdate = DateTime.UtcNow;
                _qboConnectionInfo.RefreshToken = refreshToken;

                // Test Connectivity
                QboUniversal qboUniversal = await QboUniversal.CreateAsync(_qboConnectionInfo, _qboConnectionConfig);

                // Update initialStatus in QboConnectionInfo table in DP
                await _qboConnectionInfoDb.UpdateQboOAuthTokenStatusAsync(
                    new Command(_qboConnectionInfo.MasterAccountNum, _qboConnectionInfo.ProfileNum),
                    QboOAuthTokenStatus.Success);

                return (true, errMsg);
            }
            catch (Exception ex)
            {
                try
                {
                    await _qboConnectionInfoDb.UpdateQboOAuthTokenStatusAsync(
                    new Command(_qboConnectionInfo.MasterAccountNum, _qboConnectionInfo.ProfileNum),
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
        /// If get RedirectUrl successfully return ( true, url )
        /// Else return ( false, errorMsg )
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<(bool, string)> OAuthUrl()

        {
            try
            {
                //UserCredentilApiReqType oAuthRedirectUrlApiReq =
                //    JsonConvert.DeserializeObject<UserCredentilApiReqType>(requestBody);

                //(bool isReqBodyValid, string reqBodyErrMsg) =
                //    ValidateReqBody(oAuthRedirectUrlApiReq);

                //if (!isReqBodyValid)
                //{
                //    return (false, reqBodyErrMsg);
                //}

                string decryptedClientId =
                    CryptoUtility.DecrypTextTripleDES(_qboConnectionConfig.AppClientId, _dbConfig.CryptKey);
                string decryptedClientSecret =
                    CryptoUtility.DecrypTextTripleDES(_qboConnectionConfig.AppClientSecret, _dbConfig.CryptKey);

                string authorizationUrl = await QboOAuth.GetAuthorizationURL(_qboConnectionConfig, decryptedClientId,
                    decryptedClientSecret);

                Uri url = new Uri(authorizationUrl);
                string state = HttpUtility.ParseQueryString(url.Query).Get("state");

                if (string.IsNullOrEmpty(authorizationUrl) || string.IsNullOrEmpty(state))
                {
                    return (false, "Get redirect url to Quickbook Online login failed.");
                }

                bool isConfigExist =
                    await _qboConnectionInfoDb.ExistsConnectionInfoAsync(
                        new Command(_masterAccNum, _profileNum));

                if (isConfigExist)
                {
                    await _qboConnectionInfoDb.UpdateClientCredentialAsync(
                        new Command(_masterAccNum, _profileNum), _qboConnectionConfig.AppClientId, _qboConnectionConfig.AppClientSecret, state);
                }
                else
                {
                    // Add unique constrian
                    await _qboConnectionInfoDb.AddConnectionInfoAsync(
                        new Command(_masterAccNum, _profileNum), _qboConnectionConfig.AppClientId, _qboConnectionConfig.AppClientSecret, state);
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
