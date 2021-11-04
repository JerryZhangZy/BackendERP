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
            var header = "WMSOrderHeader";
            var columns = $@"
{Helper.TableAllies}.RowNum as '{header}.RowNum',
{InfoHelper.TableAllies}.WarehouseCode as '{header}.WarehouseCode',
{Helper.TableAllies}.DatabaseNum as '{header}.CentralDatabaseNum',
{InfoHelper.TableAllies}.CentralOrderNum as '{header}.CentralOrderNum',
{InfoHelper.TableAllies}.ChannelNum as '{header}.ChannelNum',
{InfoHelper.TableAllies}.ChannelAccountNum as '{header}.ChannelAccountNum',
channelAccount.ChannelAccountName as '{header}.ChannelAccountName',
{InfoHelper.TableAllies}.ChannelOrderId as '{header}.ChannelOrderId',
{InfoHelper.TableAllies}.SecondaryChannelOrderId as '{header}.SecondaryChannelOrderId',
{Helper.TableAllies}.OrderNumber as '{header}.SellerOrderId',
{Helper.TableAllies}.Currency as '{header}.Currency',
{Helper.TableAllies}.OrderDate as '{header}.OriginalOrderDate',
{InfoHelper.TableAllies}.Notes as '{header}.SellerPrivateNotes',
{Helper.TableAllies}.TotalAmount as '{header}.TotalOrderAmount',
{Helper.TableAllies}.TaxAmount as '{header}.TotalTaxAmount',
{Helper.TableAllies}.ShippingAmount as '{header}.TotalShippingAmount',
{Helper.TableAllies}.ShippingTaxAmount as '{header}.TotalShippingTaxAmount',
{InfoHelper.TableAllies}.ShipToName as '{header}.ShipToName',
{InfoHelper.TableAllies}.ShipToFirstName as '{header}.ShipToFirstName',
{InfoHelper.TableAllies}.ShipToLastName as '{header}.ShipToLastName',
{InfoHelper.TableAllies}.ShipToSuffix as '{header}.ShipToSuffix',
{InfoHelper.TableAllies}.ShipToCompany as '{header}.ShipToCompany',
{InfoHelper.TableAllies}.ShipToCompanyJobTitle as '{header}.ShipToCompanyJobTitle',
{InfoHelper.TableAllies}.ShipToAttention as '{header}.ShipToAttention',
{InfoHelper.TableAllies}.ShipToDaytimePhone as '{header}.ShipToDaytimePhone',
{InfoHelper.TableAllies}.ShipToNightPhone as '{header}.ShipToNightPhone',
{InfoHelper.TableAllies}.ShipToAddressLine1 as '{header}.ShipToAddressLine1',
{InfoHelper.TableAllies}.ShipToAddressLine2 as '{header}.ShipToAddressLine2',
{InfoHelper.TableAllies}.ShipToAddressLine3 as '{header}.ShipToAddressLine3',
{InfoHelper.TableAllies}.ShipToCity as '{header}.ShipToCity',
{InfoHelper.TableAllies}.ShipToState as '{header}.ShipToState',
{InfoHelper.TableAllies}.ShipToStateFullName as '{header}.ShipToStateFullName',
{InfoHelper.TableAllies}.ShipToPostalCode as '{header}.ShipToPostalCode',
{InfoHelper.TableAllies}.ShipToPostalCodeExt as '{header}.ShipToPostalCodeExt',
{InfoHelper.TableAllies}.ShipToCounty as '{header}.ShipToCounty',
{InfoHelper.TableAllies}.ShipToCountry as '{header}.ShipToCountry',
{InfoHelper.TableAllies}.ShipToEmail as '{header}.ShipToEmail',
{InfoHelper.TableAllies}.BillToName as '{header}.BillToName',
{InfoHelper.TableAllies}.BillToFirstName as '{header}.BillToFirstName',
{InfoHelper.TableAllies}.BillToLastName as '{header}.BillToLastName',
{InfoHelper.TableAllies}.BillToSuffix as '{header}.BillToSuffix',
{InfoHelper.TableAllies}.BillToCompany as '{header}.BillToCompany',
{InfoHelper.TableAllies}.BillToCompanyJobTitle as '{header}.BillToCompanyJobTitle',
{InfoHelper.TableAllies}.BillToAttention as '{header}.BillToAttention',
{InfoHelper.TableAllies}.BillToAddressLine1 as '{header}.BillToAddressLine1',
{InfoHelper.TableAllies}.BillToAddressLine2 as '{header}.BillToAddressLine2',
{InfoHelper.TableAllies}.BillToAddressLine3 as '{header}.BillToAddressLine3',
{InfoHelper.TableAllies}.BillToCity as '{header}.BillToCity',
{InfoHelper.TableAllies}.BillToState as '{header}.BillToState',
{InfoHelper.TableAllies}.BillToStateFullName as '{header}.BillToStateFullName',
{InfoHelper.TableAllies}.BillToPostalCode as '{header}.BillToPostalCode',
{InfoHelper.TableAllies}.BillToPostalCodeExt as '{header}.BillToPostalCodeExt',
{InfoHelper.TableAllies}.BillToCounty as '{header}.BillToCounty',
{InfoHelper.TableAllies}.BillToCountry as '{header}.BillToCountry',
{InfoHelper.TableAllies}.BillToEmail as '{header}.BillToEmail',
{InfoHelper.TableAllies}.BillToDaytimePhone as '{header}.BillToDaytimePhone',
{InfoHelper.TableAllies}.BillToNightPhone as '{header}.BillToNightPhone'
";
            return columns;
        }
        protected string GetItem_Columns()
        {
            var item = "WMSOrderLine";

            var columns = $@"
{ItemHelper.TableAllies}.SKU as '{item}.SKU',
{ItemHelper.TableAllies}.OrderQty as '{item}.OrderQty',
{ItemHelper.TableAllies}.ShipQty as '{item}.ShipQty',
{ItemHelper.TableAllies}.CancelledQty as '{item}.CancelQty',
{ItemHelper.TableAllies}.Price as '{item}.UnitPrice',
{ItemHelper.TableAllies}.TaxAmount as '{item}.LineItemTaxAmount',
{ItemHelper.TableAllies}.ShippingAmount as '{item}.LineShippingAmount',
{ItemHelper.TableAllies}.ShippingTaxAmount as '{item}.LineShippingTaxAmount',
{ItemHelper.TableAllies}.ItemDate as '{item}.EnterDate'
";
            return columns;
        }
        protected string GetItem_Script()
        {
            var columns = $@"
( 
SELECT 
{GetItem_Columns()}
FROM { ItemHelper.TableName} { ItemHelper.TableAllies}
WHERE { ItemHelper.TableAllies}.SalesOrderUuid = { Helper.TableAllies}.SalesOrderUuid 
FOR JSON PATH
) AS SalesOrderItems";
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
 FROM {Helper.TableName} {Helper.TableAllies}
 LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({InfoHelper.TableAllies}.SalesOrderUuid = {Helper.TableAllies}.SalesOrderUuid)
 LEFT JOIN {InfoAttrHelper.TableName} {InfoAttrHelper.TableAllies} ON ({InfoAttrHelper.TableAllies}.SalesOrderUuid = {Helper.TableAllies}.SalesOrderUuid)
 LEFT JOIN @SalesOrderStatus ordst ON ({Helper.TableAllies}.OrderStatus = ordst.num)
 LEFT JOIN @SalesOrderType ordtp ON ({Helper.TableAllies}.OrderType = ordtp.num) 
 {SqlStringHelper.Join_Setting_Channel(masterAccountNum, profileNum, channelNum)}
 {SqlStringHelper.Join_Setting_ChannelAccount(masterAccountNum, profileNum, channelNum, channelAccountNum)}
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

            return paramList.ToArray();

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
                    payload.SalesOrderOpenList = sb.ToString();
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.SalesOrderOpenListCount = 0;
                payload.SalesOrderOpenList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }
    }
}
