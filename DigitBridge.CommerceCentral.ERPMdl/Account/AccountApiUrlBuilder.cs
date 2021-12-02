using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class AccountApiUrlBuilder
    {
        public List<BuilderParameter> BuilderParameters { get; set; }
        public BuildAccountAction Action { get; set; }
        public string BaseEndpoint { get; set; }
        public string ApiSecurityCode { get; set; }
        public AccountApiUrlBuilder()
        {
            BuilderParameters = new List<BuilderParameter>();
        }
        public string Buid()
        {
            try
            {
                if (string.IsNullOrEmpty(BaseEndpoint))
                    throw new InvalidOperationException("Invalid BaseEndpoint! ");

                StringBuilder builder = new StringBuilder();
                builder.Append(this.BaseEndpoint);
                builder.Append(this.Action.ToString()); ;
                builder.Append("?code=" + this.ApiSecurityCode);
                foreach (var item in this.BuilderParameters)
                {
                    builder.Append("&" + item.Key + "=" + item.Value.ToString());
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
    public class BuilderParameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public BuilderParameter(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public enum BuildAccountAction
    {
        getProfileUsers,
        createProfileUser,
        deleteProfileUser,
        getProfileUserPermissions,
        patchProfileUserPermissions,
        sendProfileUserInvitation,
        permissionFunction,
        loginProfiles,
        profileChannelFunction,
        getDefinedPermissions,
        activateProfileUser,
        changeProfileUserName,
        getProfileChannelAccount
    }

    public static class MyAccountUrlBuilderEXT
    {
        public static AccountApiUrlBuilder SetBuilderParameter(this AccountApiUrlBuilder builder, BuilderParameter parameter)
        {
            builder.BuilderParameters.Add(parameter);
            return builder;
        }
        public static AccountApiUrlBuilder SetAction(this AccountApiUrlBuilder builder, BuildAccountAction action)
        {
            builder.Action = action;
            return builder;
        }
        public static AccountApiUrlBuilder SetBaseEndpoint(this AccountApiUrlBuilder builder, string baseEndpoint)
        {
            builder.BaseEndpoint = baseEndpoint;
            return builder;
        }
        public static AccountApiUrlBuilder SetApiSecurityCode(this AccountApiUrlBuilder builder, string securityCode)
        {
            builder.ApiSecurityCode = securityCode;
            return builder;
        }
    }
}