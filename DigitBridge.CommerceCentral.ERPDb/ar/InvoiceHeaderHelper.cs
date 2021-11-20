              
    

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
    /// Represents a InvoiceHeader SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InvoiceHeaderHelper
    {
        public static readonly string TableName = "InvoiceHeader";
        public static readonly string TableAllies = "ins";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string InvoiceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InvoiceUuid) AS {name ?? "InvoiceUuid".ToCamelCase(camelCase)} ";
        public static string InvoiceNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InvoiceNumber) AS {name ?? "InvoiceNumber".ToCamelCase(camelCase)} ";
        public static string QboDocNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.QboDocNumber) AS {name ?? "QboDocNumber".ToCamelCase(camelCase)} ";
        public static string SalesOrderUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SalesOrderUuid) AS {name ?? "SalesOrderUuid".ToCamelCase(camelCase)} ";
        public static string OrderNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderNumber) AS {name ?? "OrderNumber".ToCamelCase(camelCase)} ";
        public static string InvoiceType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.InvoiceType AS {name ?? "InvoiceType".ToCamelCase(camelCase)} ";
        public static string InvoiceStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.InvoiceStatus AS {name ?? "InvoiceStatus".ToCamelCase(camelCase)} ";
        public static string InvoiceDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.InvoiceDate AS {name ?? "InvoiceDate".ToCamelCase(camelCase)} ";
        public static string InvoiceTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.InvoiceTime AS {name ?? "InvoiceTime".ToCamelCase(camelCase)} ";
        public static string DueDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DueDate AS {name ?? "DueDate".ToCamelCase(camelCase)} ";
        public static string BillDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BillDate AS {name ?? "BillDate".ToCamelCase(camelCase)} ";
        public static string ShipDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipDate AS {name ?? "ShipDate".ToCamelCase(camelCase)} ";
        public static string CustomerUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.CustomerUuid) AS {name ?? "CustomerUuid".ToCamelCase(camelCase)} ";
        public static string CustomerCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.CustomerCode) AS {name ?? "CustomerCode".ToCamelCase(camelCase)} ";
        public static string CustomerName(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.CustomerName) AS {name ?? "CustomerName".ToCamelCase(camelCase)} ";
        public static string Terms(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Terms) AS {name ?? "Terms".ToCamelCase(camelCase)} ";
        public static string TermsDays(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TermsDays AS {name ?? "TermsDays".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string SubTotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.SubTotalAmount AS {name ?? "SubTotalAmount".ToCamelCase(camelCase)} ";
        public static string SalesAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.SalesAmount AS {name ?? "SalesAmount".ToCamelCase(camelCase)} ";
        public static string TotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalAmount AS {name ?? "TotalAmount".ToCamelCase(camelCase)} ";
        public static string TaxableAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxableAmount AS {name ?? "TaxableAmount".ToCamelCase(camelCase)} ";
        public static string NonTaxableAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.NonTaxableAmount AS {name ?? "NonTaxableAmount".ToCamelCase(camelCase)} ";
        public static string TaxRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxRate AS {name ?? "TaxRate".ToCamelCase(camelCase)} ";
        public static string TaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxAmount AS {name ?? "TaxAmount".ToCamelCase(camelCase)} ";
        public static string DiscountRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountRate AS {name ?? "DiscountRate".ToCamelCase(camelCase)} ";
        public static string DiscountAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountAmount AS {name ?? "DiscountAmount".ToCamelCase(camelCase)} ";
        public static string ShippingAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingAmount AS {name ?? "ShippingAmount".ToCamelCase(camelCase)} ";
        public static string ShippingTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingTaxAmount AS {name ?? "ShippingTaxAmount".ToCamelCase(camelCase)} ";
        public static string MiscAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscAmount AS {name ?? "MiscAmount".ToCamelCase(camelCase)} ";
        public static string MiscTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscTaxAmount AS {name ?? "MiscTaxAmount".ToCamelCase(camelCase)} ";
        public static string ChargeAndAllowanceAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChargeAndAllowanceAmount AS {name ?? "ChargeAndAllowanceAmount".ToCamelCase(camelCase)} ";
        public static string ChannelAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelAmount AS {name ?? "ChannelAmount".ToCamelCase(camelCase)} ";
        public static string PaidAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PaidAmount AS {name ?? "PaidAmount".ToCamelCase(camelCase)} ";
        public static string CreditAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CreditAmount AS {name ?? "CreditAmount".ToCamelCase(camelCase)} ";
        public static string Balance(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Balance AS {name ?? "Balance".ToCamelCase(camelCase)} ";
        public static string UnitCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UnitCost AS {name ?? "UnitCost".ToCamelCase(camelCase)} ";
        public static string AvgCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.AvgCost AS {name ?? "AvgCost".ToCamelCase(camelCase)} ";
        public static string LotCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LotCost AS {name ?? "LotCost".ToCamelCase(camelCase)} ";
        public static string InvoiceSourceCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InvoiceSourceCode) AS {name ?? "InvoiceSourceCode".ToCamelCase(camelCase)} ";
        public static string UpdateDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UpdateDateUtc AS {name ?? "UpdateDateUtc".ToCamelCase(camelCase)} ";
        public static string EnterBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.EnterBy) AS {name ?? "EnterBy".ToCamelCase(camelCase)} ";
        public static string UpdateBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UpdateBy) AS {name ?? "UpdateBy".ToCamelCase(camelCase)} ";
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
RTRIM({allies}InvoiceUuid) AS InvoiceUuid,
RTRIM({allies}InvoiceNumber) AS InvoiceNumber,
RTRIM({allies}QboDocNumber) AS QboDocNumber,
RTRIM({allies}SalesOrderUuid) AS SalesOrderUuid,
RTRIM({allies}OrderNumber) AS OrderNumber,
{allies}InvoiceType AS InvoiceType,
{allies}InvoiceStatus AS InvoiceStatus,
{allies}InvoiceDate AS InvoiceDate,
{allies}InvoiceTime AS InvoiceTime,
{allies}DueDate AS DueDate,
{allies}BillDate AS BillDate,
{allies}ShipDate AS ShipDate,
RTRIM({allies}CustomerUuid) AS CustomerUuid,
RTRIM({allies}CustomerCode) AS CustomerCode,
RTRIM({allies}CustomerName) AS CustomerName,
RTRIM({allies}Terms) AS Terms,
{allies}TermsDays AS TermsDays,
RTRIM({allies}Currency) AS Currency,
{allies}SubTotalAmount AS SubTotalAmount,
{allies}SalesAmount AS SalesAmount,
{allies}TotalAmount AS TotalAmount,
{allies}TaxableAmount AS TaxableAmount,
{allies}NonTaxableAmount AS NonTaxableAmount,
{allies}TaxRate AS TaxRate,
{allies}TaxAmount AS TaxAmount,
{allies}DiscountRate AS DiscountRate,
{allies}DiscountAmount AS DiscountAmount,
{allies}ShippingAmount AS ShippingAmount,
{allies}ShippingTaxAmount AS ShippingTaxAmount,
{allies}MiscAmount AS MiscAmount,
{allies}MiscTaxAmount AS MiscTaxAmount,
{allies}ChargeAndAllowanceAmount AS ChargeAndAllowanceAmount,
{allies}ChannelAmount AS ChannelAmount,
{allies}PaidAmount AS PaidAmount,
{allies}CreditAmount AS CreditAmount,
{allies}Balance AS Balance,
{allies}UnitCost AS UnitCost,
{allies}AvgCost AS AvgCost,
{allies}LotCost AS LotCost,
RTRIM({allies}InvoiceSourceCode) AS InvoiceSourceCode,
{allies}UpdateDateUtc AS UpdateDateUtc,
RTRIM({allies}EnterBy) AS EnterBy,
RTRIM({allies}UpdateBy) AS UpdateBy,
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

