using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryLogInquiry : SqlQueryBuilder<InventoryLogSummaryQuery>
    {
        public InventoryLogInquiry(IDataBaseFactory dbFactory)
            : base(dbFactory)
        { }

        public InventoryLogInquiry(IDataBaseFactory dbFactory, InventoryLogSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        { }

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM() as TotalInQty,
SUM() as TotalOutQty,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Invoice') THEN 1 ELSE 0 END
) as InvoiceCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('InvoiceReturn') THEN 1 ELSE 0 END
) as InvoiceReturnCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Shipment') THEN 1 ELSE 0 END
) as ShipmentCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Adjust') THEN 1 ELSE 0 END
) as AdjustCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Damage') THEN 1 ELSE 0 END
) as DamageCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Count') THEN 1 ELSE 0 END
) as CountTypeCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('ToWarehouse') THEN 1 ELSE 0 END
) as ToWarehouseCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('FromWarehouse') THEN 1 ELSE 0 END
) as FromWarehouseCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Assemble') THEN 1 ELSE 0 END
) as AssembleCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('Disassemble') THEN 1 ELSE 0 END
) as DisassembleCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('POReceive') THEN 1 ELSE 0 END
) as POReceiveCount,
SUM( 
	CASE WHEN RTRIM(COALESCE({InventoryLogHelper.TableAllies}.LogType, '')) = RTRIM('POReturn') THEN 1 ELSE 0 END
) as POReturnCount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {InventoryLogHelper.TableName} {InventoryLogHelper.TableAllies} 
";
            return this.SQL_From;
        }


        public async virtual Task InventoryLogSummaryAsync(InventoryLogPayload payload)
        {
            if (payload == null)
                payload = new InventoryLogPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.InventoryLogSummary = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InventoryLogSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        private void LoadSummaryParameter(CompanySummaryPayload payload)
        {
            if (payload == null)
                return;
            QueryObject.QueryFilterList.First(x => x.Name == "MasterAccountNum").SetValue(payload.MasterAccountNum);
            QueryObject.QueryFilterList.First(x => x.Name == "ProfileNum").SetValue(payload.ProfileNum);
            QueryObject.QueryFilterList.First(x => x.Name == "LogDateFrom").SetValue(payload.Filters.DateFrom);
            QueryObject.QueryFilterList.First(x => x.Name == "LogDateTo").SetValue(payload.Filters.DateTo);
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            try
            {
                this.QueryObject.LoadJson = false;
                var result = await ExcuteAsync();
                if (result != null && result.HasData)
                {
                    payload.Summary.InventoryLogCount = result.GetData("Count").ToInt();
                    payload.Summary.TotalInQty = result.GetData("TotalInQty").ToString().ToInt();
                    payload.Summary.TotalOutQty = result.GetData("TotalOutQty").ToInt();
                    payload.Summary.InventoryLogCountOfInvoice = result.GetData("InvoiceCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfInvoiceReturn = result.GetData("InvoiceReturnCount").ToInt();
                    payload.Summary.InventoryLogCountOfShipment = result.GetData("ShipmentCount").ToInt();
                    payload.Summary.InventoryLogCountOfAdjust = result.GetData("AdjustCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfDamage = result.GetData("DamageCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfCount = result.GetData("CountTypeCount").ToInt();
                    payload.Summary.InventoryLogCountOfToWarehouse = result.GetData("ToWarehouseCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfFromWarehouse = result.GetData("FromWarehouseCount").ToInt();
                    payload.Summary.InventoryLogCountOfAssemble = result.GetData("AssembleCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfDisassemble = result.GetData("DisassembleCount").ToString().ToInt();
                    payload.Summary.InventoryLogCountOfPoReceive = result.GetData("POReceiveCount").ToInt();
                    payload.Summary.InventoryLogCountOfPoReturn = result.GetData("POReturnCount").ToString().ToInt();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }
    }
}
