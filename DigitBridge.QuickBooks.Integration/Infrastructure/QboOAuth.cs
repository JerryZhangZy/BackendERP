using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration.Infrastructure
{
    public class QboOAuth
    {
        public static async Task<string> GetAuthorizationURLAsync()
        {
            // Instantiate object
            OAuth2Client auth2Client = new OAuth2Client(MyAppSetting.AppClientId, MyAppSetting.AppClientSecret
                , MyAppSetting.RedirectUrl, MyAppSetting.Environment);

            //Prepare scopes
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.OpenId);
            scopes.Add(OidcScopes.Email);
            scopes.Add(OidcScopes.Accounting);
            scopes.Add(OidcScopes.Profile);
            scopes.Add(OidcScopes.Phone);
            scopes.Add(OidcScopes.Address);

            //Get the authorization URL
            return auth2Client.GetAuthorizationURL(scopes);
        }

        public static async Task<(string, string)> GetBearerTokenAsync(string authCode)
        {
            string refreshToken = "";
            string accessToken = "";
            // Instantiate object
            OAuth2Client auth2Client = new OAuth2Client(MyAppSetting.AppClientId, MyAppSetting.AppClientSecret
                , MyAppSetting.RedirectUrl, MyAppSetting.Environment);

            // Get OAuth2 Bearer token
            var tokenResponse = await auth2Client.GetBearerTokenAsync(authCode);

            // Retrieve access_token and refresh_token
            accessToken = tokenResponse.AccessToken;
            refreshToken = tokenResponse.RefreshToken;

            return (refreshToken, accessToken);
        }

    }
}
