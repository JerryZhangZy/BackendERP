using DigitBridge.Base.Common;
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
    public class VendorSummaryInquiry : SqlQueryBuilder<VendorSummaryQuery>
    {
        public VendorSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public VendorSummaryInquiry(IDataBaseFactory dbFactory, VendorSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        private void LoadSummaryParameter(CompanySummaryPayload payload)
        {
            if (payload == null)
                return;
            QueryObject.QueryFilterList.First(x => x.Name == "MasterAccountNum").SetValue(payload.MasterAccountNum);
            QueryObject.QueryFilterList.First(x => x.Name == "ProfileNum").SetValue(payload.ProfileNum);
            //QueryObject.QueryFilterList.First(x => x.Name == "VendorCode").SetValue(payload.Filters.VendorCode);
            QueryObject.QueryFilterList.First(x => x.Name == "VendorName").SetValue(payload.Filters.Name);
            QueryObject.QueryFilterList.First(x => x.Name == "DateFrom").SetValue(payload.Filters.DateFrom);
            QueryObject.QueryFilterList.First(x => x.Name == "DateTo").SetValue(payload.Filters.DateTo);
        }
        protected override string GetSQL_select()
        {
            var whereVendor = this.QueryObject.GetSQLWithoutPrefix("ins");
            var whereInvoice = this.QueryObject.GetSQLWithPrefix("ins");
            var whereVendorAnd = string.Empty;

            if (!string.IsNullOrWhiteSpace(whereVendor))
            {
                whereVendorAnd = $" AND {whereVendor}";
                whereVendor = $" WHERE {whereVendor}";
            }

            if (!string.IsNullOrWhiteSpace(whereInvoice))
                whereInvoice = $" AND {whereInvoice}";

            this.SQL_Select = $@"
SELECT c.[count], c.new_count, c.active_count, non.[Count] AS nonsales_count
    FROM (
        SELECT
        COUNT(1) as [count], 
        SUM(
            CASE WHEN COALESCE(vd.VendorStatus, 0) = {(int)VendorStatus.New} THEN 1
			ELSE 0 END
		) as new_count,
		SUM(
            CASE WHEN COALESCE(vd.VendorStatus, 0) = {(int)VendorStatus.Active} THEN 1
			ELSE 0 END
		) as active_count
    FROM Vendor vd
    {whereVendor}
) c
OUTER APPLY(
    SELECT COUNT(1) AS[Count]
    FROM vendor vd

    WHERE
    NOT EXISTS(
        select* FROM InvoiceHeader ins
        WHERE ins.VendorUuid = vd.VendorUuid {whereInvoice}
    )
    {whereVendorAnd}
) non
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            return "--";
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            try
            {
                var result = await ExcuteAsync(GetSQL_select());
                if (result != null && result.HasData)
                {
                    //payload.Summary.VendorCount = result.GetData("count").ToInt();
                    //payload.Summary.NewVendorCount = result.GetData("new_count").ToInt();
                    //payload.Summary.NonSalesVendorCount = result.GetData("nonsales_count").ToInt();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

        public async virtual Task GetVendorSummaryAsync(VendorPayload payload)
        {
            if (payload == null)
                payload = new VendorPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success =await ExcuteJsonAsync(sb,$"{GetSQL_select()} FOR JSON PATH");
                if (payload.Success)
                    payload.VendorSummary = sb;
            }
            catch (Exception ex)
            {
                payload.VendorSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
                payload.Success = false;
            }
        }
    }
}
