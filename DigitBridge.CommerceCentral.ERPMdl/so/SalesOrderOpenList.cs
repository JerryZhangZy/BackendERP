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

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
 --declare  @MasterAccountNum int=10001
 --declare  @ProfileNum int=10001
 --declare  @EventProcessActionStatus int=0
 --declare @OrderStatus_Cancelled int=255
 --declare @OrderStatus_hold int=16
 --declare @ERPEventProcessType int=2

            SELECT 
ord.SalesOrderUuid as 'SalesOrderUuid'
,ordi.WarehouseCode as 'WarehouseCode'
,ord.DatabaseNum as 'CentralDatabaseNum'
,ordi.CentralOrderNum as 'CentralOrderNum'
,ordi.ChannelNum as 'ChannelNum'
,ordi.ChannelAccountNum as 'ChannelAccountNum'
,channelAccount.ChannelAccountName as 'ChannelAccountName'
,ordi.ChannelOrderId as 'ChannelOrderId'
,ordi.SecondaryChannelOrderId as 'SecondaryChannelOrderId'
,ord.OrderNumber as 'ERPSalesOrderNumber'
,cho.SellerOrderId as 'SellerOrderId'
,ord.Currency as 'Currency'
,(cast(ord.OrderDate as varchar)+' ' +cast(ord.OrderTime as varchar)) as 'OriginalOrderDate'
,cho.SellerPublicNote as 'SellerPublicNotes'
,cho.SellerPrivateNote as 'SellerPrivateNotes'
,cho.EndBuyerInstruction as 'EndBuyerInstruction'

,ord.TotalAmount as 'TotalOrderAmount'
,ord.TaxAmount as 'TotalTaxAmount'
,ord.ShippingAmount as 'TotalShippingAmount'
,ord.ShippingTaxAmount as 'TotalShippingTaxAmount'
,cho.TotalShippingDiscount as 'TotalShippingDiscount'
,cho.TotalShippingDiscountTaxAmount as 'TotalShippingDiscountTaxAmount'
,cho.TotalInsuranceAmount as 'TotalInsuranceAmount'
,cho.TotalGiftOptionAmount as 'TotalGiftOptionAmount'
,cho.TotalGiftOptionTaxAmount as 'TotalGiftOptionTaxAmount'
,cho.AdditionalCostOrDiscount as 'AdditionalCostOrDiscount'
,ord.DiscountAmount as 'PromotionAmount'

,cho.MerchantDivision
,ccheaderext.SalesDivision 

,ord.ShipDate as 'EstimatedShipDate'
,ord.EarliestShipDate as 'EarliestShipDate'
,ord.LatestShipDate as 'LatestShipDate'
,ord.EtaArrivalDate as 'DeliverByDate'
,ord.SignatureFlag as 'SignatureFlag' 

,ordi.ShippingCarrier as 'RequestShippingCarrier'
,case when isnull(ordi.ShippingCarrier'')='' then ShippingClass
,else ordi.ShippingCarrier + ' ' + ordi.ShippingClass end as 'RequestShippingService'
,ordi.ShipToName as 'ShipToName'
,ordi.ShipToFirstName as 'ShipToFirstName'
,ordi.ShipToLastName as 'ShipToLastName'
,ordi.ShipToSuffix as 'ShipToSuffix'
,ordi.ShipToCompany as 'ShipToCompany'
,ordi.ShipToCompanyJobTitle as 'ShipToCompanyJobTitle'
,ordi.ShipToAttention as 'ShipToAttention'
,ordi.ShipToDaytimePhone as 'ShipToDaytimePhone'
,ordi.ShipToNightPhone as 'ShipToNightPhone'
,ordi.ShipToAddressLine1 as 'ShipToAddressLine1'
,ordi.ShipToAddressLine2 as 'ShipToAddressLine2'
,ordi.ShipToAddressLine3 as 'ShipToAddressLine3'
,ordi.ShipToCity as 'ShipToCity'
,ordi.ShipToState as 'ShipToState'
,ordi.ShipToStateFullName as 'ShipToStateFullName'
,ordi.ShipToPostalCode as 'ShipToPostalCode'
,ordi.ShipToPostalCodeExt as 'ShipToPostalCodeExt'
,ordi.ShipToCounty as 'ShipToCounty'
,ordi.ShipToCountry as 'ShipToCountry'
,ordi.ShipToEmail as 'ShipToEmail'
,ordi.BillToName as 'BillToName'
,ordi.BillToFirstName as 'BillToFirstName'
,ordi.BillToLastName as 'BillToLastName'
,ordi.BillToSuffix as 'BillToSuffix'
,ordi.BillToCompany as 'BillToCompany'
,ordi.BillToCompanyJobTitle as 'BillToCompanyJobTitle'
,ordi.BillToAttention as 'BillToAttention'
,ordi.BillToAddressLine1 as 'BillToAddressLine1'
,ordi.BillToAddressLine2 as 'BillToAddressLine2'
,ordi.BillToAddressLine3 as 'BillToAddressLine3'
,ordi.BillToCity as 'BillToCity'
,ordi.BillToState as 'BillToState'
,ordi.BillToStateFullName as 'BillToStateFullName'
,ordi.BillToPostalCode as 'BillToPostalCode'
,ordi.BillToPostalCodeExt as 'BillToPostalCodeExt'
,ordi.BillToCounty as 'BillToCounty'
,ordi.BillToCountry as 'BillToCountry'
,ordi.BillToEmail as 'BillToEmail'
,ordi.BillToDaytimePhone as 'BillToDaytimePhone'
,ordi.BillToNightPhone as 'BillToNightPhone'

