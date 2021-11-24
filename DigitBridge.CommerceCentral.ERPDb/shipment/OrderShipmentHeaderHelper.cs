              
    

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
    /// Represents a OrderShipmentHeader SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class OrderShipmentHeaderHelper
    {
        public static readonly string TableName = "OrderShipmentHeader";
        public static readonly string TableAllies = "shp";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string OrderShipmentNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderShipmentNum AS {name ?? "OrderShipmentNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string ChannelNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelNum AS {name ?? "ChannelNum".ToCamelCase(camelCase)} ";
        public static string ChannelAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChannelAccountNum AS {name ?? "ChannelAccountNum".ToCamelCase(camelCase)} ";
        public static string OrderDCAssignmentNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderDCAssignmentNum AS {name ?? "OrderDCAssignmentNum".ToCamelCase(camelCase)} ";
        public static string DistributionCenterNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DistributionCenterNum AS {name ?? "DistributionCenterNum".ToCamelCase(camelCase)} ";
        public static string CentralOrderNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CentralOrderNum AS {name ?? "CentralOrderNum".ToCamelCase(camelCase)} ";
        public static string ChannelOrderID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ChannelOrderID) AS {name ?? "ChannelOrderID".ToCamelCase(camelCase)} ";
        public static string ShipmentID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShipmentID) AS {name ?? "ShipmentID".ToCamelCase(camelCase)} ";
        public static string WarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseCode) AS {name ?? "WarehouseCode".ToCamelCase(camelCase)} ";
        public static string ShipmentType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipmentType AS {name ?? "ShipmentType".ToCamelCase(camelCase)} ";
        public static string ShipmentReferenceID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShipmentReferenceID) AS {name ?? "ShipmentReferenceID".ToCamelCase(camelCase)} ";
        public static string ShipmentDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipmentDateUtc AS {name ?? "ShipmentDateUtc".ToCamelCase(camelCase)} ";
        public static string ShippingCarrier(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingCarrier) AS {name ?? "ShippingCarrier".ToCamelCase(camelCase)} ";
        public static string ShippingClass(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShippingClass) AS {name ?? "ShippingClass".ToCamelCase(camelCase)} ";
        public static string ShippingCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingCost AS {name ?? "ShippingCost".ToCamelCase(camelCase)} ";
        public static string MainTrackingNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.MainTrackingNumber) AS {name ?? "MainTrackingNumber".ToCamelCase(camelCase)} ";
        public static string MainReturnTrackingNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.MainReturnTrackingNumber) AS {name ?? "MainReturnTrackingNumber".ToCamelCase(camelCase)} ";
        public static string BillOfLadingID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.BillOfLadingID) AS {name ?? "BillOfLadingID".ToCamelCase(camelCase)} ";
        public static string TotalPackages(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalPackages AS {name ?? "TotalPackages".ToCamelCase(camelCase)} ";
        public static string TotalShippedQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalShippedQty AS {name ?? "TotalShippedQty".ToCamelCase(camelCase)} ";
        public static string TotalCanceledQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalCanceledQty AS {name ?? "TotalCanceledQty".ToCamelCase(camelCase)} ";
        public static string TotalWeight(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalWeight AS {name ?? "TotalWeight".ToCamelCase(camelCase)} ";
        public static string TotalVolume(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TotalVolume AS {name ?? "TotalVolume".ToCamelCase(camelCase)} ";
        public static string WeightUnit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.WeightUnit AS {name ?? "WeightUnit".ToCamelCase(camelCase)} ";
        public static string LengthUnit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.LengthUnit AS {name ?? "LengthUnit".ToCamelCase(camelCase)} ";
        public static string VolumeUnit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.VolumeUnit AS {name ?? "VolumeUnit".ToCamelCase(camelCase)} ";
        public static string ShipmentStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipmentStatus AS {name ?? "ShipmentStatus".ToCamelCase(camelCase)} ";
        public static string DBChannelOrderHeaderRowID(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.DBChannelOrderHeaderRowID) AS {name ?? "DBChannelOrderHeaderRowID".ToCamelCase(camelCase)} ";
        public static string ProcessStatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProcessStatus AS {name ?? "ProcessStatus".ToCamelCase(camelCase)} ";
        public static string ProcessDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProcessDateUtc AS {name ?? "ProcessDateUtc".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
        public static string OrderShipmentUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderShipmentUuid) AS {name ?? "OrderShipmentUuid".ToCamelCase(camelCase)} ";
        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid".ToCamelCase(camelCase)} ";
        public static string InvoiceNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InvoiceNumber) AS {name ?? "InvoiceNumber".ToCamelCase(camelCase)} ";
        public static string InvoiceUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InvoiceUuid) AS {name ?? "InvoiceUuid".ToCamelCase(camelCase)} ";
        public static string SalesOrderUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SalesOrderUuid) AS {name ?? "SalesOrderUuid".ToCamelCase(camelCase)} ";
        public static string OrderNumber(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OrderNumber) AS {name ?? "OrderNumber".ToCamelCase(camelCase)} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}OrderShipmentNum AS OrderShipmentNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
{allies}ChannelNum AS ChannelNum,
{allies}ChannelAccountNum AS ChannelAccountNum,
{allies}OrderDCAssignmentNum AS OrderDCAssignmentNum,
{allies}DistributionCenterNum AS DistributionCenterNum,
{allies}CentralOrderNum AS CentralOrderNum,
RTRIM({allies}ChannelOrderID) AS ChannelOrderID,
RTRIM({allies}ShipmentID) AS ShipmentID,
RTRIM({allies}WarehouseCode) AS WarehouseCode,
{allies}ShipmentType AS ShipmentType,
RTRIM({allies}ShipmentReferenceID) AS ShipmentReferenceID,
{allies}ShipmentDateUtc AS ShipmentDateUtc,
RTRIM({allies}ShippingCarrier) AS ShippingCarrier,
RTRIM({allies}ShippingClass) AS ShippingClass,
{allies}ShippingCost AS ShippingCost,
RTRIM({allies}MainTrackingNumber) AS MainTrackingNumber,
RTRIM({allies}MainReturnTrackingNumber) AS MainReturnTrackingNumber,
RTRIM({allies}BillOfLadingID) AS BillOfLadingID,
{allies}TotalPackages AS TotalPackages,
{allies}TotalShippedQty AS TotalShippedQty,
{allies}TotalCanceledQty AS TotalCanceledQty,
{allies}TotalWeight AS TotalWeight,
{allies}TotalVolume AS TotalVolume,
{allies}WeightUnit AS WeightUnit,
{allies}LengthUnit AS LengthUnit,
{allies}VolumeUnit AS VolumeUnit,
{allies}ShipmentStatus AS ShipmentStatus,
RTRIM({allies}DBChannelOrderHeaderRowID) AS DBChannelOrderHeaderRowID,
{allies}ProcessStatus AS ProcessStatus,
{allies}ProcessDateUtc AS ProcessDateUtc,
{allies}EnterDateUtc AS EnterDateUtc,
RTRIM({allies}OrderShipmentUuid) AS OrderShipmentUuid,
{allies}RowNum AS RowNum,
{allies}DigitBridgeGuid AS DigitBridgeGuid,
RTRIM({allies}InvoiceNumber) AS InvoiceNumber,
RTRIM({allies}InvoiceUuid) AS InvoiceUuid,
RTRIM({allies}SalesOrderUuid) AS SalesOrderUuid,
RTRIM({allies}OrderNumber) AS OrderNumber
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

