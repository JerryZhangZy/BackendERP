              
    

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
    /// Represents a ApInvoiceItems SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ApInvoiceItemsHelper
    {
        public static readonly string TableName = "ApInvoiceItems";
        public static readonly string TableAllies = "apii";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string ApInvoiceItemsUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceItemsUuid) AS {name ?? "ApInvoiceItemsUuid".ToCamelCase(camelCase)} ";
        public static string ApInvoiceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceUuid) AS {name ?? "ApInvoiceUuid".ToCamelCase(camelCase)} ";
        public static string Seq(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Seq AS {name ?? "Seq".ToCamelCase(camelCase)} ";
        public static string ApInvoiceItemType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceItemType AS {name ?? "ApInvoiceItemType".ToCamelCase(camelCase)} ";
        public static string ApInvoiceItemStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ApInvoiceItemStatus AS {name ?? "ApInvoiceItemStatus".ToCamelCase(camelCase)} ";
        public static string ItemDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemDate AS {name ?? "ItemDate".ToCamelCase(camelCase)} ";
        public static string ItemTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemTime AS {name ?? "ItemTime".ToCamelCase(camelCase)} ";
        public static string ApDistributionNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApDistributionNum) AS {name ?? "ApDistributionNum".ToCamelCase(camelCase)} ";
        public static string Description(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description".ToCamelCase(camelCase)} ";
        public static string Notes(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Notes) AS {name ?? "Notes".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string Amount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Amount AS {name ?? "Amount".ToCamelCase(camelCase)} ";
        public static string IsAp(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.IsAp AS {name ?? "IsAp".ToCamelCase(camelCase)} ";
        public static string CreditAccount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CreditAccount AS {name ?? "CreditAccount".ToCamelCase(camelCase)} ";
        public static string DebitAccount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DebitAccount AS {name ?? "DebitAccount".ToCamelCase(camelCase)} ";
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
RTRIM({allies}ApInvoiceItemsUuid) AS ApInvoiceItemsUuid,
RTRIM({allies}ApInvoiceUuid) AS ApInvoiceUuid,
{allies}Seq AS Seq,
{allies}ApInvoiceItemType AS ApInvoiceItemType,
{allies}ApInvoiceItemStatus AS ApInvoiceItemStatus,
{allies}ItemDate AS ItemDate,
{allies}ItemTime AS ItemTime,
RTRIM({allies}ApDistributionNum) AS ApDistributionNum,
RTRIM({allies}Description) AS Description,
RTRIM({allies}Notes) AS Notes,
RTRIM({allies}Currency) AS Currency,
{allies}Amount AS Amount,
{allies}IsAp AS IsAp,
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