,( 
    SELECT 
    ordl.SalesOrderItemsUuid as 'SalesOrderItemsUuid'
    ,ordl.SKU as 'SKU'
    ,CAST(ordl.OrderQty as INT) as 'OrderQty'
    ,CAST(ordl.ShipQty as INT) as 'ShipQty'
    ,CAST(ordl.CancelledQty as INT) as 'CancelQty'
    ,ordl.Price as 'UnitPrice'
    ,ordl.TaxAmount as 'LineItemTaxAmount'
    ,ordl.ShippingAmount as 'LineShippingAmount'
    ,ordl.ShippingTaxAmount as 'LineShippingTaxAmount'
    ,ordl.MiscAmount as 'LineGiftAmount'
    ,ordl.MiscTaxAmount as 'LineGiftTaxAmount'
    ,ordl.DiscountAmount as 'LinePromotionAmount'
    ,--ordl.BundleItemFulfilmentLineNum as 'BundleItemFulfilmentLineNum'
    ,ordl.EnterDateUtc as 'EnterDate'

    ,prd.CentralProductNum as 'CentralProductNum'
    ,prd.UPC as 'UPC'
    ,prd.ProductTitle as 'ItemTitle'
    ,prd.BundleType as 'BundleType'

    ,chol.DatabaseNum as 'CentralDatabaseNum'
    ,chol.CentralOrderLineNum as 'CentralOrderLineNum'
    ,chol.ChannelItemID as 'ChannelItemID'
    ,chol.LineShippingDiscount as 'LineShippingDiscount'
    ,chol.LineShippingDiscountTaxAmount as 'LineShippingDiscountTaxAmount'
    ,chol.LineRecyclingFee as 'LineRecyclingFee'
    ,chol.LineGiftMsg as 'LineGiftMsg'
    ,chol.LineGiftNotes as 'LineGiftNotes'
    ,chol.LinePromotionCodes as 'LinePromotionCodes'
    ,chol.LinePromotionTaxAmount as 'LinePromotionTaxAmount'
    ,olm.MerchantSKU as 'MerchantSKU'
    ,olm.MerchantColorCode as 'MerchantColorCode'
    ,olm.MerchantSizeCode as 'MerchantSizeCode'
    ,chol.UPC as 'LineItemUPC'
    ,chol.EAN as 'LineItemEAN'
    ,olm.PackingSlipLineMessage as 'LineReferenceNumber01'
    ,'' as 'LineReferenceNumber02'
    ,'' as 'LineReferenceNumber03'
    ,0 as 'Length'

    FROM SalesOrderItems(nolock) ordl
    LEFT JOIN ProductBasic(nolock) prd
         ON ( prd.MasterAccountNum=ord.MasterAccountNum
          AND prd.ProfileNum=ord.ProfileNum
          AND prd.SKU=ordl.SKU
          )

    LEFT JOIN OrderLine(nolock) chol
         ON ( chol.CentralOrderLineUuid=ordl.CentralOrderLineUuid)

    LEFT JOIN OrderLineMerchantExt(nolock) olm ON (olm.CentralOrderLineUuid= ordl.CentralOrderLineUuid)

    WHERE ordl.SalesOrderUuid = ord.SalesOrderUuid 
    FOR JSON PATH
) AS OrderLineList

--(
--     --TODO select columns where wms need.
--     select CentralOrderNum 
--     from OrderHeader headerExt
--     where headerExt.CentralOrderNum=cho.CentralOrderNum
--     for json path
--    --, WITHOUT_ARRAY_WRAPPER
--) as OrderHeaderJson,

--(
--     --TODO select columns where wms need.
--     select CentralOrderNum 
--     from OrderLine lineExt
--     where lineExt.CentralOrderNum=cho.CentralOrderNum
--     for json path
--) as OrderLineJson 
            ";

            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM EventProcessERP(nolock) epe
 INNER JOIN SalesOrderHeader(nolock) ord  
        on  ord.MasterAccountNum=epe.MasterAccountNum
        and ord.ProfileNum=epe.ProfileNum
        and ord.SalesOrderUuid=epe.ProcessUuid
 LEFT JOIN SalesOrderHeaderInfo(nolock) ordi ON (ordi.SalesOrderUuid = ord.SalesOrderUuid)
 LEFT JOIN OrderHeader (nolock)cho ON (cho.CentralOrderNum = ordi.CentralOrderNum)
 left join OrderHeaderMerchantExt (nolock) ccHeaderExt on ccHeaderExt.CentralOrderUuid=cho.CentralOrderUuid
 LEFT JOIN Setting_Channel(nolock) chanel ON(epe.MasterAccountNum = chanel.MasterAccountNum AND epe.ProfileNum = chanel.ProfileNum AND epe.ChannelNum = chanel.ChannelNum)
 LEFT JOIN Setting_ChannelAccount(nolock) channelAccount ON(epe.MasterAccountNum = chanel.MasterAccountNum AND epe.ProfileNum = chanel.ProfileNum AND epe.ChannelNum = chanel.ChannelNum AND epe.ChannelAccountNum = channelAccount.ChannelAccountNum)
";
            return this.SQL_From;
        }


        //protected override string GetSQL_where()
        //{
        //    var whereSql = base.GetSQL_where();
        //    whereSql += $" AND ord.OrderStatus not in ({(int)SalesOrderStatus.Hold},{(int)SalesOrderStatus.Cancelled})";
        //    return whereSql;
        //}

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
