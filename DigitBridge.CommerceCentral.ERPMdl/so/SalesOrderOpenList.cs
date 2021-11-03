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

        #region select TODO pick necessary table and columns.
        protected string GetHeader_Columns()
        {
            var columns = $@"
{Helper.SelectAll(Helper.TableAllies, Helper.TableName)},
COALESCE(ordtp.text, '') as '{Helper.TableName}.OrderTypeText',  
COALESCE(ordst.text, '') as '{Helper.TableName}.OrderStatusText'
";
            return columns;
        }
        protected string GetHeaderInfo_Columns()
        {
            var columns = $@"
{InfoHelper.SelectAll(InfoHelper.TableAllies, InfoHelper.TableName)},
chanel.ChannelName as '{InfoHelper.TableName}.ChannelName',
channelAccount.ChannelAccountName as '{InfoHelper.TableName}.ChannelAccountName' 
";
            return columns;
        }
        protected string GetHeaderAttr_Columns()
        {
            var columns = $@"
{InfoAttrHelper.SelectAll(InfoAttrHelper.TableAllies, InfoAttrHelper.TableName)} 
";
            return columns;
        }
        protected string GetItemWithItemAttr_Columns()
        {
            var columns = $@"
( 
SELECT 
 {ItemHelper.SelectAll(ItemHelper.TableAllies)}
,{ItemAttrHelper.SelectAll(ItemAttrHelper.TableAllies, ItemAttrHelper.TableName)} 
FROM { ItemHelper.TableName} { ItemHelper.TableAllies}
LEFT JOIN { ItemAttrHelper.TableName} { ItemAttrHelper.TableAllies} ON({ ItemAttrHelper.TableAllies}.SalesOrderItemsUuid = { ItemHelper.TableAllies}.SalesOrderItemsUuid)
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
,{GetHeaderInfo_Columns()}
,{GetHeaderAttr_Columns()}
,{GetItemWithItemAttr_Columns()}  
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
                    payload.SalesOrderOpenList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderOpenListCount = 0;
                payload.SalesOrderOpenList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }
    }
}
