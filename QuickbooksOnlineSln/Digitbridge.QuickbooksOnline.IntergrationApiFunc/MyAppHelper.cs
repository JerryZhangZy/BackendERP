using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using UneedgoHelper.DotNet.Common;

namespace Digitbridge.QuickbooksOnline.IntegrationApiFunc
{
    public class MyAppHelper
    {
        public static bool VerifyAppPermission(HttpRequest req, ExecutionContext context,
           string appPermssionName, string appPermissionLevel, out string outMessage)
        {
            outMessage = "";
            try
            {//TODO: implement the logic if necessary

                bool hasPermission = true;

                return hasPermission;
            }
            catch (Exception ex)
            {
                string message = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                throw new Exception(message);
            }

        }

        /// <summary>
        /// This function wrapps the logic the retrive masterAccountNum, profileNum, engineer claims and check the perimssion.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="claimsPrincipal"></param>
        /// <param name="context"></param>
        /// <param name="permssionName"></param>
        /// <param name="permissionLevel"></param>
        /// <param name="outMessage"></param>
        /// <returns></returns>
        public static bool VerifyProfileLevelPermission(HttpRequest req, ClaimsPrincipal claimsPrincipal, ExecutionContext context,
            string permssionName, string permissionLevel, out string outMessage)
        {
            outMessage = "";
            try
            {
                bool hasPermission = true;

                return hasPermission;
            }
            catch (Exception ex)
            {
                string message = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                throw new Exception(message);
            }
        }

        /// <summary>
        /// If header doesn't have the value, get it from the query. If none of them have the value, return empty.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetHeaderQueryValue(HttpRequest req, string fieldName)
        {
            try
            {
                if (!req.Headers.TryGetValue(fieldName, out var fieldvalue))
                {
                    fieldvalue = req.Query[fieldName];
                }

                return fieldvalue.ForceToTrimString();
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static (bool, string) ValidateRequestFromQuery(HttpRequest req, List<RequestParameterType> reqParames)
        {
            bool noError = true;
            string errMsg = "";

            try
            {
                if (reqParames != null)
                {
                    foreach (RequestParameterType reqPara in reqParames)
                    {
                        Dictionary<string, string> paraValues = new Dictionary<string, string>();
                        if (reqPara.Sources == null || reqPara.Sources.Count == 0)
                        {
                            reqPara.Sources = new List<string>(new string[] { "Query" });
                        }
                        foreach (string source in reqPara.Sources)
                        {
                            string value = "";
                            switch (source.ToLower())
                            {
                                case "header":
                                    value = req.Headers[reqPara.Name].ForceToTrimString();
                                    break;
                                case "query":
                                    value = req.Query[reqPara.Name].ForceToTrimString();
                                    break;
                                case "path":
                                    value = Path.GetFileName(req.Path);
                                    break;
                                default:
                                    noError = false;
                                    errMsg += $"Undefined Request Parameter Source: {source}. ";
                                    //throw new Exception("Undefined Request Parameter Source:" + source);
                                    break;
                            }
                            if (!string.IsNullOrEmpty(value))
                            {
                                paraValues.Add(source.ToLower(), value);
                            }
                        }
                        if (paraValues.Count == 1)
                        {
                            reqPara.Value = paraValues.First().Value;
                        }
                        else if (paraValues.Count > 0)
                        {
                            reqPara.Value = paraValues["query"];
                        }

                        if (reqPara.IsRequired && string.IsNullOrEmpty(reqPara.Value))
                        {
                            noError = false;
                            errMsg += $"Undefined Request Parameter Source: {reqPara.Name}. ";
                            //throw new Exception("Required Paramer missing: " + reqPara.Name);
                        }
                    }
                }

                return (noError, errMsg);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
