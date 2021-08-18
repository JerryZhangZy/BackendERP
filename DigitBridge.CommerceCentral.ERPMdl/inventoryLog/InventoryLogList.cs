using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.InventoryLogHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryLogList : SqlQueryBuilder<InventoryLogQuery>
    {
        public InventoryLogList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InventoryLogList(IDataBaseFactory dbFactory, InventoryLogQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT {Helper.SelectAll()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            //paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            //paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

            return paramList.ToArray();
        
        }

        #endregion override methods

        public virtual InventoryLogPayload GetInventoryLogList(InventoryLogPayload payload)
        {
            if (payload == null)
                payload = new InventoryLogPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryLogListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.InventoryLogList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryLogListCount = 0;
                payload.InventoryLogList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<InventoryLogPayload> GetInventoryLogListAsync(InventoryLogPayload payload)
        {
            if (payload == null)
                payload = new InventoryLogPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryLogListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.InventoryLogList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryLogListCount = 0;
                payload.InventoryLogList = null;
                return payload;
                throw;
            }
            return payload;
        }

    }
}
