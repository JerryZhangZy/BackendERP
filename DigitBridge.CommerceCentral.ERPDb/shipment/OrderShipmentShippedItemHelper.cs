              
    

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
    /// Represents a OrderShipmentShippedItem SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class OrderShipmentShippedItemHelper
    {
        public static readonly string TableName = "OrderShipmentShippedItem";
        public static readonly string TableAllies = "shpl";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string OrderShipmentShippedItemNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderShipmentShippedItemNum AS {name ?? "OrderShipmentShippedItemNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string ChannelNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelNum AS {name ?? "ChannelNum".ToCamelCase(camelCase)} ";
        public static string ChannelAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelAccountNum AS {name ?? "ChannelAccountNum".ToCamelCase(camelCase)} ";
        public static string OrderShipmentNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderShipmentNum AS {name ?? "OrderShipmentNum".ToCamelCase(camelCase)} ";
        public static string OrderShipmentPackageNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderShipmentPackageNum AS {name ?? "OrderShipmentPackageNum".ToCamelCase(camelCase)} ";
        public static string ChannelOrderID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ChannelOrderID) AS {name ?? "ChannelOrderID".ToCamelCase(camelCase)} ";
        public static string OrderDCAssignmentLineNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderDCAssignmentLineNum AS {name ?? "OrderDCAssignmentLineNum".ToCamelCase(camelCase)} ";
        public static string SKU(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SKU) AS {name ?? "SKU".ToCamelCase(camelCase)} ";
        public static string ShippedQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippedQty AS {name ?? "ShippedQty".ToCamelCase(camelCase)} ";
        public static string DBChannelOrderLineRowID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.DBChannelOrderLineRowID) AS {name ?? "DBChannelOrderLineRowID".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
        public static string OrderShipmentUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderShipmentUuid) AS {name ?? "OrderShipmentUuid".ToCamelCase(camelCase)} ";
        public static string OrderShipmentPackageUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderShipmentPackageUuid) AS {name ?? "OrderShipmentPackageUuid".ToCamelCase(camelCase)} ";
        public static string OrderShipmentShippedItemUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderShipmentShippedItemUuid) AS {name ?? "OrderShipmentShippedItemUuid".ToCamelCase(camelCase)} ";
        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid".ToCamelCase(camelCase)} ";
        public static string SalesOrderItemsUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SalesOrderItemsUuid) AS {name ?? "SalesOrderItemsUuid".ToCamelCase(camelCase)} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}OrderShipmentShippedItemNum AS OrderShipmentShippedItemNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
{allies}ChannelNum AS ChannelNum,
{allies}ChannelAccountNum AS ChannelAccountNum,
{allies}OrderShipmentNum AS OrderShipmentNum,
{allies}OrderShipmentPackageNum AS OrderShipmentPackageNum,
RTRIM({allies}ChannelOrderID) AS ChannelOrderID,
{allies}OrderDCAssignmentLineNum AS OrderDCAssignmentLineNum,
RTRIM({allies}SKU) AS SKU,
{allies}ShippedQty AS ShippedQty,
RTRIM({allies}DBChannelOrderLineRowID) AS DBChannelOrderLineRowID,
{allies}EnterDateUtc AS EnterDateUtc,
RTRIM({allies}OrderShipmentUuid) AS OrderShipmentUuid,
RTRIM({allies}OrderShipmentPackageUuid) AS OrderShipmentPackageUuid,
RTRIM({allies}OrderShipmentShippedItemUuid) AS OrderShipmentShippedItemUuid,
{allies}RowNum AS RowNum,
{allies}DigitBridgeGuid AS DigitBridgeGuid,
RTRIM({allies}SalesOrderItemsUuid) AS SalesOrderItemsUuid
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

