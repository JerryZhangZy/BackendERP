using System;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using System.Web;
using System.Linq;
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
            if (!await quickBooksConnectionInfoService.GetByPayloadAsync(payload))
            {
                return (false, $"QboConnectionInfo not found for MasterAccountNum: {payload.MasterAccountNum} " +
                    $"and ProfileNum : {payload.ProfileNum}, it might has not been initialized yet.");
            }
            else
            {
                var connectionInfo = quickBooksConnectionInfoService.Data;
                return (true, connectionInfo.QuickBooksConnectionInfo.QboOAuthTokenStatus.ForceToTrimString());
            }
        }

        /// <summary>
        /// Change QboSetting Status to Inactive and QboOAuthTokenStatus to Disconnected
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> DisconnectUser()
        {
            if (!await quickBooksConnectionInfoService.GetByPayloadAsync(payload))
            {
                return (false, $"QboConnectionInfo not found for MasterAccountNum: {payload.MasterAccountNum} " +
                    $"and ProfileNum : {payload.ProfileNum}, it might has not been initialized yet,");
            }
            else
            {
                await quickBooksConnectionInfoService.DeleteDataAsync();
            }

            if (!await quickBooksSettingInfoService.GetByPayloadAsync(payload))
            {
                await quickBooksSettingInfoService.DeleteDataAsync();
            }
            return (true, "");
        }

        public async Task<(bool, string)> HandleTokens(string realmId, string authCode,string requestState)
        {
            string errMsg = "";
            if (await quickBooksConnectionInfoService.GetByRequestStateAsync(requestState))
            {
                var data = quickBooksConnectionInfoService.Data;

                // Get AccessToken and RefreshToken in QboOAuth
                string accessToken;
                string refreshToken;
                (refreshToken, accessToken) = await QboOAuth.GetBearerTokenAsync(authCode);
                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                {
                    errMsg = "Get QuickBooks Online Bearer Token Failed.";
                    return (false, errMsg);
                }
                // Store Tokens
                data.QuickBooksConnectionInfo.RealmId = realmId;
                data.QuickBooksConnectionInfo.AuthCode = authCode;
                data.QuickBooksConnectionInfo.AccessToken = accessToken;
                data.QuickBooksConnectionInfo.LastAccessTokUpdate = DateTime.UtcNow;
                data.QuickBooksConnectionInfo.LastRefreshTokUpdate = DateTime.UtcNow;
                data.QuickBooksConnectionInfo.RefreshToken = refreshToken;

                // Test Connectivity
                //QboUniversal qboUniversal = await QboUniversal.CreateAsync(_qboConnectionInfo, _qboConnectionConfig);

                // Update initialStatus in QboConnectionInfo table in DP
                quickBooksConnectionInfoService.AttachData(data);
                await quickBooksConnectionInfoService.SaveDataAsync();
                return (true, errMsg);
            }
            errMsg = "QuickBooksConnectionInfo no founds.";
            return (false, errMsg);
        }

        /// <summary>
        /// If get RedirectUrl successfully return ( true, url )
        /// Else return ( false, errorMsg )
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<(bool, string)> OAuthUrl()
        {
            string authorizationUrl = await QboOAuth.GetAuthorizationURLAsync();

            //Get State
            Uri url = new Uri(authorizationUrl);
            string state = HttpUtility.ParseQueryString(url.Query).Get("state");
            if (string.IsNullOrEmpty(authorizationUrl) || string.IsNullOrEmpty(state))
            {
                return (false, "Get redirect url to Quickbook Online login failed.");
            }
            //Create info with state or update info state
            await quickBooksConnectionInfoService.UpdateConnectionInfoStateAsync(payload, state);

            return (true, authorizationUrl);
        }
    }
}
