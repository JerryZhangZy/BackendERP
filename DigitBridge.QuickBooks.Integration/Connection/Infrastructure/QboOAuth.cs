using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration.Connection.Infrastructure
{
    public class QboOAuth
    {
        public static async Task<string> GetAuthorizationURL(QboConnectionConfig qboConnectionConfig,
            string clientId, string clientSecret)
        {
            string authUrl = "";
            try
            {
                // Instantiate object
                OAuth2Client auth2Client = new OAuth2Client(clientId, clientSecret
                    , qboConnectionConfig.RedirectUrl, qboConnectionConfig.Environment);

                //Prepare scopes
                List<OidcScopes> scopes = new List<OidcScopes>();
                scopes.Add(OidcScopes.OpenId);
                scopes.Add(OidcScopes.Email);
                scopes.Add(OidcScopes.Accounting);
                scopes.Add(OidcScopes.Profile);
                scopes.Add(OidcScopes.Phone);
                scopes.Add(OidcScopes.Address);

                //Get the authorization URL
                authUrl = auth2Client.GetAuthorizationURL(scopes);

                return authUrl;
            }
            catch (Exception ex)
            {
                string additionalMsg = "Qbo getOAuthRedirectUrl Error." + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        public static async Task<(string, string)> GetBearerToken(QboConnectionConfig qboConnectionConfig,
            string clientId, string clientSecret, string authCode)
        {
            string refreshToken = "";
            string accessToken = "";
            try
            {
                // Instantiate object
                OAuth2Client auth2Client = new OAuth2Client(clientId, clientSecret
                    , qboConnectionConfig.RedirectUrl, qboConnectionConfig.Environment);

                // Get OAuth2 Bearer token
                var tokenResponse = await auth2Client.GetBearerTokenAsync(authCode);

                // Retrieve access_token and refresh_token
                accessToken = tokenResponse.AccessToken;
                refreshToken = tokenResponse.RefreshToken;

                return (refreshToken, accessToken);
            }
            catch (Exception ex)
            {
                string additionalMsg = "Qbo getOAuthRedirectUrl Error." + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

    }
}
