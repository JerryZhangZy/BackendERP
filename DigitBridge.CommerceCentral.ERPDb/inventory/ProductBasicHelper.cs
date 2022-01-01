




              



              
    

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
    /// Represents a ProductBasic SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ProductBasicHelper
    {
        public static readonly string TableName = "ProductBasic";
        public static readonly string TableAllies = "prd";

        public static string From(string TableAllies = null) => $"FROM {TableName} {TableAllies ?? TableAllies} ";
        public static string InnerJoin(string TableAllies = null) => $"INNER JOIN {TableName} {TableAllies ?? TableAllies} ";
        public static string LeftJoin(string TableAllies = null) => $"LEFT JOIN {TableName} {TableAllies ?? TableAllies} ";

        #region - static SQL fileds statement

        public static string CentralProductNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CentralProductNum AS {name ?? "CentralProductNum".ToCamelCase(camelCase)} ";
        public static string DatabaseNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DatabaseNum AS {name ?? "DatabaseNum".ToCamelCase(camelCase)} ";
        public static string MasterAccountNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MasterAccountNum AS {name ?? "MasterAccountNum".ToCamelCase(camelCase)} ";
        public static string ProfileNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProfileNum AS {name ?? "ProfileNum".ToCamelCase(camelCase)} ";
        public static string SKU(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.SKU) AS {name ?? "SKU".ToCamelCase(camelCase)} ";
        public static string FNSku(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.FNSku) AS {name ?? "FNSku".ToCamelCase(camelCase)} ";
        public static string Condition(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Condition AS {name ?? "Condition".ToCamelCase(camelCase)} ";
        public static string Brand(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Brand) AS {name ?? "Brand".ToCamelCase(camelCase)} ";
        public static string Manufacturer(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Manufacturer) AS {name ?? "Manufacturer".ToCamelCase(camelCase)} ";
        public static string ProductTitle(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ProductTitle) AS {name ?? "ProductTitle".ToCamelCase(camelCase)} ";
        public static string LongDescription(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.LongDescription) AS {name ?? "LongDescription".ToCamelCase(camelCase)} ";
        public static string ShortDescription(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ShortDescription) AS {name ?? "ShortDescription".ToCamelCase(camelCase)} ";
        public static string Subtitle(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Subtitle) AS {name ?? "Subtitle".ToCamelCase(camelCase)} ";
        public static string ASIN(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ASIN) AS {name ?? "ASIN".ToCamelCase(camelCase)} ";
        public static string UPC(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UPC) AS {name ?? "UPC".ToCamelCase(camelCase)} ";
        public static string EAN(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.EAN) AS {name ?? "EAN".ToCamelCase(camelCase)} ";
        public static string ISBN(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ISBN) AS {name ?? "ISBN".ToCamelCase(camelCase)} ";
        public static string MPN(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.MPN) AS {name ?? "MPN".ToCamelCase(camelCase)} ";
        public static string Price(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Price AS {name ?? "Price".ToCamelCase(camelCase)} ";
        public static string Cost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.Cost AS {name ?? "Cost".ToCamelCase(camelCase)} ";
        public static string AvgCost(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.AvgCost AS {name ?? "AvgCost".ToCamelCase(camelCase)} ";
        public static string MAPPrice(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MAPPrice AS {name ?? "MAPPrice".ToCamelCase(camelCase)} ";
        public static string MSRP(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MSRP AS {name ?? "MSRP".ToCamelCase(camelCase)} ";
        public static string BundleType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BundleType AS {name ?? "BundleType".ToCamelCase(camelCase)} ";
        public static string ProductType(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProductType AS {name ?? "ProductType".ToCamelCase(camelCase)} ";
        public static string VariationVaryBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VariationVaryBy) AS {name ?? "VariationVaryBy".ToCamelCase(camelCase)} ";
        public static string CopyToChildren(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CopyToChildren AS {name ?? "CopyToChildren".ToCamelCase(camelCase)} ";
        public static string MultipackQuantity(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.MultipackQuantity AS {name ?? "MultipackQuantity".ToCamelCase(camelCase)} ";
        public static string VariationParentSKU(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.VariationParentSKU) AS {name ?? "VariationParentSKU".ToCamelCase(camelCase)} ";
        public static string IsInRelationship(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.IsInRelationship AS {name ?? "IsInRelationship".ToCamelCase(camelCase)} ";
        public static string NetWeight(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.NetWeight AS {name ?? "NetWeight".ToCamelCase(camelCase)} ";
        public static string GrossWeight(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.GrossWeight AS {name ?? "GrossWeight".ToCamelCase(camelCase)} ";
        public static string WeightUnit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.WeightUnit AS {name ?? "WeightUnit".ToCamelCase(camelCase)} ";
        public static string ProductHeight(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProductHeight AS {name ?? "ProductHeight".ToCamelCase(camelCase)} ";
        public static string ProductLength(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProductLength AS {name ?? "ProductLength".ToCamelCase(camelCase)} ";
        public static string ProductWidth(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ProductWidth AS {name ?? "ProductWidth".ToCamelCase(camelCase)} ";
        public static string BoxHeight(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BoxHeight AS {name ?? "BoxHeight".ToCamelCase(camelCase)} ";
        public static string BoxLength(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BoxLength AS {name ?? "BoxLength".ToCamelCase(camelCase)} ";
        public static string BoxWidth(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.BoxWidth AS {name ?? "BoxWidth".ToCamelCase(camelCase)} ";
        public static string DimensionUnit(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DimensionUnit AS {name ?? "DimensionUnit".ToCamelCase(camelCase)} ";
        public static string HarmonizedCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.HarmonizedCode) AS {name ?? "HarmonizedCode".ToCamelCase(camelCase)} ";
        public static string TaxProductCode(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.TaxProductCode) AS {name ?? "TaxProductCode".ToCamelCase(camelCase)} ";
        public static string IsBlocked(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.IsBlocked AS {name ?? "IsBlocked".ToCamelCase(camelCase)} ";
        public static string Warranty(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.Warranty) AS {name ?? "Warranty".ToCamelCase(camelCase)} ";
        public static string CreateBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.CreateBy) AS {name ?? "CreateBy".ToCamelCase(camelCase)} ";
        public static string UpdateBy(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.UpdateBy) AS {name ?? "UpdateBy".ToCamelCase(camelCase)} ";
        public static string CreateDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.CreateDate AS {name ?? "CreateDate".ToCamelCase(camelCase)} ";
        public static string UpdateDate(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.UpdateDate AS {name ?? "UpdateDate".ToCamelCase(camelCase)} ";
        public static string ClassificationNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.ClassificationNum AS {name ?? "ClassificationNum".ToCamelCase(camelCase)} ";
        public static string RowNum(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.RowNum AS {name ?? "RowNum".ToCamelCase(camelCase)} ";
        public static string OriginalUPC(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.OriginalUPC) AS {name ?? "OriginalUPC".ToCamelCase(camelCase)} ";
        public static string ProductUuid(string tableAllies = null, string name = null, bool camelCase = true) => $"RTRIM({tableAllies ?? TableAllies}.ProductUuid) AS {name ?? "ProductUuid".ToCamelCase(camelCase)} ";
        public static string EnterDateUtc(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.EnterDateUtc AS {name ?? "EnterDateUtc".ToCamelCase(camelCase)} ";
        public static string DigitBridgeGuid(string tableAllies = null, string name = null, bool camelCase = true) => $"{tableAllies ?? TableAllies}.DigitBridgeGuid AS {name ?? "DigitBridgeGuid".ToCamelCase(camelCase)} ";

        #endregion - static SQL fileds statement

        public static string SelectAll(string tableAllies = null) 
        {
            var allies = string.IsNullOrEmpty(tableAllies) ? string.Empty : $"{tableAllies.TrimEnd()}.";
            return $@"
{allies}CentralProductNum AS CentralProductNum,
{allies}DatabaseNum AS DatabaseNum,
{allies}MasterAccountNum AS MasterAccountNum,
{allies}ProfileNum AS ProfileNum,
RTRIM({allies}SKU) AS SKU,
RTRIM({allies}FNSku) AS FNSku,
{allies}Condition AS Condition,
RTRIM({allies}Brand) AS Brand,
RTRIM({allies}Manufacturer) AS Manufacturer,
RTRIM({allies}ProductTitle) AS ProductTitle,
RTRIM({allies}LongDescription) AS LongDescription,
RTRIM({allies}ShortDescription) AS ShortDescription,
RTRIM({allies}Subtitle) AS Subtitle,
RTRIM({allies}ASIN) AS ASIN,
RTRIM({allies}UPC) AS UPC,
RTRIM({allies}EAN) AS EAN,
RTRIM({allies}ISBN) AS ISBN,
RTRIM({allies}MPN) AS MPN,
{allies}Price AS Price,
{allies}Cost AS Cost,
{allies}AvgCost AS AvgCost,
{allies}MAPPrice AS MAPPrice,
{allies}MSRP AS MSRP,
{allies}BundleType AS BundleType,
{allies}ProductType AS ProductType,
RTRIM({allies}VariationVaryBy) AS VariationVaryBy,
{allies}CopyToChildren AS CopyToChildren,
{allies}MultipackQuantity AS MultipackQuantity,
RTRIM({allies}VariationParentSKU) AS VariationParentSKU,
{allies}IsInRelationship AS IsInRelationship,
{allies}NetWeight AS NetWeight,
{allies}GrossWeight AS GrossWeight,
{allies}WeightUnit AS WeightUnit,
{allies}ProductHeight AS ProductHeight,
{allies}ProductLength AS ProductLength,
{allies}ProductWidth AS ProductWidth,
{allies}BoxHeight AS BoxHeight,
{allies}BoxLength AS BoxLength,
{allies}BoxWidth AS BoxWidth,
{allies}DimensionUnit AS DimensionUnit,
RTRIM({allies}HarmonizedCode) AS HarmonizedCode,
RTRIM({allies}TaxProductCode) AS TaxProductCode,
{allies}IsBlocked AS IsBlocked,
RTRIM({allies}Warranty) AS Warranty,
RTRIM({allies}CreateBy) AS CreateBy,
RTRIM({allies}UpdateBy) AS UpdateBy,
{allies}CreateDate AS CreateDate,
{allies}UpdateDate AS UpdateDate,
{allies}ClassificationNum AS ClassificationNum,
{allies}RowNum AS RowNum,
RTRIM({allies}OriginalUPC) AS OriginalUPC,
RTRIM({allies}ProductUuid) AS ProductUuid,
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

