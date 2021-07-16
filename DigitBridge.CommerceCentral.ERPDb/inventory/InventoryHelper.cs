
              
    

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
    /// Represents a Inventory SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InventoryHelper
    {
        public static readonly string TableName = "Inventory";
        public static readonly string TableAllies = "inv";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string RowNum(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum"} ";
        public static string DatabaseNum(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum"} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum"} ";
        public static string ProfileNum(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum"} ";
        public static string ProductUuid(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.ProductUuid) AS {name ?? "ProductUuid"} ";
        public static string InventoryUuid(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.InventoryUuid) AS {name ?? "InventoryUuid"} ";
        public static string StyleCode(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.StyleCode) AS {name ?? "StyleCode"} ";
        public static string Color(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Color) AS {name ?? "Color"} ";
        public static string SizeType(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.SizeType) AS {name ?? "SizeType"} ";
        public static string SizeSystem(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.SizeSystem) AS {name ?? "SizeSystem"} ";
        public static string Size(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Size) AS {name ?? "Size"} ";
        public static string Width(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Width) AS {name ?? "Width"} ";
        public static string Length(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Length) AS {name ?? "Length"} ";
        public static string ClassCode(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.ClassCode) AS {name ?? "ClassCode"} ";
        public static string Department(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Department) AS {name ?? "Department"} ";
        public static string Division(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Division) AS {name ?? "Division"} ";
        public static string Year(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Year) AS {name ?? "Year"} ";
        public static string PriceRule(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.PriceRule) AS {name ?? "PriceRule"} ";
        public static string LeadTimeDay(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.LeadTimeDay AS {name ?? "LeadTimeDay"} ";
        public static string OrderSize(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OrderSize AS {name ?? "OrderSize"} ";
        public static string MinStock(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.MinStock AS {name ?? "MinStock"} ";
        public static string SKU(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.SKU) AS {name ?? "SKU"} ";
        public static string Description(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Description) AS {name ?? "Description"} ";
        public static string WarehouseUuid(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseUuid) AS {name ?? "WarehouseUuid"} ";
        public static string WarehouseNum(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseNum) AS {name ?? "WarehouseNum"} ";
        public static string WarehouseName(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.WarehouseName) AS {name ?? "WarehouseName"} ";
        public static string LotNum(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.LotNum) AS {name ?? "LotNum"} ";
        public static string LotInDate(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.LotInDate AS {name ?? "LotInDate"} ";
        public static string LotExpDate(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.LotExpDate AS {name ?? "LotExpDate"} ";
        public static string LotDescription(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.LotDescription) AS {name ?? "LotDescription"} ";
        public static string LpnNum(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.LpnNum) AS {name ?? "LpnNum"} ";
        public static string LpnDescription(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.LpnDescription) AS {name ?? "LpnDescription"} ";
        public static string Notes(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Notes) AS {name ?? "Notes"} ";
        public static string Currency(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.Currency) AS {name ?? "Currency"} ";
        public static string UOM(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.UOM) AS {name ?? "UOM"} ";
        public static string PackType(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.PackType) AS {name ?? "PackType"} ";
        public static string PackQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.PackQty AS {name ?? "PackQty"} ";
        public static string QtyPerPallot(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.QtyPerPallot AS {name ?? "QtyPerPallot"} ";
        public static string QtyPerCase(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.QtyPerCase AS {name ?? "QtyPerCase"} ";
        public static string QtyPerBox(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.QtyPerBox AS {name ?? "QtyPerBox"} ";
        public static string DefaultPackType(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.DefaultPackType) AS {name ?? "DefaultPackType"} ";
        public static string Instock(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.Instock AS {name ?? "Instock"} ";
        public static string OnHand(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OnHand AS {name ?? "OnHand"} ";
        public static string OpenSoQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OpenSoQty AS {name ?? "OpenSoQty"} ";
        public static string OpenFulfillmentQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OpenFulfillmentQty AS {name ?? "OpenFulfillmentQty"} ";
        public static string AvaQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.AvaQty AS {name ?? "AvaQty"} ";
        public static string OpenPoQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OpenPoQty AS {name ?? "OpenPoQty"} ";
        public static string OpenInTransitQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OpenInTransitQty AS {name ?? "OpenInTransitQty"} ";
        public static string OpenWipQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.OpenWipQty AS {name ?? "OpenWipQty"} ";
        public static string ProjectedQty(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.ProjectedQty AS {name ?? "ProjectedQty"} ";
        public static string BaseCost(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.BaseCost AS {name ?? "BaseCost"} ";
        public static string TaxRate(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.TaxRate AS {name ?? "TaxRate"} ";
        public static string TaxAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.TaxAmount AS {name ?? "TaxAmount"} ";
        public static string DiscountRate(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.DiscountRate AS {name ?? "DiscountRate"} ";
        public static string DiscountAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.DiscountAmount AS {name ?? "DiscountAmount"} ";
        public static string ShippingAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.ShippingAmount AS {name ?? "ShippingAmount"} ";
        public static string ShippingTaxAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.ShippingTaxAmount AS {name ?? "ShippingTaxAmount"} ";
        public static string MiscAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.MiscAmount AS {name ?? "MiscAmount"} ";
        public static string MiscTaxAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.MiscTaxAmount AS {name ?? "MiscTaxAmount"} ";
        public static string ChargeAndAllowanceAmount(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.ChargeAndAllowanceAmount AS {name ?? "ChargeAndAllowanceAmount"} ";
        public static string UnitCost(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.UnitCost AS {name ?? "UnitCost"} ";
        public static string AvgCost(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.AvgCost AS {name ?? "AvgCost"} ";
        public static string SalesCost(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.SalesCost AS {name ?? "SalesCost"} ";
        public static string Stockable(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.Stockable AS {name ?? "Stockable"} ";
        public static string IsAr(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.IsAr AS {name ?? "IsAr"} ";
        public static string IsAp(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.IsAp AS {name ?? "IsAp"} ";
        public static string Taxable(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.Taxable AS {name ?? "Taxable"} ";
        public static string Costable(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.Costable AS {name ?? "Costable"} ";
        public static string UpdateDateUtc(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.UpdateDateUtc AS {name ?? "UpdateDateUtc"} ";
        public static string EnterBy(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.EnterBy) AS {name ?? "EnterBy"} ";
        public static string UpdateBy(string tableAllies = null, string name = null) => $"RTRIM({tableAllies ?? TableAllies}.UpdateBy) AS {name ?? "UpdateBy"} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc"} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid"} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}RowNum AS RowNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
RTRIM({allies}ProductUuid) AS ProductUuid,
RTRIM({allies}InventoryUuid) AS InventoryUuid,
RTRIM({allies}StyleCode) AS StyleCode,
RTRIM({allies}Color) AS Color,
RTRIM({allies}SizeType) AS SizeType,
RTRIM({allies}SizeSystem) AS SizeSystem,
RTRIM({allies}Size) AS Size,
RTRIM({allies}Width) AS Width,
RTRIM({allies}Length) AS Length,
RTRIM({allies}ClassCode) AS ClassCode,
RTRIM({allies}Department) AS Department,
RTRIM({allies}Division) AS Division,
RTRIM({allies}Year) AS Year,
RTRIM({allies}PriceRule) AS PriceRule,
{allies}LeadTimeDay AS LeadTimeDay,
{allies}OrderSize AS OrderSize,
{allies}MinStock AS MinStock,
RTRIM({allies}SKU) AS SKU,
RTRIM({allies}Description) AS Description,
RTRIM({allies}WarehouseUuid) AS WarehouseUuid,
RTRIM({allies}WarehouseNum) AS WarehouseNum,
RTRIM({allies}WarehouseName) AS WarehouseName,
RTRIM({allies}LotNum) AS LotNum,
{allies}LotInDate AS LotInDate,
{allies}LotExpDate AS LotExpDate,
RTRIM({allies}LotDescription) AS LotDescription,
RTRIM({allies}LpnNum) AS LpnNum,
RTRIM({allies}LpnDescription) AS LpnDescription,
RTRIM({allies}Notes) AS Notes,
RTRIM({allies}Currency) AS Currency,
RTRIM({allies}UOM) AS UOM,
RTRIM({allies}PackType) AS PackType,
{allies}PackQty AS PackQty,
{allies}QtyPerPallot AS QtyPerPallot,
{allies}QtyPerCase AS QtyPerCase,
{allies}QtyPerBox AS QtyPerBox,
RTRIM({allies}DefaultPackType) AS DefaultPackType,
{allies}Instock AS Instock,
{allies}OnHand AS OnHand,
{allies}OpenSoQty AS OpenSoQty,
{allies}OpenFulfillmentQty AS OpenFulfillmentQty,
{allies}AvaQty AS AvaQty,
{allies}OpenPoQty AS OpenPoQty,
{allies}OpenInTransitQty AS OpenInTransitQty,
{allies}OpenWipQty AS OpenWipQty,
{allies}ProjectedQty AS ProjectedQty,
{allies}BaseCost AS BaseCost,
{allies}TaxRate AS TaxRate,
{allies}TaxAmount AS TaxAmount,
{allies}DiscountRate AS DiscountRate,
{allies}DiscountAmount AS DiscountAmount,
{allies}ShippingAmount AS ShippingAmount,
{allies}ShippingTaxAmount AS ShippingTaxAmount,
{allies}MiscAmount AS MiscAmount,
{allies}MiscTaxAmount AS MiscTaxAmount,
{allies}ChargeAndAllowanceAmount AS ChargeAndAllowanceAmount,
{allies}UnitCost AS UnitCost,
{allies}AvgCost AS AvgCost,
{allies}SalesCost AS SalesCost,
{allies}Stockable AS Stockable,
{allies}IsAr AS IsAr,
{allies}IsAp AS IsAp,
{allies}Taxable AS Taxable,
{allies}Costable AS Costable,
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

