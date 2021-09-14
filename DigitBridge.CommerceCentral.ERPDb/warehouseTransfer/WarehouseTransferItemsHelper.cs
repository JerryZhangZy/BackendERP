              
    

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
    /// Represents a WarehouseTransferItems SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class WarehouseTransferItemsHelper
    {
        public static readonly string TableName = "WarehouseTransferItems";
        public static readonly string TableAllies = "wthi";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferItemsUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseTransferItemsUuid) AS {name ?? "WarehouseTransferItemsUuid".ToCamelCase(camelCase)} ";
        public static string ReferWarehouseTransferItemsUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ReferWarehouseTransferItemsUuid) AS {name ?? "ReferWarehouseTransferItemsUuid".ToCamelCase(camelCase)} ";
        public static string WarehouseTransferUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseTransferUuid) AS {name ?? "WarehouseTransferUuid".ToCamelCase(camelCase)} ";
        public static string Seq(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Seq AS {name ?? "Seq".ToCamelCase(camelCase)} ";
        public static string ItemDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemDate AS {name ?? "ItemDate".ToCamelCase(camelCase)} ";
        public static string ItemTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemTime AS {name ?? "ItemTime".ToCamelCase(camelCase)} ";
        public static string SKU(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SKU) AS {name ?? "SKU".ToCamelCase(camelCase)} ";
        public static string ProductUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ProductUuid) AS {name ?? "ProductUuid".ToCamelCase(camelCase)} ";
        public static string FromInventoryUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FromInventoryUuid) AS {name ?? "FromInventoryUuid".ToCamelCase(camelCase)} ";
        public static string FromWarehouseUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FromWarehouseUuid) AS {name ?? "FromWarehouseUuid".ToCamelCase(camelCase)} ";
        public static string FromWarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FromWarehouseCode) AS {name ?? "FromWarehouseCode".ToCamelCase(camelCase)} ";
        public static string LotNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LotNum) AS {name ?? "LotNum".ToCamelCase(camelCase)} ";
        public static string ToInventoryUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToInventoryUuid) AS {name ?? "ToInventoryUuid".ToCamelCase(camelCase)} ";
        public static string ToWarehouseUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToWarehouseUuid) AS {name ?? "ToWarehouseUuid".ToCamelCase(camelCase)} ";
        public static string ToWarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToWarehouseCode) AS {name ?? "ToWarehouseCode".ToCamelCase(camelCase)} ";
        public static string ToLotNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ToLotNum) AS {name ?? "ToLotNum".ToCamelCase(camelCase)} ";
        public static string Description(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description".ToCamelCase(camelCase)} ";
        public static string Notes(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Notes) AS {name ?? "Notes".ToCamelCase(camelCase)} ";
        public static string UOM(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UOM) AS {name ?? "UOM".ToCamelCase(camelCase)} ";
        public static string PackType(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PackType) AS {name ?? "PackType".ToCamelCase(camelCase)} ";
        public static string PackQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PackQty AS {name ?? "PackQty".ToCamelCase(camelCase)} ";
        public static string TransferPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransferPack AS {name ?? "TransferPack".ToCamelCase(camelCase)} ";
        public static string TransferQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TransferQty AS {name ?? "TransferQty".ToCamelCase(camelCase)} ";
        public static string FromBeforeInstockPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.FromBeforeInstockPack AS {name ?? "FromBeforeInstockPack".ToCamelCase(camelCase)} ";
        public static string FromBeforeInstockQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.FromBeforeInstockQty AS {name ?? "FromBeforeInstockQty".ToCamelCase(camelCase)} ";
        public static string ToBeforeInstockPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ToBeforeInstockPack AS {name ?? "ToBeforeInstockPack".ToCamelCase(camelCase)} ";
        public static string ToBeforeInstockQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ToBeforeInstockQty AS {name ?? "ToBeforeInstockQty".ToCamelCase(camelCase)} ";
        public static string UnitCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UnitCost AS {name ?? "UnitCost".ToCamelCase(camelCase)} ";
        public static string AvgCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.AvgCost AS {name ?? "AvgCost".ToCamelCase(camelCase)} ";
        public static string LotCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LotCost AS {name ?? "LotCost".ToCamelCase(camelCase)} ";
        public static string LotInDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LotInDate AS {name ?? "LotInDate".ToCamelCase(camelCase)} ";
        public static string LotExpDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LotExpDate AS {name ?? "LotExpDate".ToCamelCase(camelCase)} ";
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
RTRIM({allies}WarehouseTransferItemsUuid) AS WarehouseTransferItemsUuid,
RTRIM({allies}ReferWarehouseTransferItemsUuid) AS ReferWarehouseTransferItemsUuid,
RTRIM({allies}WarehouseTransferUuid) AS WarehouseTransferUuid,
{allies}Seq AS Seq,
{allies}ItemDate AS ItemDate,
{allies}ItemTime AS ItemTime,
RTRIM({allies}SKU) AS SKU,
RTRIM({allies}ProductUuid) AS ProductUuid,
RTRIM({allies}FromInventoryUuid) AS FromInventoryUuid,
RTRIM({allies}FromWarehouseUuid) AS FromWarehouseUuid,
RTRIM({allies}FromWarehouseCode) AS FromWarehouseCode,
RTRIM({allies}LotNum) AS LotNum,
RTRIM({allies}ToInventoryUuid) AS ToInventoryUuid,
RTRIM({allies}ToWarehouseUuid) AS ToWarehouseUuid,
RTRIM({allies}ToWarehouseCode) AS ToWarehouseCode,
RTRIM({allies}ToLotNum) AS ToLotNum,
RTRIM({allies}Description) AS Description,
RTRIM({allies}Notes) AS Notes,
RTRIM({allies}UOM) AS UOM,
RTRIM({allies}PackType) AS PackType,
{allies}PackQty AS PackQty,
{allies}TransferPack AS TransferPack,
{allies}TransferQty AS TransferQty,
{allies}FromBeforeInstockPack AS FromBeforeInstockPack,
{allies}FromBeforeInstockQty AS FromBeforeInstockQty,
{allies}ToBeforeInstockPack AS ToBeforeInstockPack,
{allies}ToBeforeInstockQty AS ToBeforeInstockQty,
{allies}UnitCost AS UnitCost,
{allies}AvgCost AS AvgCost,
{allies}LotCost AS LotCost,
{allies}LotInDate AS LotInDate,
{allies}LotExpDate AS LotExpDate,
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
