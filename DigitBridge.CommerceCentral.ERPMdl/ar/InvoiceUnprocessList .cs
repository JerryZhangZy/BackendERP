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
ins.RowNum  as 'InvoiceHeader.OrderInvoiceNum',
ins.InvoiceUuid as 'InvoiceHeader.InvoiceUuid',
--ins.OrderInvoiceNum as 'InvoiceHeader.OrderInvoiceNum',
ins.DatabaseNum as 'InvoiceHeader.DatabaseNum',
ins.MasterAccountNum as 'InvoiceHeader.MasterAccountNum',
ins.ProfileNum as 'InvoiceHeader.ProfileNum',
insi.ChannelNum as 'InvoiceHeader.ChannelNum',
insi.ChannelAccountNum as 'InvoiceHeader.ChannelAccountNum',
ins.InvoiceNumber as 'InvoiceHeader.InvoiceNumber',
ins.InvoiceDate as 'InvoiceHeader.InvoiceDateUtc',
insi.CentralOrderNum as 'InvoiceHeader.CentralOrderNum',
insi.ChannelOrderID as 'InvoiceHeader.ChannelOrderID',
insi.OrderDCAssignmentNum as 'InvoiceHeader.OrderDCAssignmentNum',
insi.OrderShipmentNum as 'InvoiceHeader.OrderShipmentNum',
shipment.ShipmentId as 'InvoiceHeader.ShipmentID',
ins.ShipDate as 'InvoiceHeader.ShipmentDateUtc',
insi.ShippingCarrier as 'InvoiceHeader.ShippingCarrier',
insi.ShippingClass as 'InvoiceHeader.ShippingClass',
ins.ShippingAmount as 'InvoiceHeader.ShippingCost',
shipment.MainTrackingNumber as 'InvoiceHeader.MainTrackingNumber',
ins.SubTotalAmount as 'InvoiceHeader.InvoiceAmount',
ins.TaxAmount as 'InvoiceHeader.InvoiceTaxAmount',
ins.ChargeAndAllowanceAmount as 'InvoiceHeader.InvoiceHandlingFee',
ins.DiscountAmount as 'InvoiceHeader.InvoiceDiscountAmount',
ins.TotalAmount as 'InvoiceHeader.TotalAmount',
--ins.InvoiceTermsType as 'InvoiceHeader.InvoiceTermsType',
ins.Terms as 'InvoiceHeader.InvoiceTermsDescrption',
ins.TermsDays as 'InvoiceHeader.InvoiceTermsDays',
insi.DBChannelOrderHeaderRowID as 'InvoiceHeader.DBChannelOrderHeaderRowID',
insi.RefNum as 'InvoiceHeader.ReferenceId',
ins.EnterDateUtc as 'InvoiceHeader.EnterDateUtc', 

( 
    SELECT 
    insl.Seq AS OrderInvoiceLineNum,
    --insl.DatabaseNum AS DatabaseNum,
    --insl.MasterAccountNum AS MasterAccountNum,
    --insl.ProfileNum AS ProfileNum,
    --insl.ChannelNum AS ChannelNum,
    --insl.ChannelAccountNum AS ChannelAccountNum,
    --insl.OrderShipmentItemNum AS OrderShipmentItemNum,
    --insl.CentralOrderLineNum AS CentralOrderLineNum,
    insl.CentralOrderLineUuid  AS CentralOrderLineUuid,
    insl.OrderDCAssignmentLineNum AS OrderDCAssignmentLineNum,
    insl.OrderDCAssignmentLineUuid AS OrderDCAssignmentLineUuid,
    insl.SKU AS SKU,
    --insl.ChannelItemID AS ChannelItemID,
    insl.ShipQty AS ShippedQty,
    insl.DiscountPrice AS UnitPrice,
    insl.ExtAmount AS LineItemAmount,
    insl.TaxAmount AS LineTaxAmount,
    insl.ChargeAndAllowanceAmount AS LineHandlingFee,
    insl.DiscountAmount AS LineDiscountAmount,
    insl.ItemTotalAmount AS LineAmount,
    insl.DBChannelOrderLineRowID AS DBChannelOrderLineRowID,
    insl.InvoiceItemStatus AS ItemStatus,
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
 INNER JOIN InvoiceHeader ins  
        on  ins.MasterAccountNum=epe.MasterAccountNum
        and ins.ProfileNum=epe.ProfileNum
        and ins.InvoiceUuid=epe.ProcessUuid
 LEFT JOIN InvoiceHeaderInfo insi ON (ins.InvoiceUuid = insi.InvoiceUuid)
 LEFT JOIN OrderShipmentHeader shipment on (shipment.OrderShipmentUuid=insi.OrderShipmentUuid) and shipment.ShipmentStatus!={(int)OrderShipmentStatusEnum.Cancelled}
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
