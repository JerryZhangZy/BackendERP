
              
    

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
    /// Represents a InventoryAttributes SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InventoryAttributesHelper
    {
        public static readonly string TableName = "InventoryAttributes";
        public static readonly string TableAllies = "inva";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum"} ";
        public static string ProductUuid(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.ProductUuid) AS {name ?? "ProductUuid"} ";
        public static string InventoryUuid(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.InventoryUuid) AS {name ?? "InventoryUuid"} ";
        public static string JsonFields(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.JsonFields) AS {name ?? "JsonFields"} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc"} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid"} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}RowNum AS RowNum,
RTRIM({allies}ProductUuid) AS ProductUuid,
RTRIM({allies}InventoryUuid) AS InventoryUuid,
RTRIM({allies}JsonFields) AS JsonFields,
{allies}EnterDateUtc AS EnterDateUtc,
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

