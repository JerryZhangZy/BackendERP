              
    

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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksExportLog SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class QuickBooksExportLogHelper
    {
        public static readonly string TableName = "QuickBooksExportLog";
        public static readonly string TableAllies = "qbel";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string QuickBooksExportLogUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.QuickBooksExportLogUuid) AS {name ?? "QuickBooksExportLogUuid".ToCamelCase(camelCase)} ";
        public static string BatchNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BatchNum AS {name ?? "BatchNum".ToCamelCase(camelCase)} ";
        public static string LogType(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LogType) AS {name ?? "LogType".ToCamelCase(camelCase)} ";
        public static string LogUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LogUuid) AS {name ?? "LogUuid".ToCamelCase(camelCase)} ";
        public static string DocNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.DocNumber) AS {name ?? "DocNumber".ToCamelCase(camelCase)} ";
        public static string DocStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DocStatus AS {name ?? "DocStatus".ToCamelCase(camelCase)} ";
        public static string LogDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LogDate AS {name ?? "LogDate".ToCamelCase(camelCase)} ";
        public static string LogTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LogTime AS {name ?? "LogTime".ToCamelCase(camelCase)} ";
        public static string LogBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LogBy) AS {name ?? "LogBy".ToCamelCase(camelCase)} ";
        public static string TxnId(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.TxnId) AS {name ?? "TxnId".ToCamelCase(camelCase)} ";
        public static string ErrorMessage(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ErrorMessage AS {name ?? "ErrorMessage".ToCamelCase(camelCase)} ";
        public static string RequestInfo(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.RequestInfo) AS {name ?? "RequestInfo".ToCamelCase(camelCase)} ";
        public static string LogStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LogStatus AS {name ?? "LogStatus".ToCamelCase(camelCase)} ";
        public static string EnterBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.EnterBy) AS {name ?? "EnterBy".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
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
RTRIM({allies}QuickBooksExportLogUuid) AS QuickBooksExportLogUuid,
{allies}BatchNum AS BatchNum,
RTRIM({allies}LogType) AS LogType,
RTRIM({allies}LogUuid) AS LogUuid,
RTRIM({allies}DocNumber) AS DocNumber,
{allies}DocStatus AS DocStatus,
{allies}LogDate AS LogDate,
{allies}LogTime AS LogTime,
RTRIM({allies}LogBy) AS LogBy,
RTRIM({allies}EnterBy) AS EnterBy,
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

