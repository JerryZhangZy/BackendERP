              
    

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
    /// Represents a ShippingCodes SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ShippingCodesHelper
    {
        public static readonly string TableName = "ShippingCodes";
        public static readonly string TableAllies = "spc";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string ShippingCodesUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingCodesUuid) AS {name ?? "ShippingCodesUuid".ToCamelCase(camelCase)} ";
        public static string ShippingCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingCode) AS {name ?? "ShippingCode".ToCamelCase(camelCase)} ";
        public static string Description(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description".ToCamelCase(camelCase)} ";
        public static string ShippingCarrier(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingCarrier) AS {name ?? "ShippingCarrier".ToCamelCase(camelCase)} ";
        public static string ShippingClass(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingClass) AS {name ?? "ShippingClass".ToCamelCase(camelCase)} ";
        public static string Scac(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Scac) AS {name ?? "Scac".ToCamelCase(camelCase)} ";
        public static string JsonFields(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.JsonFields) AS {name ?? "JsonFields".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
        public static string UpdateDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UpdateDateUtc AS {name ?? "UpdateDateUtc".ToCamelCase(camelCase)} ";
        public static string EnterBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.EnterBy) AS {name ?? "EnterBy".ToCamelCase(camelCase)} ";
        public static string UpdateBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UpdateBy) AS {name ?? "UpdateBy".ToCamelCase(camelCase)} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid".ToCamelCase(camelCase)} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}RowNum AS RowNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
RTRIM({allies}ShippingCodesUuid) AS ShippingCodesUuid,
RTRIM({allies}ShippingCode) AS ShippingCode,
RTRIM({allies}Description) AS Description,
RTRIM({allies}ShippingCarrier) AS ShippingCarrier,
RTRIM({allies}ShippingClass) AS ShippingClass,
RTRIM({allies}Scac) AS Scac,
RTRIM({allies}JsonFields) AS JsonFields,
{allies}EnterDateUtc AS EnterDateUtc,
{allies}UpdateDateUtc AS UpdateDateUtc,
RTRIM({allies}EnterBy) AS EnterBy,
RTRIM({allies}UpdateBy) AS UpdateBy,
{allies}DigitBridgeGuid AS DigitBridgeGuid
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

