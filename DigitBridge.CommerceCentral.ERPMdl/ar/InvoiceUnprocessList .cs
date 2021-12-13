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
    public class InvoiceUnprocessList : SqlQueryBuilder<InvoiceUnprocessQuery>
    {
        public InvoiceUnprocessList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceUnprocessList(IDataBaseFactory dbFactory, InvoiceUnprocessQuery queryObject)
            : base(dbFactory, queryObject)
        {
        } 

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
COALESCE(ins.InvoiceUuid,'') as 'InvoiceHeader.InvoiceUuid',
COALESCE(insi.RefNum,'') as 'InvoiceHeader.OrderInvoiceNum',
COALESCE(ins.DatabaseNum,0) as 'InvoiceHeader.DatabaseNum',
COALESCE(ins.MasterAccountNum,0) as 'InvoiceHeader.MasterAccountNum',
COALESCE(ins.ProfileNum,0) as 'InvoiceHeader.ProfileNum',
COALESCE(insi.ChannelNum,0) as 'InvoiceHeader.ChannelNum',
COALESCE(insi.ChannelAccountNum,0) as 'InvoiceHeader.ChannelAccountNum',
COALESCE(ins.InvoiceNumber,'') as 'InvoiceHeader.InvoiceNumber',
ins.InvoiceDate as 'InvoiceHeader.InvoiceDateUtc',
COALESCE(insi.CentralOrderNum,0) as 'InvoiceHeader.CentralOrderNum',
COALESCE(insi.ChannelOrderID,'') as 'InvoiceHeader.ChannelOrderID',
COALESCE(insi.OrderDCAssignmentNum,0) as 'InvoiceHeader.OrderDCAssignmentNum',
COALESCE(insi.OrderShipmentNum,0) as 'InvoiceHeader.OrderShipmentNum',
COALESCE(osh.ShipmentID,'') as 'InvoiceHeader.ShipmentID',
ins.ShipDate as 'InvoiceHeader.ShipmentDateUtc',
COALESCE(insi.ShippingCarrier,'') as 'InvoiceHeader.ShippingCarrier',
COALESCE(insi.ShippingClass,'') as 'InvoiceHeader.ShippingClass',
COALESCE(ins.ShippingAmount,0) as 'InvoiceHeader.ShippingCost',
COALESCE(osh.MainTrackingNumber,'') as 'InvoiceHeader.MainTrackingNumber',
COALESCE(ins.SubTotalAmount,0) as 'InvoiceHeader.InvoiceAmount',
COALESCE(ins.TaxAmount,0) as 'InvoiceHeader.InvoiceTaxAmount',
COALESCE(ins.MiscAmount,0) as 'InvoiceHeader.InvoiceHandlingFee',
COALESCE((ins.SalesAmount - ins.DiscountAmount),0) as 'InvoiceHeader.InvoiceDiscountAmount',
COALESCE(ins.TotalAmount,0) as 'InvoiceHeader.TotalAmount',
COALESCE(ins.Terms,'') as 'InvoiceHeader.InvoiceTermsType',
--ins.Terms as 'InvoiceHeader.InvoiceTermsDescrption',
COALESCE(ins.TermsDays,0) as 'InvoiceHeader.InvoiceTermsDays',
COALESCE(insi.DBChannelOrderHeaderRowID,0) as 'InvoiceHeader.DBChannelOrderHeaderRowID',
ins.EnterDateUtc as 'InvoiceHeader.EnterDateUtc', 
( 
    SELECT 
        COALESCE(insl.RowNum,0) AS OrderInvoiceLineNum,
        COALESCE(ins.DatabaseNum,0) AS DatabaseNum,
        COALESCE(ins.MasterAccountNum,0) AS MasterAccountNum,
        COALESCE(ins.ProfileNum,0) AS ProfileNum,
        COALESCE(insi.ChannelNum,0) AS ChannelNum,
        COALESCE(insi.ChannelAccountNum,0) AS ChannelAccountNum,
        --insi.OrderShipmentItemNum AS OrderShipmentItemNum,
        --insl.CentralOrderLineNum AS CentralOrderLineNum,
        COALESCE(insl.OrderDCAssignmentLineNum,0) AS OrderDCAssignmentLineNum,
        COALESCE(insl.SKU,'') AS SKU,
        --insl.ChannelItemID AS ChannelItemID,
        COALESCE(insl.ShipQty,0) AS ShippedQty,
        COALESCE(insl.DiscountPrice,0) AS UnitPrice,
        COALESCE(insl.ExtAmount,0) AS LineItemAmount,
        COALESCE(insl.TaxAmount,0) AS LineTaxAmount,
        0 AS LineHandlingFee,
        COALESCE(insl.DiscountAmount,0) AS LineDiscountAmount,
        COALESCE(insl.ItemTotalAmount,0) AS LineAmount,
        COALESCE(insl.DBChannelOrderLineRowID,0) AS DBChannelOrderLineRowID,
        --insl.InvoiceItemStatus AS ItemStatus,
        insl.EnterDateUtc AS EnterDateUtc
    FROM InvoiceItems insl
    WHERE insl.InvoiceUuid = ins.InvoiceUuid 
    FOR JSON PATH
) AS InvoiceItems
            ";

            return this.SQL_Select;
        }


        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM EventProcessERP epe
 INNER JOIN InvoiceHeader ins on (
        ins.MasterAccountNum=epe.MasterAccountNum
        and ins.ProfileNum=epe.ProfileNum
        and ins.InvoiceUuid=epe.ProcessUuid
)
 LEFT JOIN InvoiceHeaderInfo insi ON (ins.InvoiceUuid = insi.InvoiceUuid)
 LEFT JOIN OrderShipmentHeader osh ON (insi.OrderShipmentUuid = osh.OrderShipmentUuid)
";
            return this.SQL_From;
        } 

        #endregion override methods

        public virtual async Task GetUnprocessedInvoicesAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceUnprocessListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.InvoiceUnprocessList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceUnprocessListCount = 0;
                AddError(ex.ObjectToString());
            }
            payload.Messages = this.Messages;
        }
    }
}
