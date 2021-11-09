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
using Helper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderHelper;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderInfoHelper;
using ItemHelper = DigitBridge.CommerceCentral.ERPDb.InvoiceItemsHelper;
using EventHelper = DigitBridge.CommerceCentral.ERPDb.EventProcessERPHelper;
namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceUnprocessList : SqlQueryBuilderForEventProcess<InvoiceUnprocessQuery>
    {
        public InvoiceUnprocessList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceUnprocessList(IDataBaseFactory dbFactory, InvoiceUnprocessQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }


        #region get select columns
        protected string GetHeader_Columns()
        {
            var header = "InvoiceHeader";
            var columns = $@"
{EventHelper.TableAllies}.EventUuid,
--{Helper.TableAllies}.OrderInvoiceNum as '{header}.OrderInvoiceNum',
{Helper.TableAllies}.DatabaseNum as '{header}.DatabaseNum',
{Helper.TableAllies}.MasterAccountNum as '{header}.MasterAccountNum',
{Helper.TableAllies}.ProfileNum as '{header}.ProfileNum',
{InfoHelper.TableAllies}.ChannelNum as '{header}.ChannelNum',
{InfoHelper.TableAllies}.ChannelAccountNum as '{header}.ChannelAccountNum',
{Helper.TableAllies}.InvoiceNumber as '{header}.InvoiceNumber',
{Helper.TableAllies}.InvoiceDate as '{header}.InvoiceDateUtc',
{InfoHelper.TableAllies}.CentralOrderNum as '{header}.CentralOrderNum',
{InfoHelper.TableAllies}.ChannelOrderID as '{header}.ChannelOrderID',
--{InfoHelper.TableAllies}.OrderDCAssignmentNum as '{header}.OrderDCAssignmentNum',
{InfoHelper.TableAllies}.OrderShipmentNum as '{header}.OrderShipmentNum',--need join
--{InfoHelper.TableAllies}.ShipmentID as '{header}.ShipmentID',--need join
{Helper.TableAllies}.ShipDate as '{header}.ShipmentDateUtc',
{InfoHelper.TableAllies}.ShippingCarrier as '{header}.ShippingCarrier',
{InfoHelper.TableAllies}.ShippingClass as '{header}.ShippingClass',
{Helper.TableAllies}.ShippingAmount as '{header}.ShippingCost',
--{InfoHelper.TableAllies}.MainTrackingNumber as '{header}.MainTrackingNumber',--need join
{Helper.TableAllies}.SubTotalAmount as '{header}.InvoiceAmount',
{Helper.TableAllies}.TaxAmount as '{header}.InvoiceTaxAmount',
{Helper.TableAllies}.ChargeAndAllowanceAmount as '{header}.InvoiceHandlingFee',
{Helper.TableAllies}.DiscountAmount as '{header}.InvoiceDiscountAmount',
{Helper.TableAllies}.TotalAmount as '{header}.TotalAmount',
--{Helper.TableAllies}.InvoiceTermsType as '{header}.InvoiceTermsType',
{Helper.TableAllies}.Terms as '{header}.InvoiceTermsDescrption',
{Helper.TableAllies}.TermsDays as '{header}.InvoiceTermsDays'
--{Helper.TableAllies}.DBChannelOrderHeaderRowID as '{header}.DBChannelOrderHeaderRowID',
--{Helper.TableAllies}.EnterDateUtc as '{header}.EnterDateUtc' 
";
            return columns;
        }
        protected string GetItem_Columns()
        {
            //TODO adjust column mapping.
            var columns = $@"
{ItemHelper.TableAllies}.Seq AS OrderInvoiceLineNum,
--{ItemHelper.TableAllies}.DatabaseNum AS DatabaseNum,
--{ItemHelper.TableAllies}.MasterAccountNum AS MasterAccountNum,
--{ItemHelper.TableAllies}.ProfileNum AS ProfileNum,
--{ItemHelper.TableAllies}.ChannelNum AS ChannelNum,
--{ItemHelper.TableAllies}.ChannelAccountNum AS ChannelAccountNum,
--{ItemHelper.TableAllies}.OrderShipmentItemNum AS OrderShipmentItemNum,
--{ItemHelper.TableAllies}.CentralOrderLineNum AS CentralOrderLineNum,
--{ItemHelper.TableAllies}.OrderDCAssignmentLineNum AS OrderDCAssignmentLineNum,
{ItemHelper.TableAllies}.SKU AS SKU,
--{ItemHelper.TableAllies}.ChannelItemID AS ChannelItemID,
{ItemHelper.TableAllies}.ShipQty AS ShippedQty,
{ItemHelper.TableAllies}.DiscountPrice AS UnitPrice,
{ItemHelper.TableAllies}.ExtAmount AS LineItemAmount,
{ItemHelper.TableAllies}.TaxAmount AS LineTaxAmount,
{ItemHelper.TableAllies}.ChargeAndAllowanceAmount AS LineHandlingFee,
{ItemHelper.TableAllies}.DiscountAmount AS LineDiscountAmount,
{ItemHelper.TableAllies}.ItemTotalAmount AS LineAmount
--{ItemHelper.TableAllies}.DBChannelOrderLineRowID AS DBChannelOrderLineRowID,
--{ItemHelper.TableAllies}.ItemStatus AS ItemStatus,
--{ItemHelper.TableAllies}.EnterDateUtc AS EnterDateUtc
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
WHERE { ItemHelper.TableAllies}.InvoiceUuid = { Helper.TableAllies}.InvoiceUuid 
FOR JSON PATH
) AS InvoiceItems";
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
            this.SQL_From = $@"
 FROM {EventHelper.TableName} {EventHelper.TableAllies}
 INNER JOIN {Helper.TableName} {Helper.TableAllies}  
        on  {Helper.TableAllies}.MasterAccountNum={EventHelper.TableAllies}.MasterAccountNum
        and {Helper.TableAllies}.ProfileNum={EventHelper.TableAllies}.ProfileNum
        and {Helper.TableAllies}.InvoiceUuid={EventHelper.TableAllies}.ProcessUuid
 LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({Helper.TableAllies}.InvoiceUuid = {InfoHelper.TableAllies}.InvoiceUuid)
";
            return this.SQL_From;
        }

        //public override SqlParameter[] GetSqlParameters()
        //{
        //    var paramList = base.GetSqlParameters().ToList();
        //    paramList.Add("@EventProcessActionStatusEnum".ToEnumParameter<InvoiceStatusEnum>());
        //    paramList.Add("@EventProcessTypeEnum".ToEnumParameter<EventProcessTypeEnum>());

        //    return paramList.ToArray();

        //}

        #endregion override methods

        public virtual void GetUnprocessedInvoices(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceUnprocessListCount = Count();
                payload.Success = ExcuteJsonForQueryByLocked(sb, EventProcessTypeEnum.InvoiceToChanel);
                payload.Messages = this.Messages;
                if (payload.Success)
                    payload.InvoiceUnprocessList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceUnprocessListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetUnprocessedInvoicesAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceUnprocessListCount = await CountAsync();
                payload.Success = await ExcuteJsonForQueryByLockedAsync(sb, EventProcessTypeEnum.InvoiceToChanel);
                payload.Messages = this.Messages;
                if (payload.Success)
                    payload.InvoiceUnprocessList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceUnprocessListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }


    }
}
