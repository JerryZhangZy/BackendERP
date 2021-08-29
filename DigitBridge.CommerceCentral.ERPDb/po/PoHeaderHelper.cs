              
    

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
    /// Represents a PoHeader SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class PoHeaderHelper
    {
        public static readonly string TableName = "PoHeader";
        public static readonly string TableAllies = "poh";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string PoUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PoUuid) AS {name ?? "PoUuid".ToCamelCase(camelCase)} ";
        public static string PoNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PoNum) AS {name ?? "PoNum".ToCamelCase(camelCase)} ";
        public static string PoType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PoType AS {name ?? "PoType".ToCamelCase(camelCase)} ";
        public static string PoStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PoStatus AS {name ?? "PoStatus".ToCamelCase(camelCase)} ";
        public static string PoDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PoDate AS {name ?? "PoDate".ToCamelCase(camelCase)} ";
        public static string PoTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PoTime AS {name ?? "PoTime".ToCamelCase(camelCase)} ";
        public static string EtaShipDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EtaShipDate AS {name ?? "EtaShipDate".ToCamelCase(camelCase)} ";
        public static string EtaArrivalDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EtaArrivalDate AS {name ?? "EtaArrivalDate".ToCamelCase(camelCase)} ";
        public static string CancelDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CancelDate AS {name ?? "CancelDate".ToCamelCase(camelCase)} ";
        public static string VendorUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorUuid) AS {name ?? "VendorUuid".ToCamelCase(camelCase)} ";
        public static string VendorNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorNum) AS {name ?? "VendorNum".ToCamelCase(camelCase)} ";
        public static string VendorName(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VendorName) AS {name ?? "VendorName".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string SubTotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.SubTotalAmount AS {name ?? "SubTotalAmount".ToCamelCase(camelCase)} ";
        public static string TotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalAmount AS {name ?? "TotalAmount".ToCamelCase(camelCase)} ";
        public static string TaxRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxRate AS {name ?? "TaxRate".ToCamelCase(camelCase)} ";
        public static string TaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxAmount AS {name ?? "TaxAmount".ToCamelCase(camelCase)} ";
        public static string DiscountRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountRate AS {name ?? "DiscountRate".ToCamelCase(camelCase)} ";
        public static string DiscountAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountAmount AS {name ?? "DiscountAmount".ToCamelCase(camelCase)} ";
        public static string ShippingAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingAmount AS {name ?? "ShippingAmount".ToCamelCase(camelCase)} ";
        public static string ShippingTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingTaxAmount AS {name ?? "ShippingTaxAmount".ToCamelCase(camelCase)} ";
        public static string MiscAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscAmount AS {name ?? "MiscAmount".ToCamelCase(camelCase)} ";
        public static string MiscTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscTaxAmount AS {name ?? "MiscTaxAmount".ToCamelCase(camelCase)} ";
        public static string ChargeAndAllowanceAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChargeAndAllowanceAmount AS {name ?? "ChargeAndAllowanceAmount".ToCamelCase(camelCase)} ";
        public static string PoSourceCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PoSourceCode) AS {name ?? "PoSourceCode".ToCamelCase(camelCase)} ";
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
RTRIM({allies}PoUuid) AS PoUuid,
RTRIM({allies}PoNum) AS PoNum,
{allies}PoType AS PoType,
{allies}PoStatus AS PoStatus,
{allies}PoDate AS PoDate,
{allies}PoTime AS PoTime,
{allies}EtaShipDate AS EtaShipDate,
{allies}EtaArrivalDate AS EtaArrivalDate,
{allies}CancelDate AS CancelDate,
RTRIM({allies}VendorUuid) AS VendorUuid,
RTRIM({allies}VendorNum) AS VendorNum,
RTRIM({allies}VendorName) AS VendorName,
RTRIM({allies}Currency) AS Currency,
{allies}SubTotalAmount AS SubTotalAmount,
{allies}TotalAmount AS TotalAmount,
{allies}TaxRate AS TaxRate,
{allies}TaxAmount AS TaxAmount,
{allies}DiscountRate AS DiscountRate,
{allies}DiscountAmount AS DiscountAmount,
{allies}ShippingAmount AS ShippingAmount,
{allies}ShippingTaxAmount AS ShippingTaxAmount,
{allies}MiscAmount AS MiscAmount,
{allies}MiscTaxAmount AS MiscTaxAmount,
{allies}ChargeAndAllowanceAmount AS ChargeAndAllowanceAmount,
RTRIM({allies}PoSourceCode) AS PoSourceCode,
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

