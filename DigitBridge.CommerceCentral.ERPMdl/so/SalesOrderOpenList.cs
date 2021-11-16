using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderHelper;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderInfoHelper;
using InfoAttrHelper = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderAttributesHelper;
using ItemHelper = DigitBridge.CommerceCentral.ERPDb.SalesOrderItemsHelper;
using ItemAttrHelper = DigitBridge.CommerceCentral.ERPDb.SalesOrderItemsAttributesHelper;
using EventHelper = DigitBridge.CommerceCentral.ERPDb.EventProcessERPHelper;
using ProdcutHelper = DigitBridge.CommerceCentral.ERPDb.ProductBasicHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderOpenList : SqlQueryBuilder<SalesOrderOpenQuery>
    {
        public SalesOrderOpenList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public SalesOrderOpenList(IDataBaseFactory dbFactory, SalesOrderOpenQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region get select columns
        protected string GetHeader_Columns()
        {
            var columns = $@" 
--{EventHelper.TableAllies}.EventUuid,
{Helper.TableAllies}.SalesOrderUuid as 'SalesOrderUuid',
{InfoHelper.TableAllies}.WarehouseCode as 'WarehouseCode',
{Helper.TableAllies}.DatabaseNum as 'CentralDatabaseNum',
{InfoHelper.TableAllies}.CentralOrderNum as 'CentralOrderNum',
{InfoHelper.TableAllies}.ChannelNum as 'ChannelNum',
{InfoHelper.TableAllies}.ChannelAccountNum as 'ChannelAccountNum',
channelAccount.ChannelAccountName as 'ChannelAccountName',
{InfoHelper.TableAllies}.ChannelOrderId as 'ChannelOrderId',
{InfoHelper.TableAllies}.SecondaryChannelOrderId as 'SecondaryChannelOrderId',
{Helper.TableAllies}.OrderNumber as 'SellerOrderId',
{Helper.TableAllies}.Currency as 'Currency',
{Helper.TableAllies}.OrderDate as 'OriginalOrderDate',
{OrderHeaderHelper.TableAllies}.SellerPublicNote as 'SellerPublicNotes',
{OrderHeaderHelper.TableAllies}.SellerPrivateNote as 'SellerPrivateNotes',
{OrderHeaderHelper.TableAllies}.EndBuyerInstruction as 'EndBuyerInstruction',
{Helper.TableAllies}.TotalAmount as 'TotalOrderAmount',
{Helper.TableAllies}.TaxAmount as 'TotalTaxAmount',
{Helper.TableAllies}.ShippingAmount as 'TotalShippingAmount',
{Helper.TableAllies}.ShippingTaxAmount as 'TotalShippingTaxAmount',

{OrderHeaderHelper.TableAllies}.TotalShippingDiscount as 'TotalShippingDiscount',
{OrderHeaderHelper.TableAllies}.TotalShippingDiscountTaxAmount as 'TotalShippingDiscountTaxAmount',
{OrderHeaderHelper.TableAllies}.TotalInsuranceAmount as 'TotalInsuranceAmount',
{OrderHeaderHelper.TableAllies}.TotalGiftOptionAmount as 'TotalGiftOptionAmount',
{OrderHeaderHelper.TableAllies}.TotalGiftOptionTaxAmount as 'TotalGiftOptionTaxAmount',
{OrderHeaderHelper.TableAllies}.AdditionalCostOrDiscount as 'AdditionalCostOrDiscount',


{Helper.TableAllies}.DiscountAmount as 'PromotionAmount',
{InfoHelper.TableAllies}.ShippingCarrier as 'RequestShippingCarrier',
{InfoHelper.TableAllies}.ShippingClass as 'RequestShippingService',
{InfoHelper.TableAllies}.ShipToName as 'ShipToName',
{InfoHelper.TableAllies}.ShipToFirstName as 'ShipToFirstName',
{InfoHelper.TableAllies}.ShipToLastName as 'ShipToLastName',
{InfoHelper.TableAllies}.ShipToSuffix as 'ShipToSuffix',
{InfoHelper.TableAllies}.ShipToCompany as 'ShipToCompany',
{InfoHelper.TableAllies}.ShipToCompanyJobTitle as 'ShipToCompanyJobTitle',
{InfoHelper.TableAllies}.ShipToAttention as 'ShipToAttention',
{InfoHelper.TableAllies}.ShipToDaytimePhone as 'ShipToDaytimePhone',
{InfoHelper.TableAllies}.ShipToNightPhone as 'ShipToNightPhone',
{InfoHelper.TableAllies}.ShipToAddressLine1 as 'ShipToAddressLine1',
{InfoHelper.TableAllies}.ShipToAddressLine2 as 'ShipToAddressLine2',
{InfoHelper.TableAllies}.ShipToAddressLine3 as 'ShipToAddressLine3',
{InfoHelper.TableAllies}.ShipToCity as 'ShipToCity',
{InfoHelper.TableAllies}.ShipToState as 'ShipToState',
{InfoHelper.TableAllies}.ShipToStateFullName as 'ShipToStateFullName',
{InfoHelper.TableAllies}.ShipToPostalCode as 'ShipToPostalCode',
{InfoHelper.TableAllies}.ShipToPostalCodeExt as 'ShipToPostalCodeExt',
{InfoHelper.TableAllies}.ShipToCounty as 'ShipToCounty',
{InfoHelper.TableAllies}.ShipToCountry as 'ShipToCountry',
{InfoHelper.TableAllies}.ShipToEmail as 'ShipToEmail',
{InfoHelper.TableAllies}.BillToName as 'BillToName',
{InfoHelper.TableAllies}.BillToFirstName as 'BillToFirstName',
{InfoHelper.TableAllies}.BillToLastName as 'BillToLastName',
{InfoHelper.TableAllies}.BillToSuffix as 'BillToSuffix',
{InfoHelper.TableAllies}.BillToCompany as 'BillToCompany',
{InfoHelper.TableAllies}.BillToCompanyJobTitle as 'BillToCompanyJobTitle',
{InfoHelper.TableAllies}.BillToAttention as 'BillToAttention',
{InfoHelper.TableAllies}.BillToAddressLine1 as 'BillToAddressLine1',
{InfoHelper.TableAllies}.BillToAddressLine2 as 'BillToAddressLine2',
{InfoHelper.TableAllies}.BillToAddressLine3 as 'BillToAddressLine3',
{InfoHelper.TableAllies}.BillToCity as 'BillToCity',
{InfoHelper.TableAllies}.BillToState as 'BillToState',
{InfoHelper.TableAllies}.BillToStateFullName as 'BillToStateFullName',
{InfoHelper.TableAllies}.BillToPostalCode as 'BillToPostalCode',
{InfoHelper.TableAllies}.BillToPostalCodeExt as 'BillToPostalCodeExt',
{InfoHelper.TableAllies}.BillToCounty as 'BillToCounty',
{InfoHelper.TableAllies}.BillToCountry as 'BillToCountry',
{InfoHelper.TableAllies}.BillToEmail as 'BillToEmail',
{InfoHelper.TableAllies}.BillToDaytimePhone as 'BillToDaytimePhone',
{InfoHelper.TableAllies}.BillToNightPhone as 'BillToNightPhone'
";
            return columns;
        }
        protected string GetItem_Columns()
        {
            var columns = $@"
{ItemHelper.TableAllies}.SalesOrderItemsUuid as 'SalesOrderItemsUuid',
{ItemHelper.TableAllies}.SKU as 'SKU',
CAST({ ItemHelper.TableAllies}.OrderQty as INT) as 'OrderQty',
CAST({ ItemHelper.TableAllies}.ShipQty as INT) as 'ShipQty',
CAST({ ItemHelper.TableAllies}.CancelledQty as INT) as 'CancelQty',
{ItemHelper.TableAllies}.Price as 'UnitPrice',
{ItemHelper.TableAllies}.TaxAmount as 'LineItemTaxAmount',
{ItemHelper.TableAllies}.ShippingAmount as 'LineShippingAmount',
{ItemHelper.TableAllies}.ShippingTaxAmount as 'LineShippingTaxAmount',
{ItemHelper.TableAllies}.MiscAmount as 'LineGiftAmount',
{ItemHelper.TableAllies}.MiscTaxAmount as 'LineGiftTaxAmount',
{ItemHelper.TableAllies}.DiscountAmount as 'LinePromotionAmount',
--{ItemHelper.TableAllies}.BundleItemFulfilmentLineNum as 'BundleItemFulfilmentLineNum',
{ItemHelper.TableAllies}.EnterDateUtc as 'EnterDate' 
";
            return columns;
        }

        protected string GetSkuItem_Columns()
        {
            var columns = $@"
{ProdcutHelper.TableAllies}.CentralProductNum as 'CentralProductNum',
{ProdcutHelper.TableAllies}.UPC as 'UPC',
{ProdcutHelper.TableAllies}.ProductTitle as 'ItemTitle',
{ProdcutHelper.TableAllies}.BundleType as 'BundleType'
";
            return columns;
        }

        protected string GetOrderLineItem_Columns()
        {
            var columns = $@"
{OrderLineHelper.TableAllies}.DatabaseNum as 'CentralDatabaseNum',
{OrderLineHelper.TableAllies}.CentralOrderLineNum as 'CentralOrderLineNum',
{OrderLineHelper.TableAllies}.ChannelItemID as 'ChannelItemID',
{OrderLineHelper.TableAllies}.LineShippingDiscount as 'LineShippingDiscount',
{OrderLineHelper.TableAllies}.LineShippingDiscountTaxAmount as 'LineShippingDiscountTaxAmount',
{OrderLineHelper.TableAllies}.LineRecyclingFee as 'LineRecyclingFee',
{OrderLineHelper.TableAllies}.LineGiftMsg as 'LineGiftMsg',
{OrderLineHelper.TableAllies}.LineGiftNotes as 'LineGiftNotes',
{OrderLineHelper.TableAllies}.LinePromotionCodes as 'LinePromotionCodes',
{OrderLineHelper.TableAllies}.LinePromotionTaxAmount as 'LinePromotionTaxAmount' 
";
            return columns;
        }

        protected string GetItem_Script()
        {
            var columns = $@"
( 
SELECT 
{GetItem_Columns()},
{GetSkuItem_Columns()},
{GetOrderLineItem_Columns()}
FROM { ItemHelper.TableName} { ItemHelper.TableAllies}
LEFT JOIN {ProdcutHelper.TableName} {ProdcutHelper.TableAllies}
     ON ( {ProdcutHelper.TableAllies}.MasterAccountNum={Helper.TableAllies}.MasterAccountNum
      AND {ProdcutHelper.TableAllies}.ProfileNum={Helper.TableAllies}.ProfileNum
      AND {ProdcutHelper.TableAllies}.SKU={ItemHelper.TableAllies}.SKU
      )

LEFT JOIN {OrderLineHelper.TableName} {OrderLineHelper.TableAllies}
     ON ( {OrderLineHelper.TableAllies}.CentralOrderLineUuid={ItemHelper.TableAllies}.CentralOrderLineUuid)

WHERE { ItemHelper.TableAllies}.SalesOrderUuid = { Helper.TableAllies}.SalesOrderUuid 
FOR JSON PATH
) AS OrderLineList";
            return columns;
        }
        #endregion

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
            SELECT 
             {GetHeader_Columns()}
            ,{GetItem_Script()} 
            ";

            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{Helper.TableAllies}.MasterAccountNum";
            var profileNum = $"{Helper.TableAllies}.ProfileNum";
            var channelNum = $"{InfoHelper.TableAllies}.ChannelNum";
            var channelAccountNum = $"{InfoHelper.TableAllies}.ChannelAccountNum";

            this.SQL_From = $@"
 FROM {EventHelper.TableName} {EventHelper.TableAllies}
 INNER JOIN {Helper.TableName} {Helper.TableAllies}  
        on  {Helper.TableAllies}.MasterAccountNum={EventHelper.TableAllies}.MasterAccountNum
        and {Helper.TableAllies}.ProfileNum={EventHelper.TableAllies}.ProfileNum
        and {Helper.TableAllies}.SalesOrderUuid={EventHelper.TableAllies}.ProcessUuid
 LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({InfoHelper.TableAllies}.SalesOrderUuid = {Helper.TableAllies}.SalesOrderUuid)
 LEFT JOIN {OrderHeaderHelper.TableName} {OrderHeaderHelper.TableAllies} ON ({OrderHeaderHelper.TableAllies}.CentralOrderNum = {InfoHelper.TableAllies}.CentralOrderNum)
 {SqlStringHelper.Join_Setting_Channel(masterAccountNum, profileNum, channelNum)}
 {SqlStringHelper.Join_Setting_ChannelAccount(masterAccountNum, profileNum, channelNum, channelAccountNum)}
";
            return this.SQL_From;
        }

        //public override SqlParameter[] GetSqlParameters()
        //{
        //    var paramList = base.GetSqlParameters().ToList();
        //    paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
        //    paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

        //    return paramList.ToArray();

        //}

        protected override string GetSQL_orderBy()
        {
            this.SQL_OrderBy = $" order by {Helper.TableAllies}.UpdateDateUtc ";

            return this.SQL_OrderBy;
        }
        #endregion override methods 

        public virtual async Task GetSalesOrdersOpenListAsync(SalesOrderOpenListPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderOpenListPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {

                if (payload.IsQueryTotalCount)
                    payload.SalesOrderOpenListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.SalesOrderOpenList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.SalesOrderOpenListCount = 0;
                payload.SalesOrderOpenList = null;
                AddError(ex.ObjectToString());
            }
            payload.Messages = this.Messages;
        }
    }
}
