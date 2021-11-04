              
    

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
    /// Represents a ApInvoiceTransaction SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ApInvoiceTransactionHelper
    {
        public static readonly string TableName = "ApInvoiceTransaction";
        public static readonly string TableAllies = "aptrans";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string TransUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.TransUuid) AS {name ?? "TransUuid".ToCamelCase(camelCase)} ";
        public static string TransNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransNum AS {name ?? "TransNum".ToCamelCase(camelCase)} ";
        public static string ApInvoiceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceUuid) AS {name ?? "ApInvoiceUuid".ToCamelCase(camelCase)} ";
        public static string ApInvoiceNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ApInvoiceNum) AS {name ?? "ApInvoiceNum".ToCamelCase(camelCase)} ";
        public static string TransType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransType AS {name ?? "TransType".ToCamelCase(camelCase)} ";
        public static string TransStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransStatus AS {name ?? "TransStatus".ToCamelCase(camelCase)} ";
        public static string TransDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransDate AS {name ?? "TransDate".ToCamelCase(camelCase)} ";
        public static string TransTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransTime AS {name ?? "TransTime".ToCamelCase(camelCase)} ";
        public static string Description(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description".ToCamelCase(camelCase)} ";
        public static string Notes(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Notes) AS {name ?? "Notes".ToCamelCase(camelCase)} ";
        public static string PaidBy(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PaidBy AS {name ?? "PaidBy".ToCamelCase(camelCase)} ";
        public static string BankAccountUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.BankAccountUuid) AS {name ?? "BankAccountUuid".ToCamelCase(camelCase)} ";
        public static string BankAccountCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.BankAccountCode) AS {name ?? "BankAccountCode".ToCamelCase(camelCase)} ";
        public static string CheckNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.CheckNum) AS {name ?? "CheckNum".ToCamelCase(camelCase)} ";
        public static string AuthCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.AuthCode) AS {name ?? "AuthCode".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string ExchangeRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ExchangeRate AS {name ?? "ExchangeRate".ToCamelCase(camelCase)} ";
        public static string Amount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Amount AS {name ?? "Amount".ToCamelCase(camelCase)} ";
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
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
RTRIM({allies}TransUuid) AS TransUuid,
{allies}TransNum AS TransNum,
RTRIM({allies}ApInvoiceUuid) AS ApInvoiceUuid,
RTRIM({allies}ApInvoiceNum) AS ApInvoiceNum,
{allies}TransType AS TransType,
{allies}TransStatus AS TransStatus,
{allies}TransDate AS TransDate,
{allies}TransTime AS TransTime,
RTRIM({allies}Description) AS Description,
RTRIM({allies}Notes) AS Notes,
{allies}PaidBy AS PaidBy,
RTRIM({allies}BankAccountUuid) AS BankAccountUuid,
RTRIM({allies}BankAccountCode) AS BankAccountCode,
RTRIM({allies}CheckNum) AS CheckNum,
RTRIM({allies}AuthCode) AS AuthCode,
RTRIM({allies}Currency) AS Currency,
{allies}ExchangeRate AS ExchangeRate,
{allies}Amount AS Amount,
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
