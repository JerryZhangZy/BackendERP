              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text;
using Newtonsoft.Json;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a Setting_Channel SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class Setting_ChannelHelper
    {
        public static readonly string TableName = "Setting_Channel";
        public static readonly string TableAllies = "chn";
        public static readonly string ChannelAccountAlias = "chna";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string ChannelNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelNum AS {name ?? "ChannelNum".ToCamelCase(camelCase)} ";
        public static string ChannelName(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ChannelName) AS {name ?? "ChannelName".ToCamelCase(camelCase)} ";
        public static string OrderSplitFlag(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderSplitFlag AS {name ?? "OrderSplitFlag".ToCamelCase(camelCase)} ";
        public static string OrderSplitPriorityLevel(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderSplitPriorityLevel AS {name ?? "OrderSplitPriorityLevel".ToCamelCase(camelCase)} ";
        public static string Category(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Category) AS {name ?? "Category".ToCamelCase(camelCase)} ";
        public static string PlatformNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PlatformNum AS {name ?? "PlatformNum".ToCamelCase(camelCase)} ";
        public static string PlatformName(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PlatformName) AS {name ?? "PlatformName".ToCamelCase(camelCase)} ";
        public static string ChannelCurrency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ChannelCurrency) AS {name ?? "ChannelCurrency".ToCamelCase(camelCase)} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
{allies}ChannelNum AS ChannelNum,
RTRIM({allies}ChannelName) AS ChannelName,
{allies}OrderSplitFlag AS OrderSplitFlag,
{allies}OrderSplitPriorityLevel AS OrderSplitPriorityLevel,
RTRIM({allies}Category) AS Category,
{allies}PlatformNum AS PlatformNum,
RTRIM({allies}PlatformName) AS PlatformName,
RTRIM({allies}ChannelCurrency) AS ChannelCurrency
";
        }

        public static string SelectAllWhere(string sqlWhere, string tableAllies = null, bool forJson = false) 
        {
            if (!sqlWhere.StartsWith("WHERE", StringComparison.CurrentCultureIgnoreCase))
                sqlWhere = $"WHERE {sqlWhere}";
            var forJsonString = forJson ? "FOR JSON PATH" : string.Empty;
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : tableAllies.TrimEnd();

            return $"SELECT {SelectAll(tableAllies)} FROM {TableName} {allies} {sqlWhere} {forJsonString}";
        }

    }
}
