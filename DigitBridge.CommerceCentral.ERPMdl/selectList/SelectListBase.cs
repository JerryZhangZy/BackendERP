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
using Hepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderHelper;
using InfoHepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderInfoHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface ISelectList
    {
        string Name { get; }

        bool GetSelectList(SelectListPayload payload);
        Task<bool> GetSelectListAsync(SelectListPayload payload);
    }

    public class SelectListBase : SqlQueryBuilderRawSql<SelectListQuery>, ISelectList
    {
        public virtual string Name => string.Empty;

        public SelectListBase(IDataBaseFactory dbFactory) : base(dbFactory, new SelectListQuery())
        {
        }

        protected virtual void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.customerCode, '') LIKE '{this.QueryObject.Term.FilterValue}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT i.*, COALESCE(d.CustomerName, '') AS description 
FROM (
    SELECT 
        tbl.CustomerCode AS cd, 
        COUNT(1) AS cnt
    FROM InvoiceHeader tbl
    WHERE COALESCE(tbl.CustomerCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.CustomerCode
) i 
LEFT JOIN Customer d ON (d.CustomerUuid = i.CustomerUuid) 
ORDER BY i.cd
";
            return this.SQL_Select;
        }

        public virtual bool GetSelectList(SelectListPayload payload)
        {
            if (payload == null)
                payload = new SelectListPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.Data = sb;
            }
            catch (Exception ex)
            {
                payload.Data = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
            return payload.Success;
        }

        public virtual async Task<bool> GetSelectListAsync(SelectListPayload payload)
        {
            if (payload == null)
                payload = new SelectListPayload();

            this.LoadRequestParameter(payload);
            if (payload.With_Term)
                this.QueryObject.Term.FilterValue = payload.Term;

            this.QueryObject.LoadJson = true;
            StringBuilder sb = new StringBuilder();
            try
            {
                //TODO 
                //if(payload.IsQueryTotalCount)
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.Data = sb;
            }
            catch (Exception ex)
            {
                payload.Data = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
            return payload.Success;
        }
    }
}
