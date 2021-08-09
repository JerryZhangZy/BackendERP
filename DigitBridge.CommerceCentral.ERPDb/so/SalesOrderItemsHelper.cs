




              



              
    

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
    /// Represents a SalesOrderItems SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class SalesOrderItemsHelper
    {
        public static readonly string TableName = "SalesOrderItems";
        public static readonly string TableAllies = "ordl";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string SalesOrderItemsUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SalesOrderItemsUuid) AS {name ?? "SalesOrderItemsUuid".ToCamelCase(camelCase)} ";
        public static string SalesOrderUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SalesOrderUuid) AS {name ?? "SalesOrderUuid".ToCamelCase(camelCase)} ";
        public static string Seq(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Seq AS {name ?? "Seq".ToCamelCase(camelCase)} ";
        public static string OrderItemType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderItemType AS {name ?? "OrderItemType".ToCamelCase(camelCase)} ";
        public static string SalesOrderItemstatus(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.SalesOrderItemstatus AS {name ?? "SalesOrderItemstatus".ToCamelCase(camelCase)} ";
        public static string ItemDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemDate AS {name ?? "ItemDate".ToCamelCase(camelCase)} ";
        public static string ItemTime(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemTime AS {name ?? "ItemTime".ToCamelCase(camelCase)} ";
        public static string ShipDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipDate AS {name ?? "ShipDate".ToCamelCase(camelCase)} ";
        public static string EtaArrivalDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EtaArrivalDate AS {name ?? "EtaArrivalDate".ToCamelCase(camelCase)} ";
        public static string SKU(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SKU) AS {name ?? "SKU".ToCamelCase(camelCase)} ";
        public static string ProductUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ProductUuid) AS {name ?? "ProductUuid".ToCamelCase(camelCase)} ";
        public static string InventoryUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.InventoryUuid) AS {name ?? "InventoryUuid".ToCamelCase(camelCase)} ";
        public static string WarehouseUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseUuid) AS {name ?? "WarehouseUuid".ToCamelCase(camelCase)} ";
        public static string WarehouseCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseCode) AS {name ?? "WarehouseCode".ToCamelCase(camelCase)} ";
        public static string LotNum(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LotNum) AS {name ?? "LotNum".ToCamelCase(camelCase)} ";
        public static string Description(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description".ToCamelCase(camelCase)} ";
        public static string Notes(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Notes) AS {name ?? "Notes".ToCamelCase(camelCase)} ";
        public static string Currency(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency".ToCamelCase(camelCase)} ";
        public static string UOM(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UOM) AS {name ?? "UOM".ToCamelCase(camelCase)} ";
        public static string PackType(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PackType) AS {name ?? "PackType".ToCamelCase(camelCase)} ";
        public static string PackQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.PackQty AS {name ?? "PackQty".ToCamelCase(camelCase)} ";
        public static string OrderPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderPack AS {name ?? "OrderPack".ToCamelCase(camelCase)} ";
        public static string ShipPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipPack AS {name ?? "ShipPack".ToCamelCase(camelCase)} ";
        public static string CancelledPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CancelledPack AS {name ?? "CancelledPack".ToCamelCase(camelCase)} ";
        public static string OpenPack(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OpenPack AS {name ?? "OpenPack".ToCamelCase(camelCase)} ";
        public static string OrderQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OrderQty AS {name ?? "OrderQty".ToCamelCase(camelCase)} ";
        public static string ShipQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipQty AS {name ?? "ShipQty".ToCamelCase(camelCase)} ";
        public static string CancelledQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CancelledQty AS {name ?? "CancelledQty".ToCamelCase(camelCase)} ";
        public static string OpenQty(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OpenQty AS {name ?? "OpenQty".ToCamelCase(camelCase)} ";
        public static string PriceRule(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.PriceRule) AS {name ?? "PriceRule".ToCamelCase(camelCase)} ";
        public static string Price(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Price AS {name ?? "Price".ToCamelCase(camelCase)} ";
        public static string DiscountRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountRate AS {name ?? "DiscountRate".ToCamelCase(camelCase)} ";
        public static string DiscountAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountAmount AS {name ?? "DiscountAmount".ToCamelCase(camelCase)} ";
        public static string DiscountPrice(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DiscountPrice AS {name ?? "DiscountPrice".ToCamelCase(camelCase)} ";
        public static string ExtAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ExtAmount AS {name ?? "ExtAmount".ToCamelCase(camelCase)} ";
        public static string TaxableAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxableAmount AS {name ?? "TaxableAmount".ToCamelCase(camelCase)} ";
        public static string NonTaxableAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.NonTaxableAmount AS {name ?? "NonTaxableAmount".ToCamelCase(camelCase)} ";
        public static string TaxRate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxRate AS {name ?? "TaxRate".ToCamelCase(camelCase)} ";
        public static string TaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.TaxAmount AS {name ?? "TaxAmount".ToCamelCase(camelCase)} ";
        public static string ShippingAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingAmount AS {name ?? "ShippingAmount".ToCamelCase(camelCase)} ";
        public static string ShippingTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShippingTaxAmount AS {name ?? "ShippingTaxAmount".ToCamelCase(camelCase)} ";
        public static string MiscAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscAmount AS {name ?? "MiscAmount".ToCamelCase(camelCase)} ";
        public static string MiscTaxAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MiscTaxAmount AS {name ?? "MiscTaxAmount".ToCamelCase(camelCase)} ";
        public static string ChargeAndAllowanceAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ChargeAndAllowanceAmount AS {name ?? "ChargeAndAllowanceAmount".ToCamelCase(camelCase)} ";
        public static string ItemTotalAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ItemTotalAmount AS {name ?? "ItemTotalAmount".ToCamelCase(camelCase)} ";
        public static string ShipAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ShipAmount AS {name ?? "ShipAmount".ToCamelCase(camelCase)} ";
        public static string CancelledAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CancelledAmount AS {name ?? "CancelledAmount".ToCamelCase(camelCase)} ";
        public static string OpenAmount(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.OpenAmount AS {name ?? "OpenAmount".ToCamelCase(camelCase)} ";
        public static string Stockable(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Stockable AS {name ?? "Stockable".ToCamelCase(camelCase)} ";
        public static string IsAr(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.IsAr AS {name ?? "IsAr".ToCamelCase(camelCase)} ";
        public static string Taxable(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Taxable AS {name ?? "Taxable".ToCamelCase(camelCase)} ";
        public static string Costable(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Costable AS {name ?? "Costable".ToCamelCase(camelCase)} ";
        public static string IsProfit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.IsProfit AS {name ?? "IsProfit".ToCamelCase(camelCase)} ";
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
RTRIM({allies}SalesOrderItemsUuid) AS SalesOrderItemsUuid,
RTRIM({allies}SalesOrderUuid) AS SalesOrderUuid,
{allies}Seq AS Seq,
{allies}OrderItemType AS OrderItemType,
{allies}SalesOrderItemstatus AS SalesOrderItemstatus,
{allies}ItemDate AS ItemDate,
{allies}ItemTime AS ItemTime,
{allies}ShipDate AS ShipDate,
{allies}EtaArrivalDate AS EtaArrivalDate,
RTRIM({allies}SKU) AS SKU,
RTRIM({allies}ProductUuid) AS ProductUuid,
RTRIM({allies}InventoryUuid) AS InventoryUuid,
RTRIM({allies}WarehouseUuid) AS WarehouseUuid,
RTRIM({allies}WarehouseCode) AS WarehouseCode,
RTRIM({allies}LotNum) AS LotNum,
RTRIM({allies}Description) AS Description,
RTRIM({allies}Notes) AS Notes,
RTRIM({allies}Currency) AS Currency,
RTRIM({allies}UOM) AS UOM,
RTRIM({allies}PackType) AS PackType,
{allies}PackQty AS PackQty,
{allies}OrderPack AS OrderPack,
{allies}ShipPack AS ShipPack,
{allies}CancelledPack AS CancelledPack,
{allies}OpenPack AS OpenPack,
{allies}OrderQty AS OrderQty,
{allies}ShipQty AS ShipQty,
{allies}CancelledQty AS CancelledQty,
{allies}OpenQty AS OpenQty,
RTRIM({allies}PriceRule) AS PriceRule,
{allies}Price AS Price,
{allies}DiscountRate AS DiscountRate,
{allies}DiscountAmount AS DiscountAmount,
{allies}DiscountPrice AS DiscountPrice,
{allies}ExtAmount AS ExtAmount,
{allies}TaxableAmount AS TaxableAmount,
{allies}NonTaxableAmount AS NonTaxableAmount,
{allies}TaxRate AS TaxRate,
{allies}TaxAmount AS TaxAmount,
{allies}ShippingAmount AS ShippingAmount,
{allies}ShippingTaxAmount AS ShippingTaxAmount,
{allies}MiscAmount AS MiscAmount,
{allies}MiscTaxAmount AS MiscTaxAmount,
{allies}ChargeAndAllowanceAmount AS ChargeAndAllowanceAmount,
{allies}ItemTotalAmount AS ItemTotalAmount,
{allies}ShipAmount AS ShipAmount,
{allies}CancelledAmount AS CancelledAmount,
{allies}OpenAmount AS OpenAmount,
{allies}Stockable AS Stockable,
{allies}IsAr AS IsAr,
{allies}Taxable AS Taxable,
{allies}Costable AS Costable,
{allies}IsProfit AS IsProfit,
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

