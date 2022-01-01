using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using DigitBridge.Base.Utility;
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class AccountApiService
    {
        public const string BackdoorMode = "BackdoorMode";
        public const string BackdoorModePassword = "BackdoorModePassword";
        public const string BackdoorModeEmail = "BackdoorModeEmail";

        private static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        public static ClaimsPrincipal EngineerClaimsPrincipal(AccountPayload payload)
        {
            var claimsPrincipal = payload.ClaimsPrincipal;
            var useBackdoor = MySingletonAppSetting.BackdoorMode;
            if (useBackdoor)
            {
                var email = GetBackdoorEmail(payload);
                if (!string.IsNullOrEmpty(email))
                {
                    ClaimsPrincipal newClaimsPrincipal = new ClaimsPrincipal();
                    ClaimsIdentity newIdentity = new ClaimsIdentity();
                    newIdentity.AddClaim(new Claim("emails", email));
                    newClaimsPrincipal.AddIdentity(newIdentity);
                    return newClaimsPrincipal;
                }
            }

            //for local testing with token
            var token = payload.AccessToken;
            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwt = new JwtSecurityToken(token);
                var claim = jwt.Claims.ToList().Find(x => x.Type == "emails");
                if (claim != null)
                {
                    ClaimsPrincipal newClaimsPrincipal = new ClaimsPrincipal();
                    ClaimsIdentity newIdentity = new ClaimsIdentity();
                    newIdentity.AddClaim(claim);
                    newClaimsPrincipal.AddIdentity(newIdentity);
                    return newClaimsPrincipal;
                }
            }

            return claimsPrincipal;
        }


        public static string GetUserEmail(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal != null ? claimsPrincipal.FindFirst("emails")?.Value ?? string.Empty : string.Empty;
        }

        public static string GetBackdoorEmail(AccountPayload payload)
            => (!MySingletonAppSetting.BackdoorMode || MySingletonAppSetting.BackdoorModePassword != payload.BackdoorModePassword)
                ? string.Empty
                : payload.BackdoorModeEmail;

        #region load profiles and Permission, use to user login and logout
        public static async Task<IList<UserProfile>> GetUserProfiles(IPayload payload)
        {
            var masterAccountNum = payload.MasterAccountNum;
            var profileNum = payload.ProfileNum;
            var claimsPrincipal = payload.ClaimsPrincipal;
            var email = GetUserEmail(claimsPrincipal);
            var token = payload.AccessToken;

            var accountApi = new AccountApiUrlBuilder()
                .SetAction(BuildAccountAction.loginProfiles)
                .SetBaseEndpoint(MySingletonAppSetting.AccountApiEndPoint)
                .SetApiSecurityCode(MySingletonAppSetting.AccountApiCode);

            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            accountApi.SetBuilderParameter(new BuilderParameter(BackdoorModeEmail, WebUtility.UrlEncode(email)));
            accountApi.SetBuilderParameter(new BuilderParameter(BackdoorModePassword, MySingletonAppSetting.BackdoorModePassword));

            var response = await client.GetAsync(accountApi.Buid());
            string result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            try
            {
                //var jsonArray = JArray.Parse(result);
                //var profiles = jsonArray.MapToList<UserProfile>();
                var profiles = result.JsonToObject<IList<UserProfile>>().ToList();
                if (profiles == null || profiles.Count == 0)
                    return null;
                return profiles;
            }
            catch (Exception ex)
            {
                return null;
                //throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static async Task<bool> LoadUserProfiles(AccountPayload payload)
        {
            var profiles = await GetUserProfiles(payload);
            if (profiles == null || profiles.Count == 0)
                payload.ReturnError($"Profiles not found.");
            payload.Profiles = profiles.ToLoginProfileModel();
            payload.Success = true;
            return true;
        }

        public static async Task<IList<UserPermission>> GetUserPermissions(IPayload payload)
        {
            var masterAccountNum = payload.MasterAccountNum;
            var profileNum = payload.ProfileNum;
            var claimsPrincipal = payload.ClaimsPrincipal;
            var email = GetUserEmail(claimsPrincipal);
            var token = payload.AccessToken;
            var backdoorPassword = payload.BackdoorModePassword;

            var accountApi = new AccountApiUrlBuilder()
                .SetAction(BuildAccountAction.getProfileUserPermissions)
                .SetBaseEndpoint(MySingletonAppSetting.AccountApiEndPoint)
                .SetApiSecurityCode(MySingletonAppSetting.AccountApiCode);

            accountApi.SetBuilderParameter(new BuilderParameter("masterAccountNum", masterAccountNum));
            accountApi.SetBuilderParameter(new BuilderParameter("profileNum", profileNum));

            accountApi.SetBuilderParameter(new BuilderParameter(BackdoorModeEmail, WebUtility.UrlEncode(email)));
            accountApi.SetBuilderParameter(new BuilderParameter(BackdoorModePassword, MySingletonAppSetting.BackdoorModePassword));
            accountApi.SetBuilderParameter(new BuilderParameter("email", WebUtility.UrlEncode(email)));

            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync(accountApi.Buid());
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            try
            {
                //var jsonArray = JArray.Parse(result);
                //var permissions = jsonArray.MapToList<UserPermission>();
                //return Result<List<UserPermission>>.Success(permissions);

                var permissions = result.JsonToObject<IList<UserPermission>>().ToList();
                if (permissions == null || permissions.Count == 0)
                    return null;
                return permissions;
            }
            catch (Exception ex)
            {
                return null;
                //throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        public static async Task<bool> LoadUserPermissions(AccountPayload payload)
        {
            var permissions = await GetUserPermissions(payload);
            if (permissions == null || permissions.Count == 0)
                payload.ReturnError($"UserPermission not found.");
            payload.UserPermissions = permissions;
            payload.Success = true;
            return true;
        }

        public static async Task<bool> Logout(AccountPayload payload)
        {
            var masterAccountNum = payload.MasterAccountNum;
            var profileNum = payload.ProfileNum;
            var claimsPrincipal = payload.ClaimsPrincipal;
            var email = GetUserEmail(claimsPrincipal);
            var token = payload.AccessToken;

            string key = $"{email.ToUpper()}-{masterAccountNum}-{profileNum}";
            UserProfile userProfile = null;
            if (Cache.TryGetValue<UserProfile>(key, out userProfile))
            {
                Cache.Remove(key);
            }
            payload.UserProfile = userProfile;
            payload.Success = true;
            return true;
        }

        #endregion

        #region get single profile with Permissions, use to Assert Security Permission in other API
        public static async Task<UserProfile> GetUserProfile(IPayload payload)
        {
            var masterAccountNum = payload.MasterAccountNum;
            var profileNum = payload.ProfileNum;
            var claimsPrincipal = payload.ClaimsPrincipal;
            var email = GetUserEmail(claimsPrincipal);
            var token = payload.AccessToken;

            if (string.IsNullOrEmpty(email))
                return null; 

            string key = $"{email.ToUpper()}-{masterAccountNum}-{profileNum}";
            UserProfile userProfile = null;
            if (Cache.TryGetValue<UserProfile>(key, out userProfile))
                return userProfile;

            var profiles = await GetUserProfiles(payload);
            if (profiles == null)
                return null;

            userProfile = profiles.FirstOrDefault(x => 
                x.Email.EqualsIgnoreSpace(email) && x.MasterAccountNum == masterAccountNum && x.ProfileNum == profileNum);

            if (userProfile == null)
                return null;

             var permissions = (await GetUserPermissions(payload)).ToList();
            if (permissions != null)
                userProfile.Permissions = permissions;

            Cache.Set(key, userProfile, TimeSpan.FromHours(3));
            return userProfile;
        }
        #endregion get single profile with Permissions

    }
}
