              
    

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
    /// Represents a ApInvoiceHeader SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ApInvoiceHeaderHelper
    {
        public static readonly string TableName = "ApInvoiceHeader";
        public static readonly string TableAllies = "apih";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string ApInvoiceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceUuid) AS {name ?? "ApInvoiceUuid".ToCamelCase(camelCase)} ";
        public static string ApInvoiceNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceNum) AS {name ?? "ApInvoiceNum".ToCamelCase(camelCase)} ";
        public static string ApInvoiceType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceType AS {name ?? "ApInvoiceType".ToCamelCase(camelCase)} ";
        public static string ApInvoiceStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceStatus AS {name ?? "ApInvoiceStatus".ToCamelCase(camelCase)} ";
        public static string ApInvoiceDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceDate AS {name ?? "ApInvoiceDate".ToCamelCase(camelCase)} ";
        public static string ApInvoiceTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceTime AS {name ?? "ApInvoiceTime".ToCamelCase(camelCase)} ";
        public static string VendorUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorUuid) AS {name ?? "VendorUuid".ToCamelCase(camelCase)} ";
        public static string VendorNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorNum) AS {name ?? "VendorNum".ToCamelCase(camelCase)} ";
        public static string VendorName(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorName) AS {name ?? "VendorName".ToCamelCase(camelCase)} ";
        public static string VendorInvoiceNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorInvoiceNum) AS {name ?? "VendorInvoiceNum".ToCamelCase(camelCase)} ";
        public static string VendorInvoiceDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.VendorInvoiceDate AS {name ?? "VendorInvoiceDate".ToCamelCase(camelCase)} ";
        public static string DueDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DueDate AS {name ?? "DueDate".ToCamelCase(camelCase)} ";
        public static string BillDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BillDate AS {name ?? "BillDate".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string TotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalAmount AS {name ?? "TotalAmount".ToCamelCase(camelCase)} ";
        public static string PaidAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PaidAmount AS {name ?? "PaidAmount".ToCamelCase(camelCase)} ";
        public static string CreditAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CreditAmount AS {name ?? "CreditAmount".ToCamelCase(camelCase)} ";
        public static string Balance(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Balance AS {name ?? "Balance".ToCamelCase(camelCase)} ";
        public static string CreditAccount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CreditAccount AS {name ?? "CreditAccount".ToCamelCase(camelCase)} ";
        public static string DebitAccount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DebitAccount AS {name ?? "DebitAccount".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
        public static string UpdateDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UpdateDateUtc AS {name ?? "UpdateDateUtc".ToCamelCase(camelCase)} ";
        public static string EnterBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.EnterBy) AS {name ?? "EnterBy".ToCamelCase(camelCase)} ";
        public static string UpdateBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UpdateBy) AS {name ?? "UpdateBy".ToCamelCase(camelCase)} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid".ToCamelCase(camelCase)} ";
        public static string PoUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PoUuid) AS {name ?? "PoUuid".ToCamelCase(camelCase)} ";
        public static string PoNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PoNum) AS {name ?? "PoNum".ToCamelCase(camelCase)} ";
     

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}RowNum AS RowNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
RTRIM({allies}ApInvoiceUuid) AS ApInvoiceUuid,
RTRIM({allies}ApInvoiceNum) AS ApInvoiceNum,
{allies}ApInvoiceType AS ApInvoiceType,
{allies}ApInvoiceStatus AS ApInvoiceStatus,
{allies}ApInvoiceDate AS ApInvoiceDate,
{allies}ApInvoiceTime AS ApInvoiceTime,
RTRIM({allies}VendorUuid) AS VendorUuid,
RTRIM({allies}VendorNum) AS VendorNum,
RTRIM({allies}VendorName) AS VendorName,
RTRIM({allies}VendorInvoiceNum) AS VendorInvoiceNum,
{allies}VendorInvoiceDate AS VendorInvoiceDate,
{allies}DueDate AS DueDate,
{allies}BillDate AS BillDate,
RTRIM({allies}Currency) AS Currency,
{allies}TotalAmount AS TotalAmount,
{allies}PaidAmount AS PaidAmount,
{allies}CreditAmount AS CreditAmount,
{allies}Balance AS Balance,
{allies}CreditAccount AS CreditAccount,
{allies}DebitAccount AS DebitAccount,
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

