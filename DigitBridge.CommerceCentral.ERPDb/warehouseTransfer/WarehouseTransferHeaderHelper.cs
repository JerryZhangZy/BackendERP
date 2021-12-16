              
    

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
    /// Represents a WarehouseTransferHeader SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class WarehouseTransferHeaderHelper
    {
        public static readonly string TableName = "WarehouseTransferHeader";
        public static readonly string TableAllies = "wth";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseTransferUuid) AS {name ?? "WarehouseTransferUuid".ToCamelCase(camelCase)} ";
        public static string BatchNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.BatchNumber) AS {name ?? "BatchNumber".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.WarehouseTransferType AS {name ?? "WarehouseTransferType".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.WarehouseTransferStatus AS {name ?? "WarehouseTransferStatus".ToCamelCase(camelCase)} ";
        public static string TransferDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransferDate AS {name ?? "TransferDate".ToCamelCase(camelCase)} ";
        public static string TransferTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransferTime AS {name ?? "TransferTime".ToCamelCase(camelCase)} ";
        public static string Processor(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Processor) AS {name ?? "Processor".ToCamelCase(camelCase)} ";
        public static string ReceiveDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ReceiveDate AS {name ?? "ReceiveDate".ToCamelCase(camelCase)} ";
        public static string ReceiveTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ReceiveTime AS {name ?? "ReceiveTime".ToCamelCase(camelCase)} ";
        public static string ReceiveProcessor(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ReceiveProcessor) AS {name ?? "ReceiveProcessor".ToCamelCase(camelCase)} ";
        public static string FromWarehouseUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FromWarehouseUuid) AS {name ?? "FromWarehouseUuid".ToCamelCase(camelCase)} ";
        public static string FromWarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FromWarehouseCode) AS {name ?? "FromWarehouseCode".ToCamelCase(camelCase)} ";
        public static string ToWarehouseUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToWarehouseUuid) AS {name ?? "ToWarehouseUuid".ToCamelCase(camelCase)} ";
        public static string ToWarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToWarehouseCode) AS {name ?? "ToWarehouseCode".ToCamelCase(camelCase)} ";
        public static string ReferenceType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ReferenceType AS {name ?? "ReferenceType".ToCamelCase(camelCase)} ";
        public static string ReferenceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ReferenceUuid) AS {name ?? "ReferenceUuid".ToCamelCase(camelCase)} ";
        public static string ReferenceNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ReferenceNum) AS {name ?? "ReferenceNum".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferSourceCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseTransferSourceCode) AS {name ?? "WarehouseTransferSourceCode".ToCamelCase(camelCase)} ";

        public static string InTransitToWarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InTransitToWarehouseCode) AS {name ?? "InTransitToWarehouseCode".ToCamelCase(camelCase)} ";
        


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
RTRIM({allies}WarehouseTransferUuid) AS WarehouseTransferUuid,
RTRIM({allies}BatchNumber) AS BatchNumber,
{allies}WarehouseTransferType AS WarehouseTransferType,
{allies}WarehouseTransferStatus AS WarehouseTransferStatus,
{allies}TransferDate AS TransferDate,
{allies}TransferTime AS TransferTime,
RTRIM({allies}Processor) AS Processor,
{allies}ReceiveDate AS ReceiveDate,
{allies}ReceiveTime AS ReceiveTime,
RTRIM({allies}ReceiveProcessor) AS ReceiveProcessor,
RTRIM({allies}FromWarehouseUuid) AS FromWarehouseUuid,
RTRIM({allies}FromWarehouseCode) AS FromWarehouseCode,
RTRIM({allies}ToWarehouseUuid) AS ToWarehouseUuid,
RTRIM({allies}ToWarehouseCode) AS ToWarehouseCode,
{allies}ReferenceType AS ReferenceType,
RTRIM({allies}ReferenceUuid) AS ReferenceUuid,
RTRIM({allies}ReferenceNum) AS ReferenceNum,
RTRIM({allies}WarehouseTransferSourceCode) AS WarehouseTransferSourceCode,
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

